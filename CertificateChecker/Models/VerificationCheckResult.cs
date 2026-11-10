namespace CertificateChecker.Models
{
    public class VerificationCheckResult
    {
        public string Component { get; set; } = string.Empty;
        public string Status { get; set; } = "UNKNOWN";
        public string Description { get; set; } = string.Empty;
        public string Source { get; set; } = "-";
    }
}