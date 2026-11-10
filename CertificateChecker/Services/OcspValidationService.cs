using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using CertificateChecker.Models;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;

namespace CertificateChecker.Services
{
    public class OcspValidationService
    {
        public List<VerificationCheckResult> Validate(
            X509Certificate2 certificate,
            IEnumerable<string> ocspUrls)
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
                // OCSP adreslerini temizle.
                var urls = (ocspUrls ?? Enumerable.Empty<string>())
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

                bool anyResponderReached = false;

                // Sertifikanın ve issuer'ın BouncyCastle karşılıklarını hazırla.
                var bcParser = new X509CertificateParser();

                var subjectCertificate =
                    bcParser.ReadCertificate(certificate.RawData);

                Org.BouncyCastle.X509.X509Certificate issuerCertificate = null;

                try
                {
                    using (var chain = new X509Chain())
                    {
                        chain.ChainPolicy.RevocationMode =
                            X509RevocationMode.NoCheck;

                        chain.Build(certificate);

                        if (chain.ChainElements.Count > 1)
                        {
                            issuerCertificate = bcParser.ReadCertificate(
                                chain.ChainElements[1].Certificate.RawData
                            );
                        }
                    }
                }
                catch
                {
                    // Issuer bulunamazsa aşağıdaki fallback değerlendirmesi yapılır.
                }

                // Issuer ve OCSP adresleri mevcutsa doğrudan OCSP sorgusu yap.
                if (issuerCertificate != null && urls.Count > 0)
                {
                    foreach (string url in urls)
                    {
                        try
                        {
                            var certId = new CertificateID(
                                CertificateID.HashSha1,
                                issuerCertificate,
                                subjectCertificate.SerialNumber
                            );

                            var requestGenerator = new OcspReqGenerator();

                            requestGenerator.AddRequest(certId);

                            var request = requestGenerator.Generate();

                            byte[] responseBytes;

                            using (var client = new TimeoutWebClient())
                            {
                                client.Headers.Add(
                                    "Content-Type",
                                    "application/ocsp-request"
                                );

                                client.Headers.Add(
                                    "Accept",
                                    "application/ocsp-response"
                                );

                                client.Headers.Add(
                                    "User-Agent",
                                    "CertificateChecker/1.0"
                                );

                                responseBytes = client.UploadData(
                                    url,
                                    request.GetEncoded()
                                );
                            }

                            anyResponderReached = true;

                            var ocspResponse = new OcspResp(responseBytes);

                            // OCSP responder hata döndürmüş olabilir.
                            if (ocspResponse.Status != 0)
                                continue;

                            var basicResponse =
                                ocspResponse.GetResponseObject() as BasicOcspResp;

                            if (basicResponse == null)
                                continue;

                            // Gelen yanıtın gerçekten bu sertifikaya ait
                            // sorguyu içerdiğini kontrol et.
                            var singleResponse = basicResponse.Responses
                                .FirstOrDefault(response =>
                                    response.GetCertID().Equals(certId));

                            if (singleResponse == null)
                                continue;

                            var certificateStatus =
                                singleResponse.GetCertStatus();

                            // Sertifikanın iptal edilmediği bildiriliyor.
                            if (certificateStatus == CertificateStatus.Good)
                            {
                                results.Add(CreateResult(
                                    "OK",
                                    "OCSP responder returned GOOD.",
                                    url
                                ));

                                return results;
                            }

                            // Sertifikanın iptal edildiği bildiriliyor.
                            if (certificateStatus is RevokedStatus)
                            {
                                results.Add(CreateResult(
                                    "ERROR",
                                    "OCSP responder returned REVOKED.",
                                    url
                                ));

                                return results;
                            }

                            // UNKNOWN, REVOKED anlamına gelmez.
                            if (certificateStatus is UnknownStatus)
                            {
                                // Diğer responder adreslerini de dene.
                                continue;
                            }
                        }
                        catch
                        {
                            // Bir adres başarısız olursa sıradakini dene.
                            continue;
                        }
                    }
                }

                // Doğrudan OCSP'den kesin bir sonuç alınamadı.
                // Eski metottaki gibi Windows online zincir kontrolüne geç.
                bool windowsOk = false;

                try
                {
                    using (var windowsChain = new X509Chain())
                    {
                        windowsChain.ChainPolicy.RevocationMode =
                            X509RevocationMode.Online;

                        windowsChain.ChainPolicy.RevocationFlag =
                            X509RevocationFlag.EndCertificateOnly;

                        windowsChain.ChainPolicy.UrlRetrievalTimeout =
                            TimeSpan.FromSeconds(10);

                        windowsOk = windowsChain.Build(certificate);
                    }
                }
                catch
                {
                    windowsOk = false;
                }

                string fallbackStatus = windowsOk
                    ? "WARNING"
                    : "ERROR";

                string fallbackDescription;

                if (urls.Count == 0)
                {
                    fallbackDescription = windowsOk
                        ? "No OCSP URL found. Windows online chain check succeeded; direct OCSP status was not verified."
                        : "No OCSP URL found and Windows online chain check failed.";
                }
                else if (issuerCertificate == null)
                {
                    fallbackDescription = windowsOk
                        ? "Issuer certificate could not be found. Windows online chain check succeeded; direct OCSP status was not verified."
                        : "Issuer certificate could not be found and Windows online chain check failed.";
                }
                else if (anyResponderReached)
                {
                    fallbackDescription = windowsOk
                        ? "No definitive OCSP result. Windows online chain check succeeded; direct OCSP status was not verified."
                        : "No definitive OCSP result and Windows online chain check failed.";
                }
                else
                {
                    fallbackDescription = windowsOk
                        ? "OCSP responder unreachable. Windows online chain check succeeded; direct OCSP status was not verified."
                        : "OCSP responder unreachable and Windows online chain check failed.";
                }

                if (urls.Count == 0)
                {
                    results.Add(CreateResult(
                        fallbackStatus,
                        fallbackDescription,
                        "-"
                    ));
                }
                else
                {
                    foreach (string url in urls)
                    {
                        results.Add(CreateResult(
                            fallbackStatus,
                            fallbackDescription,
                            url
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                results.Add(CreateResult(
                    "ERROR",
                    "OCSP validation error: " + ex.Message,
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
                Component = "OCSP",
                Status = status,
                Description = description,
                Source = source
            };
        }

        // WebClient için indirme/yükleme zaman aşımı desteği.
        private sealed class TimeoutWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);

                request.Timeout = 10000;

                if (request is HttpWebRequest httpRequest)
                {
                    httpRequest.ReadWriteTimeout = 10000;
                }

                return request;
            }
        }
    }
}