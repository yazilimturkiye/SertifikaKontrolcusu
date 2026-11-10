using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;

using CertificateChecker.Models;

namespace CertificateChecker.Services
{
    public class CertificateParsingService
    {
        public CertificateDetails Parse(X509Certificate2 cert)
        {
            if (cert == null)
                throw new ArgumentNullException(nameof(cert));

            var details = new CertificateDetails
            {
                Certificate = cert,
                Subject = cert.Subject ?? "",
                Issuer = cert.Issuer ?? "",
                SerialNumber = cert.SerialNumber ?? "",
                NotBefore = cert.NotBefore,
                NotAfter = cert.NotAfter,
                SignatureAlgorithm = cert.SignatureAlgorithm?.FriendlyName ?? cert.SignatureAlgorithm?.Value ?? "Not Found",
                Thumbprint = cert.Thumbprint ?? ""
            };
            details.AuthorityKeyIdentifier = SafeGet(() => GetAuthorityKeyIdentifier(cert));
            details.SubjectKeyIdentifier = SafeGet(() => GetSubjectKeyIdentifier(cert));
            details.KeyUsage = SafeGet(() => GetKeyUsage(cert));
            details.BasicConstraints = SafeGet(() => GetBasicConstraints(cert));
            details.PublicKeyInfo = SafeGet(() => GetKeySize(cert));
            details.CertificatePolicy = SafeGet(() => GetCertificatePolicy(cert));
            details.CrlText = ParseCRLAddresses(cert, details.CrlUrls);
            details.AiaText = ParseAIAAddresses(cert, details.AiaUrls);
            details.OcspText = ParseOCSPAddresses(cert, details.OcspUrls);
            details.EkuText = ParseEKU(cert, details.ExtendedKeyUsages);
            details.SanText = ParseSAN(cert, details.SubjectAlternativeNames);
            return details;
        }
        private string SafeGet(Func<string> getter)
        {
            try
            {
                return getter();
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        private string GetBasicConstraints(X509Certificate2 cert)
        {
            foreach (var extension in cert.Extensions)
            {
                if (extension is X509BasicConstraintsExtension constraints)
                {
                    return
                        $"CA: {constraints.CertificateAuthority}, " +
                        $"PathLength: " +
                        $"{(constraints.HasPathLengthConstraint ? constraints.PathLengthConstraint.ToString() : "None")}";
                }
            }
            return "No constraints found in certificate.";
        }

        private string GetKeyUsage(X509Certificate2 cert)
        {
            foreach (var extension in cert.Extensions)
            {
                if (extension is X509KeyUsageExtension keyUsage)
                {
                    var result = new List<string>();
                    var flags = keyUsage.KeyUsages;

                    if (flags.HasFlag(X509KeyUsageFlags.DigitalSignature))
                        result.Add("Digital Signature");
                    if (flags.HasFlag(X509KeyUsageFlags.NonRepudiation))
                        result.Add("Non Repudiation");
                    if (flags.HasFlag(X509KeyUsageFlags.KeyEncipherment))
                        result.Add("Key Encipherment");
                    if (flags.HasFlag(X509KeyUsageFlags.DataEncipherment))
                        result.Add("Data Encipherment");
                    if (flags.HasFlag(X509KeyUsageFlags.KeyAgreement))
                        result.Add("Key Agreement");
                    if (flags.HasFlag(X509KeyUsageFlags.KeyCertSign))
                        result.Add("Certificate Signing");
                    if (flags.HasFlag(X509KeyUsageFlags.CrlSign))
                        result.Add("CRL Signing");
                    if (flags.HasFlag(X509KeyUsageFlags.EncipherOnly))
                        result.Add("Encipher Only");
                    if (flags.HasFlag(X509KeyUsageFlags.DecipherOnly))
                        result.Add("Decipher Only");
                    return result.Count > 0
                        ? string.Join(", ", result)
                        : "Not Found";
                }
            }
            return "Not Found";
        }
        private string GetKeySize(X509Certificate2 cert)
        {
            using (RSA rsa = cert.GetRSAPublicKey())
            {
                if (rsa != null)
                    return $"RSA{rsa.KeySize}";
            }
            using (ECDsa ec = cert.GetECDsaPublicKey())
            {
                if (ec != null)
                    return $"ECC{ec.KeySize}";
            }
            return "Not Found";
        }

        private string GetSubjectKeyIdentifier(X509Certificate2 cert)
        {
            foreach (System.Security.Cryptography.X509Certificates.X509Extension extension in cert.Extensions)
            {
                if (extension.Oid?.Value == "2.5.29.14")
                {
                    if (extension is X509SubjectKeyIdentifierExtension ski)
                        return ski.SubjectKeyIdentifier;

                    var parsedExtension =
                        new X509SubjectKeyIdentifierExtension(
                            extension.RawData,
                            extension.Critical);

                    return parsedExtension.SubjectKeyIdentifier;
                }
            }
            return "Not Found";
        }
        private string GetAuthorityKeyIdentifier(X509Certificate2 cert)
        {
            foreach (System.Security.Cryptography.X509Certificates.X509Extension extension in cert.Extensions)
            {
                if (extension.Oid?.Value == "2.5.29.35")
                {
                    // Mevcut uygulamadaki gösterim biçimi korunuyor.
                    return BitConverter
                        .ToString(extension.RawData)
                        .Replace("-", "");
                }
            }
            return "Not Found";
        }
        private string GetCertificatePolicy(X509Certificate2 cert)
        {
            foreach (System.Security.Cryptography.X509Certificates.X509Extension extension in cert.Extensions)
            {
                if (extension.Oid?.Value == "2.5.29.32")
                {
                    var encodedData = new AsnEncodedData(
                        extension.Oid,
                        extension.RawData);

                    string policyText = encodedData.Format(true);

                    return string.IsNullOrWhiteSpace(policyText)
                        ? "Not Found"
                        : policyText;
                }
            }
            return "Not Found";
        }
        private string ParseCRLAddresses(X509Certificate2 cert,List<string> urls)
        {
            try
            {
                var bcCert = new X509CertificateParser().ReadCertificate(cert.RawData);
                var crlExtension = bcCert.GetExtensionValue(X509Extensions.CrlDistributionPoints);
                if (crlExtension == null)
                    return "Not Found";
                var asn1 = Asn1Object.FromByteArray(crlExtension.GetOctets());
                var crlDistributionPoints = CrlDistPoint.GetInstance(asn1);
                foreach (DistributionPoint point
                    in crlDistributionPoints.GetDistributionPoints())
                {
                    var pointName = point.DistributionPointName;
                    if (pointName?.Type != DistributionPointName.FullName)
                        continue;
                    var generalNames = GeneralNames.GetInstance(pointName.Name);
                    foreach (GeneralName name in generalNames.GetNames())
                    {
                        if (name.TagNo != GeneralName.UniformResourceIdentifier)
                            continue;
                        string uri = DerIA5String.GetInstance(name.Name).GetString();
                        urls.Add(uri);
                    }
                }
                return urls.Count > 0
                    ? string.Join(Environment.NewLine, urls)
                    : "No URI found";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        private string ParseAIAAddresses(X509Certificate2 cert,List<string> urls)
        {
            try
            {
                var bcCert = new X509CertificateParser().ReadCertificate(cert.RawData);

                var aiaExtension = bcCert.GetExtensionValue( X509Extensions.AuthorityInfoAccess);
                if (aiaExtension == null)
                    return "Not Found";
                var aiaAsn1 = Asn1Object.FromByteArray(aiaExtension.GetOctets());
                var aia = AuthorityInformationAccess.GetInstance(aiaAsn1);
                foreach (AccessDescription description
                    in aia.GetAccessDescriptions())
                {
                    if (!description.AccessMethod.Equals(
                            AccessDescription.IdADCAIssuers))
                        continue;

                    if (description.AccessLocation.TagNo
                        != GeneralName.UniformResourceIdentifier)
                        continue;
                    string uri = DerIA5String.GetInstance(description.AccessLocation.Name).GetString();
                    urls.Add(uri);
                }
                return urls.Count > 0
                    ? string.Join(Environment.NewLine, urls)
                    : "No CA Issuers URI found";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        private string ParseOCSPAddresses(X509Certificate2 cert,List<string> urls)
        {
            try
            {
                var bcCert = new X509CertificateParser().ReadCertificate(cert.RawData);
                var aiaExtension = bcCert.GetExtensionValue(X509Extensions.AuthorityInfoAccess);
                if (aiaExtension == null)
                    return "Not Found";
                var aiaAsn1 = Asn1Object.FromByteArray(aiaExtension.GetOctets());
                var aia = AuthorityInformationAccess.GetInstance(aiaAsn1);
                foreach (AccessDescription description
                    in aia.GetAccessDescriptions())
                {
                    if (!description.AccessMethod.Equals(
                            AccessDescription.IdADOcsp))
                        continue;
                    if (description.AccessLocation.TagNo
                        != GeneralName.UniformResourceIdentifier)
                        continue;
                    string uri = DerIA5String.GetInstance(description.AccessLocation.Name).GetString();
                    urls.Add(uri);
                }
                return urls.Count > 0
                    ? string.Join(Environment.NewLine, urls)
                    : "No OCSP URI found";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        private string ParseEKU(X509Certificate2 cert,List<string> usages)
        {
            try
            {
                var bcCert =new X509CertificateParser().ReadCertificate(cert.RawData);
                var ekuList = bcCert.GetExtendedKeyUsage();
                if (ekuList == null || ekuList.Count == 0)
                    return "Not Found";
                foreach (DerObjectIdentifier oid in ekuList)
                {
                    string friendlyName = GetEkuFriendlyName(oid.Id);
                    usages.Add($"{friendlyName} ({oid.Id})");
                }
                return usages.Count > 0
                    ? string.Join(Environment.NewLine, usages)
                    : "Not Found";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        private string GetEkuFriendlyName(string oid)
        {
            switch (oid)
            {
                case "1.3.6.1.5.5.7.3.1":
                    return "Server Authentication";
                case "1.3.6.1.5.5.7.3.2":
                    return "Client Authentication";
                case "1.3.6.1.5.5.7.3.3":
                    return "Code Signing";
                case "1.3.6.1.5.5.7.3.4":
                    return "Email Protection";
                case "1.3.6.1.5.5.7.3.8":
                    return "Time Stamping";
                case "1.3.6.1.5.5.7.3.9":
                    return "OCSP Signing";
                case "2.5.29.37.0":
                    return "Any Extended Key Usage";
                default:
                    return "Unknown Usage";
            }
        }
        private string ParseSAN(X509Certificate2 cert,List<string> sanEntries)
        {
            try
            {
                var bcCert = new X509CertificateParser().ReadCertificate(cert.RawData);
                var sanList = bcCert.GetSubjectAlternativeNames();
                if (sanList == null || sanList.Count == 0)
                    return "Not Found";
                foreach (var rawEntry in sanList)
                {
                    if (!(rawEntry is IList<object> entry)
                        || entry.Count < 2)
                        continue;
                    int tag = Convert.ToInt32(entry[0]);
                    string value = entry[1]?.ToString();
                    if ((tag == 2 || tag == 7)
                        && !string.IsNullOrWhiteSpace(value))
                    {
                        sanEntries.Add(value);
                    }
                }
                return sanEntries.Count > 0
                    ? string.Join(Environment.NewLine, sanEntries)
                    : "Not Found";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}