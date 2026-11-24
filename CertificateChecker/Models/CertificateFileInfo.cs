using System;
using System.Collections.Generic;
using System.Text;

namespace CertificateChecker.Models
{
    public class CertificateFileInfo
    {
        public string FileName { get; set; }

        public string CommonName { get; set; }

        public string FileExtension { get; set; }

        public byte[] FileData { get; set; }

        public string Description { get; set; }
        public string Subject { get; set; }
    }
}