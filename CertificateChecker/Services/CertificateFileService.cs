using System;
using System.IO;
using System.Windows.Forms;
using CertificateChecker.Models;

namespace CertificateChecker.Services
{
    public class CertificateFileService
    {
        public bool SaveFile(CertificateFileInfo fileInfo)
        {
            if (fileInfo == null ||
                fileInfo.FileData == null ||
                fileInfo.FileData.Length == 0)
            {
                MessageBox.Show(
                    "No file available to save.",
                    "Certificate Checker",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.FileName = fileInfo.FileName;
                dialog.DefaultExt = fileInfo.FileExtension;
                dialog.AddExtension = true;

                dialog.Filter =
                    fileInfo.FileExtension.ToUpper() +
                    " Files (*" + fileInfo.FileExtension +
                    ")|*" + fileInfo.FileExtension +
                    "|All Files (*.*)|*.*";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return false;

                File.WriteAllBytes(
                    dialog.FileName,
                    fileInfo.FileData
                );

                MessageBox.Show(
                    "File saved successfully.",
                    "Certificate Checker",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return true;
            }
        }
    }
}
