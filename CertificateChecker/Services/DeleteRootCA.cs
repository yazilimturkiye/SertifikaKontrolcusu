
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace CertificateChecker.Services
{
    public class DeleteRootCA
    {
        private readonly string _databasePath;

        public DeleteRootCA()
        {
            _databasePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data",
                "CertificateAuthority.db");
        }

        public bool DeleteCertificate(int certificateId, out int deletedCount, out string errorMessage)
        {
            deletedCount = 0;
            errorMessage = string.Empty;

            if (!File.Exists(_databasePath))
            {
                errorMessage = "CA database could not be found.";
                return false;
            }
            try
            {
                using (var connection = new SQLiteConnection(
                    "Data Source=" + _databasePath + ";Version=3;"))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var descendants = new HashSet<long>();
                            var pending = new Stack<long>();
                            pending.Push(certificateId);
                            while (pending.Count > 0)
                            {
                                long currentId = pending.Pop();
                                if (!descendants.Add(currentId))
                                    continue;
                                using (var command = new SQLiteCommand(@"SELECT certificate_id FROM certificate_issuers WHERE issuer_certificate_id = @id",connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@id", currentId);
                                    using (var reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                            pending.Push(Convert.ToInt64(reader[0]));
                                    }
                                }
                            }
                            using (var command = new SQLiteCommand(@"SELECT COUNT(*) FROM ca_certificates WHERE certificate_id = @id",connection, transaction))
                            {
                                command.Parameters.AddWithValue("@id", certificateId);
                                if (Convert.ToInt32(command.ExecuteScalar()) == 0)
                                    throw new InvalidOperationException(
                                        "The selected certificate was not found.");
                            }
                            foreach (long id in descendants)
                            {
                                using (var command = new SQLiteCommand("DELETE FROM certificate_issuers WHERE certificate_id = @id OR issuer_certificate_id = @id", connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@id", id);
                                    command.ExecuteNonQuery();
                                }
                                using (var command = new SQLiteCommand("DELETE FROM certificate_sources WHERE certificate_id = @id",connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@id", id);
                                    command.ExecuteNonQuery();
                                }
                                using (var command = new SQLiteCommand("DELETE FROM ca_certificates WHERE certificate_id = @id",connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@id", id);
                                    deletedCount += command.ExecuteNonQuery();
                                }
                            }
                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                deletedCount = 0;
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
