using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using CertificateChecker.Models;

namespace CertificateChecker.Services
{
    public class SecurityAnalysisService
    {
        public List<SecurityCheckResult> Analyze(X509Certificate2 certificate)
        {
            var results = new List<SecurityCheckResult>();

            if (certificate == null)
            {
                Add(results, "Certificate", "CRITICAL", "No certificate was provided for analysis.");
                return results;
            }
            CheckCertificateVersion(certificate, results);
            CheckSerialNumber(certificate, results);
            CheckSubjectAndIssuer(certificate, results);
            CheckSignatureAlgorithm(certificate, results);
            CheckPublicKey(certificate, results);
            CheckValidity(certificate, results);
            CheckBasicConstraints(certificate, results);
            CheckKeyUsage(certificate, results);
            CheckExtendedKeyUsage(certificate, results);
            CheckSubjectAlternativeName(certificate, results);
            CheckCertificatePolicies(certificate, results);
            CheckCriticalExtensions(certificate, results);
            CheckDuplicateExtensions(certificate, results);
            return results;
        }

        private void CheckCertificateVersion(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            int version = certificate.Version;
            Add(results,"Certificate Version",version == 3 ? "OK" : "REVIEW",$"X.509 version: v{version}.");
        }
        private void CheckSerialNumber(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            byte[] serialBytes = certificate.GetSerialNumber();
            string serial = certificate.SerialNumber ?? string.Empty;
            if (serialBytes == null || serialBytes.Length == 0)
            {
                Add(results, "Serial Number", "CRITICAL", "Serial number is empty.");
                return;
            }
            bool allZero = serialBytes.All(b => b == 0);
            if (allZero)
            {
                Add(results, "Serial Number", "WARNING", "Serial number consists entirely of zero bytes.");
            }
            else
            {
                Add(results, "Serial Number", "INFO", $"Serial number: {serial}.");
            }
            Add(results,"Serial Number Length","INFO",$"Serial number representation: {serialBytes.Length} bytes.");
        }
        private void CheckSubjectAndIssuer(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            string subject = certificate.Subject ?? string.Empty;
            string issuer = certificate.Issuer ?? string.Empty;
            Add(results,"Subject",string.IsNullOrWhiteSpace(subject) ? "WARNING" : "INFO",string.IsNullOrWhiteSpace(subject) ? "Subject field is empty." : $"Subject: {subject}.");
            Add(results,"Issuer",string.IsNullOrWhiteSpace(issuer) ? "WARNING" : "INFO",string.IsNullOrWhiteSpace(issuer) ? "Issuer field is empty." : $"Issuer: {issuer}.");
            bool sameName = string.Equals(subject, issuer, StringComparison.OrdinalIgnoreCase);
            Add(results,"Subject / Issuer","INFO",sameName? "Subject and Issuer names are identical. This can indicate a self-issued certificate; it does not prove that the certificate is self-signed.": "Subject and Issuer names differ.");
        }
        private void CheckSignatureAlgorithm(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            string algorithm = certificate.SignatureAlgorithm?.FriendlyName ?? "Unknown";
            string oid = certificate.SignatureAlgorithm?.Value ?? string.Empty;
            if (oid == "1.2.840.113549.1.1.4" ||
                oid == "1.2.840.10040.4.3")
            {
                Add(results, "Signature Algorithm", "CRITICAL", $"Legacy weak signature algorithm detected: {algorithm} ({oid}).");
                return;
            }
            if (oid == "1.2.840.113549.1.1.5" ||
                oid == "1.2.840.10045.4.1")
            {
                Add(results, "Signature Algorithm", "WARNING", $"SHA-1 signature algorithm detected: {algorithm} ({oid}).");
                return;
            }
            if (oid == "1.2.840.113549.1.1.10" ||
                oid == "1.2.840.113549.1.1.11" ||
                oid == "1.2.840.113549.1.1.12" ||
                oid == "1.2.840.113549.1.1.13" ||
                oid == "1.2.840.10045.4.3.2" ||
                oid == "1.2.840.10045.4.3.3" ||
                oid == "1.2.840.10045.4.3.4")
            {
                Add(results, "Signature Algorithm", "OK", $"Recognized signature algorithm: {algorithm} ({oid}).");
                return;
            }
            Add(results, "Signature Algorithm", "REVIEW", $"Algorithm requires review: {algorithm} ({oid}).");
        }
        private void CheckPublicKey(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            using RSA rsa = certificate.GetRSAPublicKey();
            if (rsa != null)
            {
                int bits = rsa.KeySize;
                string keyStatus = bits < 2048 ? "CRITICAL" : "OK";
                Add(results,"Public Key Algorithm","INFO",$"RSA public key. Key size: {bits} bits.");
                Add(results,"RSA Key Size",keyStatus,bits < 2048? $"RSA key size is {bits} bits, below the commonly recommended 2048-bit minimum.": $"RSA key size is {bits} bits.");
                try
                {
                    RSAParameters parameters = rsa.ExportParameters(false);
                    if (parameters.Exponent == null || parameters.Exponent.Length == 0)
                    {
                        Add(results, "RSA Public Exponent", "REVIEW", "RSA public exponent could not be read.");
                    }
                    else
                    {
                        string exponentHex = Convert.ToHexString(parameters.Exponent);
                        string exponentDecimal = new System.Numerics.BigInteger(parameters.Exponent,isUnsigned: true,isBigEndian: true).ToString();
                        bool commonExponent = exponentDecimal == "65537";
                        Add(results,"RSA Public Exponent",commonExponent ? "OK" : "REVIEW",$"Public exponent: {exponentDecimal} (0x{exponentHex}). " +(commonExponent? "This is the commonly used RSA public exponent.": "Review the exponent against your cryptographic policy."));
                    }
                }
                catch (CryptographicException)
                {
                    Add(results, "RSA Public Exponent", "UNKNOWN", "RSA public parameters could not be exported by the cryptographic provider.");
                }
                return;
            }
            using ECDsa ecdsa = certificate.GetECDsaPublicKey();
            if (ecdsa != null)
            {
                int bits = ecdsa.KeySize;
                Add(results,"Public Key Algorithm","INFO",$"ECDSA public key. Key size: {bits} bits. Curve identity requires separate inspection.");
                Add(results,"ECDSA Key Size",bits < 256 ? "WARNING" : "INFO",$"ECDSA key size: {bits} bits. Evaluate the actual curve and applicable security policy.");
                return;
            }
            using DSA dsa = certificate.GetDSAPublicKey();
            if (dsa != null)
            {
                Add(results,"Public Key Algorithm","REVIEW",$"DSA public key detected. Key size: {dsa.KeySize} bits; assess against your security policy.");
                return;
            }
            Add(results,"Public Key Algorithm","REVIEW","Public key algorithm is unsupported or could not be identified by the available providers.");
        }
        private void CheckValidity(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            DateTime now = DateTime.Now;
            DateTime notBefore = certificate.NotBefore;
            DateTime notAfter = certificate.NotAfter;
            if (now < notBefore)
            {
                Add(results,"Validity","CRITICAL",$"Certificate is not yet valid. Starts: {notBefore:yyyy-MM-dd HH:mm:ss}.");
            }
            else if (now > notAfter)
            {
                Add(results,"Validity","CRITICAL",$"Certificate expired on {notAfter:yyyy-MM-dd HH:mm:ss}.");
            }
            else
            {
                Add(results,"Validity","OK",$"Certificate is currently valid. Expires: {notAfter:yyyy-MM-dd HH:mm:ss}.");
            }
            double totalDays = (notAfter - notBefore).TotalDays;
            double ageDays = (now - notBefore).TotalDays;
            double remainingDays = (notAfter - now).TotalDays;
            Add(results,"Certificate Lifetime","INFO",$"Total validity period: {Math.Ceiling(totalDays):N0} days.");
            Add(results,"Certificate Age","INFO",$"Age since validity start: {Math.Max(0, Math.Floor(ageDays)):N0} days.");
            if (remainingDays < 0)
            {
                Add(results,"Expiration","CRITICAL",$"Certificate expired {Math.Ceiling(Math.Abs(remainingDays)):N0} days ago.");
            }
            else if (remainingDays <= 14)
            {
                Add(results,"Expiration","WARNING",$"Certificate expires in {Math.Ceiling(remainingDays):N0} days.");
            }
            else
            {
                Add(results,"Expiration","INFO",$"Certificate expires in {Math.Ceiling(remainingDays):N0} days.");
            }
        }
        private void CheckBasicConstraints(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            foreach (X509Extension extension in certificate.Extensions)
            {
                if (extension is X509BasicConstraintsExtension basicConstraints)
                {
                    string pathLength = basicConstraints.HasPathLengthConstraint? basicConstraints.PathLengthConstraint.ToString(): "Not specified";
                    Add(results,"Basic Constraints","INFO",$"CA: {basicConstraints.CertificateAuthority}. Path length: {pathLength}.");
                    return;
                }
            }
            Add(results,"Basic Constraints","REVIEW","Basic Constraints extension is missing. Whether this is acceptable depends on the certificate profile.");
        }
        private void CheckKeyUsage(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            foreach (X509Extension extension in certificate.Extensions)
            {
                if (extension is X509KeyUsageExtension keyUsage)
                {
                    Add(results,"Key Usage","INFO",$"Key Usage flags: {keyUsage.KeyUsages}.");
                    return;
                }
            }
            Add(results,"Key Usage","REVIEW","Key Usage extension is missing. Requirements depend on the certificate's intended purpose.");
        }
        private void CheckExtendedKeyUsage(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            foreach (X509Extension extension in certificate.Extensions)
            {
                if (extension is X509EnhancedKeyUsageExtension eku)
                {
                    var usages = new List<string>();

                    foreach (Oid oid in eku.EnhancedKeyUsages)
                    {
                        usages.Add($"{oid.FriendlyName ?? "Unknown"} ({oid.Value})");
                    }

                    Add(results,"Extended Key Usage","INFO",usages.Count > 0 ? string.Join("; ", usages): "Extended Key Usage extension is empty.");
                    return;
                }
            }
            Add(results,"Extended Key Usage","REVIEW","Extended Key Usage extension is missing. Requirements depend on the certificate's intended purpose.");
        }
        private void CheckSubjectAlternativeName(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            foreach (X509Extension extension in certificate.Extensions)
            {
                if (extension.Oid?.Value == "2.5.29.17")
                {
                    Add(results,"Subject Alternative Name","INFO",$"SAN extension is present. Raw extension data length: {extension.RawData.Length} bytes. " +"Individual DNS, IP, email, and URI entries are not decoded by this check.");
                    return;
                }
            }
            Add(results,"Subject Alternative Name","REVIEW","SAN extension is missing. Check whether it is required for the certificate's intended purpose.");
        }
        private void CheckCertificatePolicies(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            foreach (X509Extension extension in certificate.Extensions)
            {
                if (extension.Oid?.Value == "2.5.29.32")
                {
                    Add(results,"Certificate Policies","INFO",$"Certificate Policies extension is present. Raw extension data length: {extension.RawData.Length} bytes. " +"Policy OIDs are not decoded by this check.");
                    return;
                }
            }
            Add(results,"Certificate Policies","INFO","Certificate Policies extension is not present.");
        }
        private void CheckCriticalExtensions(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            var criticalOids = new List<string>();
            foreach (X509Extension extension in certificate.Extensions)
            {
                if (extension.Critical)
                {
                    criticalOids.Add($"{extension.Oid?.FriendlyName ?? "Unknown"} ({extension.Oid?.Value ?? "Unknown OID"})");
                }
            }
            Add(results,"Critical Extensions","INFO",criticalOids.Count == 0? "No critical extensions are marked.": $"Critical extensions ({criticalOids.Count}): {string.Join("; ", criticalOids)}.");
        }

        private void CheckDuplicateExtensions(X509Certificate2 certificate, List<SecurityCheckResult> results)
        {
            var duplicateOids = certificate.Extensions.Cast<X509Extension>().Where(extension => !string.IsNullOrWhiteSpace(extension.Oid?.Value)).GroupBy(extension => extension.Oid!.Value).Where(group => group.Count() > 1).Select(group => group.Key).ToList();
            if (duplicateOids.Count == 0)
            {
                Add(results,"Duplicate Extensions","OK","No duplicate extension OIDs were detected.");
            }
            else
            {
                Add(results,"Duplicate Extensions","REVIEW",$"Repeated extension OIDs detected: {string.Join(", ", duplicateOids)}.");
            }
        }
        private void Add(List<SecurityCheckResult> results,string component,string status,string description)
        {
            results.Add(new SecurityCheckResult
            {
                Component = component,Status = status,Description = description
            });
        }
    }
}