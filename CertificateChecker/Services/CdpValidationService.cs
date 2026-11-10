using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using CertificateChecker.Models;
using Org.BouncyCastle.X509;

namespace CertificateChecker.Services
{
    public class CdpValidationService
    {
        public VerificationCheckResult Validate(
            X509Certificate2 certificate,
            IEnumerable<string> cdpUrls)
        {
            if (certificate == null)
            {
                return CreateResult(
                    "ERROR",
                    "Certificate is null.",
                    "-"
                );
            }

            try
            {
                var urls = (cdpUrls ?? Enumerable.Empty<string>())
                    .Where(url => !string.IsNullOrWhiteSpace(url))
                    .Select(url => url.Trim())
                    .Where(url =>
                        !url.Equals("No URI found", StringComparison.OrdinalIgnoreCase) &&
                        !url.Equals("Not Found", StringComparison.OrdinalIgnoreCase) &&
                        !url.StartsWith("ldap://", StringComparison.OrdinalIgnoreCase))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (urls.Count == 0)
                {
                    return CreateResult(
                        "WARNING",
                        "CDP not available.",
                        "-"
                    );
                }

                bool anyCrlDownloaded = false;
                var crlParser = new X509CrlParser();
                var certificateParser = new X509CertificateParser();

                foreach (string url in urls)
                {
                    try
                    {
                        bool isHttp =
                            url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                            url.StartsWith("https://", StringComparison.OrdinalIgnoreCase);

                        bool isFile =
                            url.StartsWith("file://", StringComparison.OrdinalIgnoreCase);

                        if (!isHttp && !isFile)
                            continue;

                        byte[] crlBytes;

                        if (isFile)
                        {
                            crlBytes = File.ReadAllBytes(new Uri(url).LocalPath);
                        }
                        else
                        {
                            using (var client = new WebClient())
                            {
                                client.Headers.Add(
                                    "User-Agent",
                                    "CertificateChecker/1.0"
                                );
                                crlBytes = client.DownloadData(url);
                            }
                        }

                        if (crlBytes == null || crlBytes.Length == 0)
                            continue;

                        anyCrlDownloaded = true;

                        var crl = crlParser.ReadCrl(crlBytes);

                        // CRL imzalayan sertifikayı zincirde ara.
                        Org.BouncyCastle.X509.X509Certificate issuerCertificate = null;

                        using (var chain = new X509Chain())
                        {
                            chain.ChainPolicy.RevocationMode =
                                X509RevocationMode.NoCheck;

                            chain.ChainPolicy.VerificationFlags =
                                X509VerificationFlags.AllowUnknownCertificateAuthority;

                            chain.Build(certificate);

                            foreach (X509ChainElement element in chain.ChainElements)
                            {
                                var candidate = certificateParser.ReadCertificate(
                                    element.Certificate.RawData
                                );

                                if (candidate.SubjectDN.Equivalent(crl.IssuerDN))
                                {
                                    issuerCertificate = candidate;
                                    break;
                                }
                            }
                        }

                        if (issuerCertificate == null)
                        {
                            continue;
                        }

                        // CRL imzasını doğrula.
                        try
                        {
                            crl.Verify(issuerCertificate.GetPublicKey());
                        }
                        catch
                        {
                            return CreateResult(
                                "ERROR",
                                "CRL signature is invalid.",
                                url
                            );
                        }

                        // CRL'nin geçerlilik tarihlerini kontrol et.
                        DateTime now = DateTime.UtcNow;

                        if (crl.NextUpdate.HasValue &&
                            crl.NextUpdate.Value.ToUniversalTime() < now)
                        {
                            return CreateResult(
                                "WARNING",
                                "CRL has expired.",
                                url
                            );
                        }

                        if (crl.ThisUpdate.ToUniversalTime() > now)
                        {
                            return CreateResult(
                                "WARNING",
                                "CRL ThisUpdate is in the future.",
                                url
                            );
                        }

                        // Sertifika seri numarası CRL içinde mi?
                        var serialNumber =
                            new Org.BouncyCastle.Math.BigInteger(
                                certificate.SerialNumber,
                                16
                            );

                        var revokedCertificate =
                            crl.GetRevokedCertificate(serialNumber);

                        if (revokedCertificate != null)
                        {
                            return CreateResult(
                                "ERROR",
                                "Certificate revoked.",
                                url
                            );
                        }

                        return CreateResult(
                            "OK",
                            "CRL signature and certificate status verified.",
                            url
                        );
                    }
                    catch
                    {
                        // Bir CDP adresi başarısız olursa diğerlerini dene.
                        continue;
                    }
                }

                if (!anyCrlDownloaded)
                {
                    return CreateResult(
                        "WARNING",
                        "CRL could not be downloaded from the available CDP addresses.",
                        "-"
                    );
                }

                return CreateResult(
                    "WARNING",
                    "A CRL was downloaded, but its issuer could not be verified.",
                    "-"
                );
            }
            catch (Exception ex)
            {
                return CreateResult(
                    "ERROR",
                    "Unexpected CRL validation error: " + ex.Message,
                    "-"
                );
            }
        }

        private static VerificationCheckResult CreateResult(
            string status,
            string description,
            string source)
        {
            return new VerificationCheckResult
            {
                Component = "CDP",
                Status = status,
                Description = description,
                Source = source
            };
        }
    }
}