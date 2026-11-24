using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CertificateChecker.Models;
using System.Net;
using System.IO;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Asn1.X509;

namespace CertificateChecker.Services
{
    public class AiaValidationService
    {
        public List<VerificationCheckResult> Validate(X509Certificate2 certificate,IEnumerable<string> aiaUrls)
        {
            var results = new List<VerificationCheckResult>();

            if (certificate == null)
            {
                results.Add(CreateResult("ERROR", "Certificate is null.", "-"));
                return results;
            }

            var urls = (aiaUrls ?? Enumerable.Empty<string>())
                .Where(url => !string.IsNullOrWhiteSpace(url))
                .Select(url => url.Trim())
                .Where(url => url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                           || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (urls.Count == 0)
            {
                results.Add(CreateResult(
                    "WARNING",
                    "No HTTP/HTTPS CA Issuers AIA URL found.",
                    "-"
                ));

                return results;
            }

            foreach (string url in urls)
            {
                try
                {
                    using (var issuerCertificate = DownloadIssuerCertificate(url))
                    {
                        bool valid = VerifyIssuer(certificate, issuerCertificate);

                        results.Add(CreateResult(
                            valid ? "OK" : "ERROR",
                            valid
                                ? "Issuer certificate downloaded and signature verified."
                                : "Downloaded certificate is not a valid issuer of the selected certificate.",
                            url
                        ));
                    }
                }
                catch (Exception ex)
                {
                    results.Add(CreateResult(
                        "ERROR",
                        "AIA issuer retrieval or validation failed: " + ex.Message,
                        url
                    ));
                }
            }

            return results;
        }
        private static byte[] GetAuthorityKeyIdentifier(
    Org.BouncyCastle.X509.X509Certificate certificate)
        {
            var extension = certificate.GetExtensionValue(
                X509Extensions.AuthorityKeyIdentifier
            );

            if (extension == null)
                return null;

            var aki = AuthorityKeyIdentifier.GetInstance(
                Org.BouncyCastle.Asn1.Asn1Object.FromByteArray(
                    extension.GetOctets()
                )
            );

            return aki.GetKeyIdentifier();
        }
        private static byte[] GetSubjectKeyIdentifier(
    Org.BouncyCastle.X509.X509Certificate certificate)
        {
            var extension = certificate.GetExtensionValue(
                X509Extensions.SubjectKeyIdentifier
            );

            if (extension == null)
                return null;

            var ski = SubjectKeyIdentifier.GetInstance(
                Org.BouncyCastle.Asn1.Asn1Object.FromByteArray(
                    extension.GetOctets()
                )
            );

            return ski.GetKeyIdentifier();
        }
        private static X509Certificate2 DownloadIssuerCertificate(string url)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", "CertificateChecker/1.0");
                client.Encoding = System.Text.Encoding.UTF8;

                byte[] data = client.DownloadData(url);

                var collection = new X509Certificate2Collection();

                try
                {
                    collection.Import(data);
                }
                catch
                {
                    // DER formatında tek sertifika olabilir.
                    return new X509Certificate2(data);
                }

                if (collection.Count == 0)
                    throw new InvalidDataException("No certificate found at AIA URL.");

                return new X509Certificate2(collection[0]);
            }
        }
        private static bool VerifyIssuer(
    X509Certificate2 certificate,
    X509Certificate2 issuerCertificate)
        {
            if (!string.Equals(
                certificate.Issuer,
                issuerCertificate.Subject,
                StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var parser = new X509CertificateParser();

            var childBc = parser.ReadCertificate(certificate.RawData);
            var issuerBc = parser.ReadCertificate(issuerCertificate.RawData);

            byte[] aki = GetAuthorityKeyIdentifier(childBc);
            byte[] ski = GetSubjectKeyIdentifier(issuerBc);

            if (aki != null && ski != null &&
                !aki.SequenceEqual(ski))
            {
                return false;
            }

            try
            {
                childBc.Verify(issuerBc.GetPublicKey());
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static VerificationCheckResult CreateResult(
            string status,
            string description,
            string source)
        {
            return new VerificationCheckResult
            {
                Component = "AIA",
                Status = status,
                Description = description,
                Source = source
            };
        }
    }
}