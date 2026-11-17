using CertificateChecker.Models;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace CertificateChecker.Services
{
    /// <summary>
    /// Imports public CA certificates into Data/CertificateAuthority.db.
    /// Supports CER, CRT, PEM, DER, P7B and P7C. Private-key formats are not supported.
    /// </summary>
    public sealed class ImportRootCA
    {
        private readonly string _databasePath;

        public ImportRootCA()
        {
            _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "Data", "CertificateAuthority.db");
        }

        public ImportRootCA(string databasePath)
        {
            if (string.IsNullOrWhiteSpace(databasePath))
                throw new ArgumentException("A database path is required.", "databasePath");
            _databasePath = databasePath;
        }

        /// <summary>Imports all supported certificates found in the selected files.</summary>
        public CAImportResult ImportCertificates(IEnumerable<string> filePaths)
        {
            var result = new CAImportResult();
            if (filePaths == null)
            {
                result.ErrorMessage = "No files were selected.";
                result.Message = result.ErrorMessage;
                return result;
            }
            if (!File.Exists(_databasePath))
            {
                result.ErrorMessage = "CA database was not found: " + _databasePath;
                result.Message = result.ErrorMessage;
                return result;
            }

            var candidates = new List<X509Certificate2>();
            foreach (string path in filePaths)
            {
                try { candidates.AddRange(ReadCertificates(path)); }
                catch (Exception ex)
                {
                    result.RejectedCount++;
                    AddError(result, Path.GetFileName(path) + ": " + ex.Message);
                }
            }

            // Remove duplicates inside the selected files before validating/importing.
            var unique = new List<X509Certificate2>();
            var fingerprints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (X509Certificate2 cert in candidates)
            {
                if (fingerprints.Add(GetSHA256Fingerprint(cert))) unique.Add(cert);
                else { result.DuplicateCount++; cert.Dispose(); }
            }

            try
            {
                using (var connection = OpenConnection())
                {
                    long userSourceId = GetUserImportedSourceId(connection);
                    var pending = new List<X509Certificate2>();
                    foreach (X509Certificate2 cert in unique)
                    {
                        if (!IsCACertificate(cert))
                        {
                            result.RejectedCount++;
                            AddError(result, "A selected certificate is not a CA certificate.");
                        }
                        else if (CertificateExists(connection, GetSHA256Fingerprint(cert)))
                        {
                            result.DuplicateCount++;
                        }
                        else pending.Add(cert);
                    }

                    // Process parents before children. Each successful insert is committed
                    // separately so a bad certificate cannot leave a partial row behind.
                    bool madeProgress;
                    do
                    {
                        madeProgress = false;
                        for (int i = pending.Count - 1; i >= 0; i--)
                        {
                            X509Certificate2 child = pending[i];
                            bool selfSigned = IsCryptographicallySelfSigned(child);
                            X509Certificate2 parent = null;
                            long parentId = 0;
                            bool parentInDatabase = false;

                            if (!selfSigned)
                            {
                                parent = FindValidParent(connection, child, out parentId);
                                parentInDatabase = parent != null;
                                if (parent != null && !HasDatabaseRootChain(connection, parent, pending, child))
                                {
                                    parent.Dispose();
                                    parent = null;
                                    parentId = 0;
                                    parentInDatabase = false;
                                }
                                if (parent == null)
                                {
                                    // Also allow a parent supplied in the same selection,
                                    // provided that its eventual chain reaches a DB root.
                                    parent = pending.FirstOrDefault(p => !ReferenceEquals(p, child)
                                        && IsValidIssuer(child, p));
                                    if (parent != null) parentId = 0;
                                }
                                if (parent == null) continue; // may become resolvable next pass
                            }

                            // If the issuer was in this same file selection, wait until
                            // that issuer has itself been imported and is available in
                            // the database. This makes each imported chain terminate at
                            // a root already stored in the database.
                            if (!selfSigned && !parentInDatabase) continue;

                            try
                            {
                                using (var tx = connection.BeginTransaction())
                                {
                                    long childId = InsertCertificate(connection, tx, child,
                                        selfSigned ? "Root" : "Intermediate", userSourceId);
                                    if (!selfSigned)
                                    {
                                        if (!parentInDatabase)
                                            parentId = FindCertificateId(connection, tx,
                                                GetSHA256Fingerprint(parent));
                                        InsertIssuer(connection, tx, childId, parentId, userSourceId);
                                    }
                                    tx.Commit();
                                }
                                result.ImportedCount++;
                                pending.RemoveAt(i);
                                madeProgress = true;
                            }
                            catch (Exception ex)
                            {
                                result.RejectedCount++;
                                AddError(result, child.Subject + ": " + ex.Message);
                                pending.RemoveAt(i);
                            }
                        }
                    } while (madeProgress && pending.Count > 0);

                    foreach (X509Certificate2 cert in pending)
                    {
                        result.RejectedCount++;
                        result.MissingRootCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(result, ex.Message);
            }
            finally
            {
                foreach (X509Certificate2 cert in candidates) cert.Dispose();
            }

            result.Success = result.ImportedCount > 0;
            result.Message = "Import completed. Imported: " + result.ImportedCount
                + "; already present: " + result.DuplicateCount
                + "; rejected: " + result.RejectedCount + ".";
            if (result.MissingRootCount > 0)
            {
                result.Message += Environment.NewLine + Environment.NewLine
                    + "The certificate cannot be imported because its Issuing CA certificate is not available in the database."
                    + Environment.NewLine + Environment.NewLine
                    + "Please import the Issuing CA certificate first, then try importing this certificate again."
                    + Environment.NewLine + Environment.NewLine
                    + "If the Issuing CA is an Intermediate CA, make sure its complete certificate chain, including the Root CA, is available in the database.";
            }
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                result.Message += Environment.NewLine + Environment.NewLine + result.ErrorMessage;
            return result;
        }

        private static List<X509Certificate2> ReadCertificates(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                throw new FileNotFoundException(
                    "The selected file was not found.", path);

            string ext = Path.GetExtension(path).ToLowerInvariant();

            string[] supported =
            {
        ".cer", ".crt", ".pem", ".der", ".p7b", ".p7c"
    };

            if (!supported.Contains(ext))
                throw new NotSupportedException(
                    "Unsupported file type: " + ext);

            byte[] data = File.ReadAllBytes(path);
            var output = new List<X509Certificate2>();

            // PEM: dosyanın içinde birden fazla sertifika olabilir.
            string text = System.Text.Encoding.ASCII.GetString(data);

            MatchCollection pem = Regex.Matches(
                text,
                "-----BEGIN CERTIFICATE-----(.*?)-----END CERTIFICATE-----",
                RegexOptions.Singleline);

            if (pem.Count > 0)
            {
                foreach (Match match in pem)
                {
                    string base64 = Regex.Replace(
                        match.Groups[1].Value, @"\s+", "");

                    output.Add(
                        new X509Certificate2(Convert.FromBase64String(base64)));
                }

                return output;
            }

            // P7B/P7C: içindeki sertifikaların tamamını oku.
            if (ext == ".p7b" || ext == ".p7c")
            {
                var parser = new X509CertificateParser();
                var bcCertificates = parser.ReadCertificates(data);

                foreach (Org.BouncyCastle.X509.X509Certificate cert in bcCertificates)
                    output.Add(new X509Certificate2(cert.GetEncoded()));

                return output;
            }

            // CER/CRT/DER: tek DER sertifikası.
            output.Add(new X509Certificate2(data));
            return output;
        }

        private static bool IsCACertificate(X509Certificate2 cert)
        {
            var bc = new X509CertificateParser().ReadCertificate(cert.RawData);
            return bc.GetBasicConstraints() >= 0;
        }

        private static bool IsCryptographicallySelfSigned(X509Certificate2 cert)
        {
            if (!string.Equals(cert.Subject, cert.Issuer, StringComparison.OrdinalIgnoreCase)) return false;
            try
            {
                var bc = new X509CertificateParser().ReadCertificate(cert.RawData);
                bc.Verify(bc.GetPublicKey());
                return IsCACertificate(cert);
            }
            catch { return false; }
        }

        private static bool IsValidIssuer(X509Certificate2 child, X509Certificate2 parent)
        {
            if (!string.Equals(child.Issuer, parent.Subject, StringComparison.OrdinalIgnoreCase)) return false;
            if (!IsCACertificate(parent)) return false;
            try
            {
                var parser = new X509CertificateParser();
                var c = parser.ReadCertificate(child.RawData);
                var p = parser.ReadCertificate(parent.RawData);
                byte[] aki = GetKeyIdentifier(c, "2.5.29.35", true);
                byte[] ski = GetKeyIdentifier(p, "2.5.29.14", false);
                if (aki != null && ski != null && !aki.SequenceEqual(ski)) return false;
                if (aki != null && ski == null) return false;
                c.Verify(p.GetPublicKey());
                return true;
            }
            catch { return false; }
        }

        private static byte[] GetKeyIdentifier(Org.BouncyCastle.X509.X509Certificate cert,
            string oid, bool authority)
        {
            var extension = cert.GetExtensionValue(new DerObjectIdentifier(oid));
            if (extension == null) return null;
            Asn1Object parsed = Asn1Object.FromByteArray(extension.GetOctets());
            if (authority) return AuthorityKeyIdentifier.GetInstance(parsed).GetKeyIdentifier();
            return SubjectKeyIdentifier.GetInstance(parsed).GetKeyIdentifier();
        }

        private X509Certificate2 FindValidParent(SQLiteConnection c, X509Certificate2 child, out long id)
        {
            id = 0;
            using (var cmd = new SQLiteCommand("SELECT certificate_id, certificate_data FROM ca_certificates WHERE subject_dn=@s", c))
            {
                cmd.Parameters.AddWithValue("@s", child.Issuer);
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                    {
                        try
                        {
                            var candidate = new X509Certificate2(Convert.FromBase64String(r.GetString(1)));
                            if (IsValidIssuer(child, candidate)) { id = r.GetInt64(0); return candidate; }
                            candidate.Dispose();
                        }
                        catch { }
                    }
            }
            return null;
        }

        private bool HasDatabaseRootChain(SQLiteConnection c, X509Certificate2 cert,
            List<X509Certificate2> pending, X509Certificate2 importing)
        {
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            X509Certificate2 current = cert;
            while (current != null && seen.Add(GetSHA256Fingerprint(current)))
            {
                if (IsCryptographicallySelfSigned(current)) return true;
                long id;
                X509Certificate2 parent = FindValidParent(c, current, out id);
                if (parent != null) { current = parent; continue; }
                parent = pending.FirstOrDefault(p => !ReferenceEquals(p, importing)
                    && IsValidIssuer(current, p));
                if (parent == null) return false;
                current = parent;
            }
            return false;
        }

        private SQLiteConnection OpenConnection()
        {
            var c = new SQLiteConnection("Data Source=" + _databasePath + ";Version=3;");
            c.Open();
            return c;
        }

        private static long GetUserImportedSourceId(SQLiteConnection c)
        {
            using (var cmd = new SQLiteCommand("SELECT source_id FROM source_categories WHERE name=@n COLLATE NOCASE LIMIT 1", c))
            {
                cmd.Parameters.AddWithValue("@n", "User Imported");
                object value = cmd.ExecuteScalar();
                if (value == null || value == DBNull.Value)
                    throw new InvalidOperationException("The 'User Imported' row is missing from source_categories. Add it before importing.");
                return Convert.ToInt64(value);
            }
        }

        private static bool CertificateExists(SQLiteConnection c, string fingerprint)
        {
            using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM ca_certificates WHERE sha256_fingerprint=@f", c))
            {
                cmd.Parameters.AddWithValue("@f", fingerprint);
                return Convert.ToInt64(cmd.ExecuteScalar()) > 0;
            }
        }

        private static long InsertCertificate(SQLiteConnection c, SQLiteTransaction tx,
            X509Certificate2 cert, string type, long sourceId)
        {
            string now = DateTime.UtcNow.ToString("o");
            string name = cert.GetNameInfo(X509NameType.SimpleName, false);
            using (var cmd = new SQLiteCommand(@"INSERT INTO ca_certificates
(certificate_type,display_name,common_name,subject_dn,issuer_dn,serial_number,sha256_fingerprint,not_before,not_after,certificate_data,created_at,updated_at)
VALUES(@type,@name,@common,@subject,@issuer,@serial,@fingerprint,@before,@after,@data,@created,@updated)", c, tx))
            {
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@name", string.IsNullOrWhiteSpace(name) ? cert.Subject : name);
                cmd.Parameters.AddWithValue("@common", string.IsNullOrWhiteSpace(name) ? cert.Subject : name);
                cmd.Parameters.AddWithValue("@subject", cert.Subject);
                cmd.Parameters.AddWithValue("@issuer", cert.Issuer);
                cmd.Parameters.AddWithValue("@serial", cert.SerialNumber);
                cmd.Parameters.AddWithValue("@fingerprint", GetSHA256Fingerprint(cert));
                cmd.Parameters.AddWithValue("@before", cert.NotBefore.ToUniversalTime().ToString("o"));
                cmd.Parameters.AddWithValue("@after", cert.NotAfter.ToUniversalTime().ToString("o"));
                cmd.Parameters.AddWithValue("@data", Convert.ToBase64String(cert.RawData));
                cmd.Parameters.AddWithValue("@created", now);
                cmd.Parameters.AddWithValue("@updated", now);
                cmd.ExecuteNonQuery();
            }
            long id = c.LastInsertRowId;
            using (var cmd = new SQLiteCommand(@"INSERT INTO certificate_sources
(certificate_id,source_id,source_record_id,source_parent_record_id,source_metadata_json,added_at)
VALUES(@id,@source,NULL,NULL,@metadata,@at)", c, tx))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@source", sourceId);
                cmd.Parameters.AddWithValue("@metadata", "{\"source\":\"User Imported\"}");
                cmd.Parameters.AddWithValue("@at", now);
                cmd.ExecuteNonQuery();
            }
            return id;
        }

        private static void InsertIssuer(SQLiteConnection c, SQLiteTransaction tx,
            long childId, long parentId, long sourceId)
        {
            using (var cmd = new SQLiteCommand(@"INSERT INTO certificate_issuers
(certificate_id,issuer_certificate_id,source_id,relationship_type)
VALUES(@child,@parent,@source,'Issuer')", c, tx))
            {
                cmd.Parameters.AddWithValue("@child", childId);
                cmd.Parameters.AddWithValue("@parent", parentId);
                cmd.Parameters.AddWithValue("@source", sourceId);
                cmd.ExecuteNonQuery();
            }
        }

        private static long FindCertificateId(SQLiteConnection c, SQLiteTransaction tx, string fingerprint)
        {
            using (var cmd = new SQLiteCommand("SELECT certificate_id FROM ca_certificates WHERE sha256_fingerprint=@f", c, tx))
            {
                cmd.Parameters.AddWithValue("@f", fingerprint);
                object value = cmd.ExecuteScalar();
                if (value == null || value == DBNull.Value) throw new InvalidOperationException("Could not find the issuer certificate after insertion.");
                return Convert.ToInt64(value);
            }
        }

        private static string GetSHA256Fingerprint(X509Certificate2 cert)
        {
            using (SHA256 sha = SHA256.Create())
                return BitConverter.ToString(sha.ComputeHash(cert.RawData)).Replace("-", string.Empty);
        }

        private static void AddError(CAImportResult result, string message)
        {
            if (string.IsNullOrWhiteSpace(result.ErrorMessage)) result.ErrorMessage = message;
            else result.ErrorMessage += Environment.NewLine + message;
        }
    }
}
