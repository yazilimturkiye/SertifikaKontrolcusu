using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CertificateChecker.Models;

namespace CertificateChecker.Services
{
    public class AiaValidationService
    {
        public List<VerificationCheckResult> Validate(
            X509Certificate2 certificate,
            IEnumerable<string> aiaUrls)
        {
            var results = new List<VerificationCheckResult>();

            if (certificate == null)
            {
                results.Add(CreateResult(
                    "ERROR",
                    "Certificate is null.",
                    "-"
                ));

                return results;
            }

            try
            {
                // AIA adreslerini temizle ve HTTP/HTTPS URL'lerini çıkar.
                var urls = (aiaUrls ?? Enumerable.Empty<string>())
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => line.Trim())
                    .Where(line =>
                        !line.Equals(
                            "No URI found",
                            StringComparison.OrdinalIgnoreCase) &&
                        !line.Equals(
                            "Not Found",
                            StringComparison.OrdinalIgnoreCase))
                    .Select(line =>
                    {
                        int httpIndex = line.IndexOf(
                            "http://",
                            StringComparison.OrdinalIgnoreCase
                        );

                        int httpsIndex = line.IndexOf(
                            "https://",
                            StringComparison.OrdinalIgnoreCase
                        );

                        int index;

                        if (httpIndex >= 0 && httpsIndex >= 0)
                        {
                            index = Math.Min(httpIndex, httpsIndex);
                        }
                        else
                        {
                            index = httpIndex >= 0
                                ? httpIndex
                                : httpsIndex;
                        }

                        return index >= 0
                            ? line.Substring(index).Trim()
                            : null;
                    })
                    .Where(url => !string.IsNullOrWhiteSpace(url))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                // Eski metottaki Windows sertifika zinciri kontrolü.
                bool windowsTrusted;

                using (var chain = new X509Chain())
                {
                    chain.ChainPolicy.RevocationMode =
                        X509RevocationMode.NoCheck;

                    chain.ChainPolicy.VerificationFlags =
                        X509VerificationFlags.NoFlag;

                    chain.ChainPolicy.UrlRetrievalTimeout =
                        TimeSpan.FromSeconds(10);

                    windowsTrusted = chain.Build(certificate);
                }

                // AIA adresi varsa her adres için ayrı sonuç üret.
                if (urls.Count > 0)
                {
                    foreach (string url in urls)
                    {
                        if (windowsTrusted)
                        {
                            results.Add(CreateResult(
                                "OK",
                                "Issuer trusted (Windows policy).",
                                url
                            ));
                        }
                        else
                        {
                            results.Add(CreateResult(
                                "ERROR",
                                "Issuer not trusted.",
                                url
                            ));
                        }
                    }
                }
                else
                {
                    // AIA adresi bulunamadıysa Windows sonucunu ayrı raporla.
                    if (windowsTrusted)
                    {
                        results.Add(CreateResult(
                            "WARNING",
                            "No AIA URL found. Windows certificate chain validation succeeded.",
                            "-"
                        ));
                    }
                    else
                    {
                        results.Add(CreateResult(
                            "ERROR",
                            "AIA not available and issuer is not trusted.",
                            "-"
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                results.Add(CreateResult(
                    "ERROR",
                    "AIA validation error: " + ex.Message,
                    "-"
                ));
            }

            return results;
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