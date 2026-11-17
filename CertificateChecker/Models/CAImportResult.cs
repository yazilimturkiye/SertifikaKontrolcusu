using System;
using System.Collections.Generic;
using System.Text;

namespace CertificateChecker.Models
{
    public class CAImportResult
    {
        public bool Success { get; set; }

        public int ImportedCount { get; set; }

        public int DuplicateCount { get; set; }

        public int RejectedCount { get; set; }

        public int MissingRootCount { get; set; }

        public string Message { get; set; }

        public string ErrorMessage { get; set; }

        public CAImportResult()
        {
            Success = false;
            ImportedCount = 0;
            DuplicateCount = 0;
            RejectedCount = 0;
            MissingRootCount = 0;
            Message = string.Empty;
            ErrorMessage = string.Empty;
        }
    }
}
