
namespace CertificateChecker.Models
{
    public class CADeleteResult
    {
        public bool Success { get; set; }

        public int DeletedCount { get; set; }

        public string Message { get; set; }

        public string ErrorMessage { get; set; }

        public CADeleteResult()
        {
            Success = false;
            DeletedCount = 0;
            Message = string.Empty;
            ErrorMessage = string.Empty;
        }
    }
}
