using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CertificateChecker.Models
{
    public class CertificateDetails
    {
        public X509Certificate2 Certificate { get; set; }

        public string Subject { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;

        public string AuthorityKeyIdentifier { get; set; } = string.Empty;
        public string SubjectKeyIdentifier { get; set; } = string.Empty;

        public string SerialNumber { get; set; } = string.Empty;
        public string KeyUsage { get; set; } = string.Empty;
        public string BasicConstraints { get; set; } = string.Empty;

        public DateTime NotBefore { get; set; }
        public DateTime NotAfter { get; set; }

        public string SignatureAlgorithm { get; set; } = string.Empty;
        public string PublicKeyInfo { get; set; } = string.Empty;
        public string Thumbprint { get; set; } = string.Empty;
        public string CertificatePolicy { get; set; } = string.Empty;

        public List<string> CrlUrls { get; set; } = new List<string>();
        public List<string> AiaUrls { get; set; } = new List<string>();
        public List<string> OcspUrls { get; set; } = new List<string>();

        public List<string> ExtendedKeyUsages { get; set; } = new List<string>();
        public List<string> SubjectAlternativeNames { get; set; } = new List<string>();

        public string CrlText { get; set; } = "Not Found";
        public string AiaText { get; set; } = "Not Found";
        public string OcspText { get; set; } = "Not Found";
        public string EkuText { get; set; } = "Not Found";
        public string SanText { get; set; } = "Not Found";
    }
}