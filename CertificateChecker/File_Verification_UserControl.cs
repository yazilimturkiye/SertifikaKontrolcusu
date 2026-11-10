using System; //yazilimturkiye.com 03.07.2021 Tüm hakları saklıdır.
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using CertificateChecker.Models;
using CertificateChecker.Services;

namespace CertificateChecker
{
    public partial class File_Verification_UserControl : UserControl
    {
        public File_Verification_UserControl()
        {
            InitializeComponent();
            SetupToolTips();
            this.Load += (s, e) =>
            {
                (this.ParentForm as Form1)?.SetPageTitle("Verification > File Verification");
            };
        }
        private string File_Path;
        private string File_Name;
        private X509Certificate2 CurrentCertificate;
        private void SetupToolTips()
        {
            toolTip1.SetToolTip(Textbox_DosyaAdi, "Displays the selected certificate file name.");
            toolTip1.SetToolTip(Textbox_DosyaYolu, "Displays the selected certificate file path.");
            toolTip1.SetToolTip(Buton_Dosya_Sec, "Browse and select a certificate file.");
            toolTip1.SetToolTip(Button_CPS, "Open the Certificate Practice Statement (CPS).");
            toolTip1.SetToolTip(Button_Terminal, "Verify the certificate using Windows Terminal (certutil).");
            toolTip1.SetToolTip(Buton_Goruntule, "Open the selected certificate.");
            toolTip1.SetToolTip(Button_RetrivalTool, "Open the Certificate Retrieval Tool.");
            toolTip1.SetToolTip(Textbox_Verilen, "Displays the certificate subject information.");
            toolTip1.SetToolTip(Textbox_Veren, "Displays the certificate issuer information.");
            toolTip1.SetToolTip(TextBox_aki, "Displays the Authority Key Identifier.");
            toolTip1.SetToolTip(TextBox_ski, "Displays the Subject Key Identifier.");
            toolTip1.SetToolTip(Textbox_Serino, "Displays the certificate serial number.");
            toolTip1.SetToolTip(Textbox_Usage, "Displays the permitted key usage purposes.");
            toolTip1.SetToolTip(Textbox_Constraints, "Displays the certificate basic constraints.");
            toolTip1.SetToolTip(Textbox_Baslangic, "Displays the certificate validity start date.");
            toolTip1.SetToolTip(Textbox_Bitis, "Displays the certificate expiration date.");
            toolTip1.SetToolTip(Textbox_Algoritma, "Displays the certificate signature algorithm.");
            toolTip1.SetToolTip(TextBox_Publickey, "Displays the public key algorithm and size.");
            toolTip1.SetToolTip(TextBox_Thumbprint, "Displays the certificate thumbprint.");
            toolTip1.SetToolTip(TextBox_Policy, "Displays the certificate policy information.");
            toolTip1.SetToolTip(TreeView_Chain, "Displays the certificate trust chain.");
            toolTip1.SetToolTip(Label_TotalChain, "Displays the total number of certificates in the chain.");
            toolTip1.SetToolTip(listView_VerificationDetail, "Displays certificate validation results.");
            toolTip1.SetToolTip(listView_SecurityAnalysis, "Displays potential certificate security issues.");
            toolTip1.SetToolTip(TextBox_EKU, "Displays the Extended Key Usage extensions.");
            toolTip1.SetToolTip(button_eku_recheck, "Recheck the Extended Key Usage.");
            toolTip1.SetToolTip(TextBox_SAN, "Displays the Subject Alternative Names.");
            toolTip1.SetToolTip(button_san_recheck, "Recheck the Subject Alternative Names.");
            toolTip1.SetToolTip(TextBox_CRL, "Displays the Certificate Revocation List distribution points.");
            toolTip1.SetToolTip(button_crl_recheck, "Recheck the Certificate Revocation List.");
            toolTip1.SetToolTip(button_crl_downloader, "Download the Certificate Revocation List.");
            toolTip1.SetToolTip(TextBox_AIA, "Displays the Authority Information Access extensions.");
            toolTip1.SetToolTip(button_aia_recheck, "Recheck the Authority Information Access.");
            toolTip1.SetToolTip(button_aia_downloader, "Download the CA certificate from the AIA address.");
            toolTip1.SetToolTip(TextBox_OCSP, "Displays the OCSP responder addresses.");
            toolTip1.SetToolTip(button_ocsp_recheck, "Recheck the OCSP responder status.");
            toolTip1.SetToolTip(button_ocsp_downloader, "Download the OCSP response.");
            toolTip1.SetToolTip(Textbox_Durum, "Displays the overall certificate validation result.");
            toolTip1.SetToolTip(Button_Result_Recheck, "Recheck the overall certificate status.");
        }
        private void ClearFileUI() // Dosya ile ilgili tüm UI alanlarını temizleyen metot. ---------------------------------------------------------
        {
            Textbox_DosyaAdi.Clear();
            Textbox_DosyaYolu.Clear();
            Textbox_Veren.Clear();
            Textbox_Verilen.Clear();
            Textbox_Baslangic.Clear();
            Textbox_Bitis.Clear();
            Textbox_Serino.Clear();
            TextBox_aki.Clear();
            TextBox_ski.Clear();
            Textbox_Algoritma.Clear();
            Textbox_Durum.Clear();
            Textbox_Constraints.Clear();
            TextBox_Publickey.Clear();
            Textbox_Usage.Clear();
            TextBox_CRL.Clear();
            TextBox_AIA.Clear();
            TextBox_OCSP.Clear();
            TextBox_EKU.Clear();
            TextBox_SAN.Clear();
            TextBox_Policy.Clear();
            TextBox_Thumbprint.Clear();
            PictureBox_Durum.Image = null;
            pictureBox_AIA.Image = null;
            pictureBox_CDP.Image = null;
            pictureBox_OCSP.Image = null;
            TreeView_Chain.Nodes.Clear();
            Label_TotalChain.Text = "-";
            listView_VerificationDetail.Items.Clear();
            listView_SecurityAnalysis.Items.Clear();
            Buton_Goruntule.Enabled = false;
            Button_CPS.Enabled = false;
            Button_Terminal.Enabled = false;
            button_crl_downloader.Enabled = false;
            button_aia_downloader.Enabled = false;
            button_ocsp_downloader.Enabled = false;
            button_crl_recheck.Enabled = false;
            button_aia_recheck.Enabled = false;
            button_ocsp_recheck.Enabled = false;
            button_eku_recheck.Enabled = false;
            button_san_recheck.Enabled = false;
            CurrentCertificate = null;
        }
        private readonly CertificateParsingService _certificateParsingService = new CertificateParsingService(); // Sertifika ayrıştırma servisi örneği.
        private void RefreshSecurityReport() // Sertifika güvenlik raporunu yenileyen metot. ---------------------------------------------------------
        {
            listView_SecurityAnalysis.BeginUpdate();
            try
            {
                listView_SecurityAnalysis.Items.Clear();
                if (CurrentCertificate == null)
                    return;
                var service = new SecurityAnalysisService();
                List<SecurityCheckResult> results = service.Analyze(CurrentCertificate);
                foreach (SecurityCheckResult result in results)
                {
                    var item = new ListViewItem(result.Component);
                    item.SubItems.Add(result.Status);
                    item.SubItems.Add(result.Description);
                    item.UseItemStyleForSubItems = false;
                    Color statusColor = result.Status switch
                    {
                        "CRITICAL" => Color.Red,
                        "WARNING" => Color.DarkOrange,
                        "OK" => Color.Green,
                        "INFO" => Color.DodgerBlue,
                        "UNKNOWN" => Color.Gray,
                        "REVIEW" => Color.DarkViolet,
                        _ => Color.Black
                    };
                    item.SubItems[1].ForeColor = statusColor;
                    listView_SecurityAnalysis.Items.Add(item);
                }
            }
            finally
            {
                listView_SecurityAnalysis.EndUpdate();
            }
        }
        private void RefreshVerificationReport() // Sertifika doğrulama raporunu yenileyen metot. ---------------------------------------------------------
        {
            if (CurrentCertificate == null)
            {
                VerificationReportService.Render(listView_VerificationDetail,new List<VerificationCheckResult>());
                return;
            }
            var results = new List<VerificationCheckResult>();
            try
            {
                List<string> crlUrls = TextBox_CRL.Text.Split(new[] { '\r', '\n' },StringSplitOptions.RemoveEmptyEntries).Select(url => url.Trim()).Where(url => !string.IsNullOrWhiteSpace(url)).ToList();
                List<string> aiaUrls = TextBox_AIA.Text.Split(new[] { '\r', '\n' },StringSplitOptions.RemoveEmptyEntries).Select(url => url.Trim()).Where(url => !string.IsNullOrWhiteSpace(url)).ToList();
                List<string> ocspUrls = TextBox_OCSP.Text.Split(new[] { '\r', '\n' },StringSplitOptions.RemoveEmptyEntries).Select(url => url.Trim()).Where(url => !string.IsNullOrWhiteSpace(url)).ToList();
                var cdpService = new CdpValidationService();
                VerificationCheckResult cdpResult = cdpService.Validate(CurrentCertificate, crlUrls);
                results.Add(cdpResult);
                var aiaService = new AiaValidationService();
                List<VerificationCheckResult> aiaResults = aiaService.Validate(CurrentCertificate, aiaUrls);
                results.AddRange(aiaResults);
                var ocspService = new OcspValidationService();
                List<VerificationCheckResult> ocspResults = ocspService.Validate(CurrentCertificate, ocspUrls);
                results.AddRange(ocspResults);
                UpdateVerificationIndicator(pictureBox_CDP,new List<VerificationCheckResult> { cdpResult });
                UpdateVerificationIndicator(pictureBox_AIA,aiaResults);
                UpdateVerificationIndicator(pictureBox_OCSP,ocspResults);
            }
            catch (Exception ex)
            {
                results.Add(new VerificationCheckResult{Component = "Verification",Status = "ERROR",Description = "Verification report error: " + ex.Message,Source = "-"});
            }
            finally
            {
                VerificationReportService.Render(listView_VerificationDetail,results);
            }
        }
        private void UpdateVerificationIndicator(PictureBox pictureBox,List<VerificationCheckResult> results) // Sertifika doğrulama göstergesini güncelleyen metot. ---------------------------------------------------------
        {
            if (pictureBox == null)
                return;
            if (results == null || results.Count == 0)
            {
                pictureBox.Image = Properties.Resources.warning;
                return;
            }
            if (results.Any(result =>result.Status.Equals("ERROR",StringComparison.OrdinalIgnoreCase) ||result.Status.Equals("CRITICAL",StringComparison.OrdinalIgnoreCase) ||result.Status.Equals("FAILED",StringComparison.OrdinalIgnoreCase)))
            {
                pictureBox.Image = Properties.Resources.error;
                return;
            }
            if (results.Any(result =>result.Status.Equals("WARNING",StringComparison.OrdinalIgnoreCase)))
            {
                pictureBox.Image = Properties.Resources.warning;
                return;
            }
            if (results.Any(result =>result.Status.Equals("OK",StringComparison.OrdinalIgnoreCase)))
            {
                pictureBox.Image = Properties.Resources.ok;
                return;
            }
            pictureBox.Image = Properties.Resources.warning;
        }
        private void CertificateDatesCheck(DateTime baslangic, DateTime bitis) //Sertifikanın başlangıç ve bitiş tarihlerini kontrol eden metot. ---------------------------------------------------------
        {
            DateTime simdi = DateTime.Now;
            if ((bitis - simdi).TotalDays <= 30)
            {
                Textbox_Bitis.ForeColor = Color.Red;
            }
            else
            {
                Textbox_Bitis.ForeColor = SystemColors.WindowText;
            }
            if (baslangic > simdi)
            {
                Textbox_Baslangic.ForeColor = Color.Red;
            }
            else
            {
                Textbox_Baslangic.ForeColor = SystemColors.WindowText;
            }
        }
        private string GetEkuFriendlyName(string oid)//Extended Key Usage (EKU) OID'lerini kullanıcı dostu isimlere çeviren metot. ---------------------------------------------------------
        {
            return oid switch
            {
                "1.3.6.1.5.5.7.3.1" => "Server Authentication",
                "1.3.6.1.5.5.7.3.2" => "Client Authentication",
                "1.3.6.1.5.5.7.3.3" => "Code Signing",
                "1.3.6.1.5.5.7.3.4" => "Email Protection",
                "1.3.6.1.5.5.7.3.8" => "Time Stamping",
                "1.3.6.1.5.5.7.3.9" => "OCSP Signing",
                "2.5.29.37.0" => "Any Extended Key Usage",
                _ => "Unknown Usage"
            };
        }
        private void UpdateDownloadButtons() //CRL, AIA ve OCSP adreslerinin geçerliliğini kontrol eden metot. ---------------------------------------------------------
        {
            bool hasCrl = TextBox_CRL.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Any(x => { string value = x.Trim(); return value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || value.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || value.StartsWith("file://", StringComparison.OrdinalIgnoreCase); });
            bool hasAia = TextBox_AIA.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Any(x => { string value = x.Trim(); return value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || value.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || value.StartsWith("file://", StringComparison.OrdinalIgnoreCase); });
            bool hasOcsp = TextBox_OCSP.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Any(x => { string value = x.Trim(); return value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || value.StartsWith("https://", StringComparison.OrdinalIgnoreCase); });
            button_crl_downloader.Enabled = hasCrl;
            button_aia_downloader.Enabled = hasAia;
            button_ocsp_downloader.Enabled = hasOcsp;
            button_crl_recheck.Enabled = hasCrl;
            button_aia_recheck.Enabled = hasAia;
            button_ocsp_recheck.Enabled = hasOcsp;
        }
        private List<string> GetUrlsFromTextBox(System.Windows.Forms.TextBox textBox) //CRL, AIA ve OCSP adreslerini TextBox'tan alıp geçerli URL'leri listeleyen metot. ---------------------------------------------------------
        {
            return textBox.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "No URI found" && x != "Not Found").Select(x =>
            {
                int http = x.IndexOf("http://", StringComparison.OrdinalIgnoreCase);
                int https = x.IndexOf("https://", StringComparison.OrdinalIgnoreCase);
                int index;
                if (http >= 0 && https >= 0)
                    index = Math.Min(http, https);
                else if (http >= 0)
                    index = http;
                else
                    index = https;

                return index >= 0 ? x.Substring(index).Trim() : null;
            }).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }
        private string GetDesktopPath() //Kullanıcının masaüstü dizin yolunu döndüren metot. ---------------------------------------------------------
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }
        private string GetSafeFileName(string fileName) //Dosya adını geçerli karakterlerle güvenli hale getiren metot. ---------------------------------------------------------
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(c, '_');
            return fileName;
        }
        private string GetUniqueFilePath(string directory, string fileName) //Belirtilen dizinde benzersiz bir dosya yolu döndüren metot. Eğer dosya zaten varsa, dosya adının sonuna bir sayı ekleyerek benzersiz bir ad oluşturur. ---------------------------------------------------------
        {
            fileName = GetSafeFileName(fileName);
            string path = Path.Combine(directory, fileName);
            if (!File.Exists(path))
                return path;
            string name = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int counter = 2;
            do
            {
                path = Path.Combine(directory, name + "_" + counter + extension);
                counter++;
            }
            while (File.Exists(path));
            return path;
        }
        private string GetFileNameFromUrl(string url, string defaultName) //URL'den dosya adını çıkaran metot. Eğer URL geçerli değilse veya dosya adı alınamıyorsa, varsayılan adı döndürür. ---------------------------------------------------------
        {
            try
            {
                Uri uri = new Uri(url);
                string fileName = Path.GetFileName(uri.LocalPath);
                if (!string.IsNullOrWhiteSpace(fileName))
                    return GetSafeFileName(fileName);
            }
            catch
            {
            }
            return defaultName;
        }
        private byte[] DownloadHttpData(string url) //HTTP veya HTTPS URL'sinden veri indiren metot. ---------------------------------------------------------
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(20);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("CertificateChecker/1.0");
                client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
                return client.GetByteArrayAsync(url).GetAwaiter().GetResult();
            }
        }
        private void DownloadCrlFiles() //CRL dosyalarını indiren metot. ---------------------------------------------------------
        {
            try
            {
                List<string> urls = GetUrlsFromTextBox(TextBox_CRL);
                if (urls.Count == 0)
                {
                    MessageBox.Show("No valid CRL address was found.", "CRL Downloader", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string desktop = GetDesktopPath();
                int success = 0;
                int failed = 0;
                foreach (string url in urls)
                {
                    try
                    {
                        byte[] data;
                        if (url.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
                        {
                            string localPath = new Uri(url).LocalPath;
                            if (!File.Exists(localPath))
                            {
                                failed++;
                                continue;
                            }
                            data = File.ReadAllBytes(localPath);
                        }
                        else
                        {
                            data = DownloadHttpData(url);
                        }

                        if (data == null || data.Length == 0)
                        {
                            failed++;
                            continue;
                        }
                        var parser = new Org.BouncyCastle.X509.X509CrlParser();
                        var crl = parser.ReadCrl(data);
                        if (crl == null)
                        {
                            failed++;
                            continue;
                        }
                        string fileName = GetFileNameFromUrl(url, "certificate_revocation_list.crl");
                        if (!fileName.EndsWith(".crl", StringComparison.OrdinalIgnoreCase))
                        {
                            fileName += ".crl";
                        }

                        string destination = GetUniqueFilePath(desktop, fileName);
                        File.WriteAllBytes(destination, data);
                        success++;
                    }
                    catch
                    {
                        failed++;
                    }
                }
                if (success > 0 && failed == 0)
                {
                    MessageBox.Show(
                        success + " CRL file(s) downloaded successfully to the Desktop.", "CRL Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information
                    );
                }
                else if (success > 0)
                {
                    MessageBox.Show(success + " CRL file(s) downloaded successfully.\r\n" + failed + " file(s) could not be downloaded.", "CRL Downloader", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("CRL files could not be downloaded.", "CRL Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CRL download failed.\r\n\r\n" + ex.Message, "CRL Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DownloadAiaFiles() //AIA dosyalarını indiren metot. ---------------------------------------------------------
        {
            try
            {
                List<string> urls = GetUrlsFromTextBox(TextBox_AIA);

                if (urls.Count == 0)
                {
                    MessageBox.Show("No valid AIA address was found.", "AIA Downloader", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string desktop = GetDesktopPath();
                int success = 0;
                int failed = 0;
                foreach (string url in urls)
                {
                    try
                    {
                        byte[] data;

                        if (url.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
                        {
                            string localPath = new Uri(url).LocalPath;
                            if (!File.Exists(localPath))
                            {
                                failed++;
                                continue;
                            }
                            data = File.ReadAllBytes(localPath);
                        }
                        else
                        {
                            data = DownloadHttpData(url);
                        }

                        if (data == null || data.Length == 0)
                        {
                            failed++;
                            continue;
                        }
                        var parser = new Org.BouncyCastle.X509.X509CertificateParser();
                        var issuerCertificate = parser.ReadCertificate(data);
                        if (issuerCertificate == null)
                        {
                            failed++;
                            continue;
                        }
                        string fileName = GetFileNameFromUrl(url, "issuer_certificate.cer");
                        string extension = Path.GetExtension(fileName);
                        if (string.IsNullOrWhiteSpace(extension))
                        {
                            fileName += ".cer";
                        }
                        string destination = GetUniqueFilePath(desktop, fileName);
                        File.WriteAllBytes(destination, data);
                        success++;
                    }
                    catch
                    {
                        failed++;
                    }
                }
                if (success > 0 && failed == 0)
                {
                    MessageBox.Show(success + " AIA certificate(s) downloaded successfully to the Desktop.", "AIA Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (success > 0)
                {
                    MessageBox.Show(success + " AIA certificate(s) downloaded successfully.\r\n" + failed + " file(s) could not be downloaded.", "AIA Downloader", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("AIA issuer certificates could not be downloaded.", "AIA Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AIA download failed.\r\n\r\n" + ex.Message, "AIA Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }
        private void DownloadOcspResponses(X509Certificate2 cert) //OCSP yanıtlarını indiren metot. ---------------------------------------------------------
        {
            try
            {
                List<string> urls = GetUrlsFromTextBox(TextBox_OCSP);

                if (urls.Count == 0)
                {
                    MessageBox.Show("No valid OCSP address was found.", "OCSP Downloader", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                var bcParser = new Org.BouncyCastle.X509.X509CertificateParser();
                var subjectBc = bcParser.ReadCertificate(cert.RawData);
                Org.BouncyCastle.X509.X509Certificate issuerBc = null;
                try
                {
                    X509Chain chain = new X509Chain();
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                    chain.Build(cert);
                    if (chain.ChainElements.Count > 1)
                    {
                        issuerBc = bcParser.ReadCertificate(chain.ChainElements[1].Certificate.RawData);
                    }
                }
                catch
                {
                }
                if (issuerBc == null)
                {
                    MessageBox.Show("The certificate issuer could not be found. OCSP request cannot be created.", "OCSP Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var certId = new Org.BouncyCastle.Ocsp.CertificateID(Org.BouncyCastle.Ocsp.CertificateID.HashSha1, issuerBc, subjectBc.SerialNumber);
                var generator = new Org.BouncyCastle.Ocsp.OcspReqGenerator();
                generator.AddRequest(certId);
                var request = generator.Generate();
                byte[] requestBytes = request.GetEncoded();
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                int success = 0;
                int failed = 0;
                int requestNumber = 1;
                foreach (string url in urls)
                {
                    try
                    {
                        byte[] responseBytes;

                        using (var client = new HttpClient())
                        {
                            client.Timeout = TimeSpan.FromSeconds(20);
                            client.DefaultRequestHeaders.UserAgent.ParseAdd("CertificateChecker/1.0");
                            client.DefaultRequestHeaders.Accept.ParseAdd("application/ocsp-response");
                            using (var content = new ByteArrayContent(requestBytes))
                            {
                                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/ocsp-request");
                                HttpResponseMessage response = client.PostAsync(url, content).GetAwaiter().GetResult();
                                response.EnsureSuccessStatusCode();
                                responseBytes = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                            }
                        }
                        if (responseBytes == null || responseBytes.Length == 0)
                        {
                            failed++;
                            requestNumber++;
                            continue;
                        }
                        string fileName = "request" + requestNumber + ".ocsp";
                        string destination = GetUniqueFilePath(desktop, fileName);
                        File.WriteAllBytes(destination, responseBytes);
                        success++;
                        requestNumber++;
                    }
                    catch
                    {
                        failed++;
                        requestNumber++;
                    }
                }
                if (success > 0 && failed == 0)
                {
                    MessageBox.Show(success + " OCSP response(s) downloaded successfully to the Desktop.", "OCSP Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (success > 0)
                {
                    MessageBox.Show(success + " OCSP response(s) downloaded successfully.\r\n" + failed + " OCSP response(s) could not be downloaded.", "OCSP Downloader", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("No OCSP response could be downloaded.", "OCSP Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCSP download failed.\r\n\r\n" + ex.Message, "OCSP Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CertificateChain(X509Certificate2 sertifika, System.Windows.Forms.TreeView treeView) //Sertifika Zinciri oluşturan metot. ---------------------------------------------------------
        {
            treeView.Nodes.Clear();
            X509Chain zincir = new X509Chain();
            zincir.ChainPolicy.RevocationMode = X509RevocationMode.Online;
            zincir.Build(sertifika);
            Label_TotalChain.Text = zincir.ChainElements.Count.ToString();
            TreeNode currentNode = null;
            for (int i = zincir.ChainElements.Count - 1; i >= 0; i--)
            {
                var element = zincir.ChainElements[i];
                string subject = element.Certificate.Subject;
                string cn = GetCommonName(subject);

                if (currentNode == null)
                {
                    currentNode = treeView.Nodes.Add(cn);
                }
                else
                {
                    currentNode = currentNode.Nodes.Add(cn);
                }
            }
            treeView.ExpandAll();
        }
        private string GetCommonName(string subject) //Sertifika konusundan Common Name (CN) bilgisini çeken metot. ---------------------------------------------------------
        {
            var match = System.Text.RegularExpressions.Regex.Match(subject, @"CN\s*=\s*([^,]+)");
            return match.Success ? match.Groups[1].Value.Trim() : subject;
        }
        public void FileCertificateVerification() //Sertifika dosyasını doğrulayan ve bilgilerini ayrıştıran metot. ---------------------------------------------------------
        {
            try
            {
                X509Certificate2 sertifika = new X509Certificate2(File_Path);
                CurrentCertificate = sertifika;
                CertificateDetails details = _certificateParsingService.Parse(sertifika);
                Textbox_Verilen.Text = details.Subject;
                Textbox_Veren.Text = details.Issuer;
                Textbox_Serino.Text = details.SerialNumber;
                TextBox_aki.Text = details.AuthorityKeyIdentifier;
                TextBox_ski.Text = details.SubjectKeyIdentifier;
                Textbox_Usage.Text = details.KeyUsage;
                Textbox_Constraints.Text = details.BasicConstraints;
                Textbox_Baslangic.Text = details.NotBefore.ToString();
                Textbox_Bitis.Text = details.NotAfter.ToString();
                Textbox_Algoritma.Text = details.SignatureAlgorithm;
                TextBox_Publickey.Text = details.PublicKeyInfo;
                TextBox_Thumbprint.Text = details.Thumbprint;
                TextBox_Policy.Text = details.CertificatePolicy;
                TextBox_CRL.Text = details.CrlText;
                TextBox_AIA.Text = details.AiaText;
                TextBox_OCSP.Text = details.OcspText;
                TextBox_EKU.Text = details.EkuText;
                TextBox_SAN.Text = details.SanText;
                button_eku_recheck.Enabled = true;
                button_san_recheck.Enabled = true;
                Button_Result_Recheck.Enabled = true;
                X509Chain chain = new X509Chain();
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                CertificateChain(sertifika, TreeView_Chain);
                CertificateDatesCheck(sertifika.NotBefore,sertifika.NotAfter);
                UpdateDownloadButtons();
                bool isChainValid = chain.Build(sertifika);
                RefreshSecurityReport();
                RefreshVerificationReport();
                if (isChainValid)
                {
                    Textbox_Durum.Text = "Certificate is Valid and Trusted.";
                    PictureBox_Durum.Image = Properties.Resources.ok;
                }
                else
                {
                    Textbox_Durum.Text = "Certificate is Invalid!";
                    PictureBox_Durum.Image = Properties.Resources.error;
                    foreach (X509ChainStatus status in chain.ChainStatus)
                    {
                        if (status.Status == X509ChainStatusFlags.NotTimeValid)
                        {
                            DateTime simdiki_zaman = DateTime.Now;
                            DateTime baslangic_zamani = DateTime.Parse(Textbox_Baslangic.Text);
                            DateTime bitis_zamani = DateTime.Parse(Textbox_Bitis.Text);
                            if (baslangic_zamani > simdiki_zaman)
                            {
                                PictureBox_Durum.Image = Properties.Resources.ok;
                                Textbox_Durum.Text = "Certificate is Not Yet Valid (Start date is in the future).";
                            }
                            else if (bitis_zamani < simdiki_zaman)
                            {
                                PictureBox_Durum.Image = Properties.Resources.error;
                                Textbox_Durum.Text = "Certificate is Expired (Validity period has ended).";
                            }
                            break;
                        }
                        else if (status.Status == X509ChainStatusFlags.Revoked)
                        {
                            Textbox_Durum.Text = "Certificate has been Revoked by the issuing authority.";
                            break;
                        }
                        else if (status.Status == X509ChainStatusFlags.UntrustedRoot)
                        {
                            Textbox_Durum.Text = "Untrusted Root: The root certificate is not trusted on this machine.";

                            break;
                        }
                        else
                        {
                            Textbox_Durum.Text = "Invalid: " + status.StatusInformation;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a valid certificate file. Valid file extensions:\n.cer, .crt, .exe, .pem, etc.","Invalid File",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private string GetCertificatePolicyUrl(X509Certificate2 cert) //Sertifika politikalarından URL'yi ayıklayan metot. ---------------------------------------------------------
        {
            foreach (var extension in cert.Extensions)
            {
                if (extension.Oid.Value == "2.5.29.32")
                {
                    var asnData = new AsnEncodedData(extension.Oid, extension.RawData);
                    string policies = asnData.Format(true);

                    var urlStartIndex = policies.IndexOf("http");
                    if (urlStartIndex >= 0)
                    {
                        var urlEndIndex = policies.IndexOfAny(new char[] { ' ', '\n', '\r' }, urlStartIndex);
                        if (urlEndIndex > urlStartIndex)
                        {
                            return policies.Substring(urlStartIndex, urlEndIndex - urlStartIndex);
                        }
                    }
                }
            }
            return null;
        }
        private void Buton_Dosya_Sec_Click(object sender, EventArgs e) //Sertifika dosyasını seçmemizi sağlayan metot. ---------------------------------------------------------
        {
            OpenFileDialog DosyaAc = new OpenFileDialog();
            DosyaAc.Title = "Please Select Certificate File...";
            DosyaAc.Filter = "Certificate Files (*.cer;*.cert;*.crt;*.pem;*.der;*.p7b;*.p7c;*.pfx;*.key)|*.cer;*.cert;*.crt;*.pem;*.der;*.p7b;*.p7c;*.pfx;*.key|All Files (*.*)|*.*";
            DosyaAc.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DosyaAc.Multiselect = false;
            if (DosyaAc.ShowDialog() == DialogResult.OK)
            {
                ClearFileUI();
                File_Name = DosyaAc.SafeFileName;
                File_Path = DosyaAc.FileName;
                FileInfo DosyaAcInfo = new FileInfo(File_Path);
                Textbox_DosyaAdi.Text = File_Name;
                Textbox_DosyaYolu.Text = File_Path;
                FileCertificateVerification();
                Buton_Goruntule.Enabled = true;
                Button_CPS.Enabled = true;
                Button_Terminal.Enabled = true;
                Button_RetrivalTool.Enabled = true;
            }
        }
        private void Buton_Goruntule_Click(object sender, EventArgs e) //Sertifika dosyasını görüntülememizi sağlayan metot. ---------------------------------------------------------
        {
            try
            {
                Process.Start(new ProcessStartInfo(File_Path) { UseShellExecute = true });
            }
            catch (Exception)
            {
                MessageBox.Show("The Certificate display operation could not be performed.\nRun the Certificate Checker as administrator and try again.", "View Certificate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Button_CPS_Click(object sender, EventArgs e) //Sertifika politikalarından URL'yi açan metot. ---------------------------------------------------------
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(File_Path);

                string certificatePolicyUrl = GetCertificatePolicyUrl(cert);
                if (!string.IsNullOrEmpty(certificatePolicyUrl))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = certificatePolicyUrl,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("No valid URL found in Certificate Policies field.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: ", "Error" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_Terminal_Click(object sender, EventArgs e) //Sertifika doğrulamasını terminal üzerinden yapan metot. ---------------------------------------------------------
        {
            if (string.IsNullOrWhiteSpace(File_Path) || !File.Exists(File_Path))
            {
                MessageBox.Show("No valid certificate file is currently loaded.", "CertUtil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string arguments = "/k certutil -urlfetch -verify \"" + File_Path + "\"";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = arguments,
                    UseShellExecute = true,
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Normal
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CertUtil could not be started.\n\n" + ex.Message, "CertUtil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_RetrivalTool_Click(object sender, EventArgs e) // Sertifika doğrulamasını Retrieval Tool üzerinden yapan metot. ---------------------------------------------------------
        {
            try
            {
                if (string.IsNullOrWhiteSpace(File_Path) || !File.Exists(File_Path))
                {
                    MessageBox.Show("Please select a valid certificate file first.", "Retrieval Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = "certutil.exe",
                    Arguments = $"-URL \"{File_Path}\"",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch Certificate Retrieval Tool:\n{ex.Message}", "Retrieval Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_eku_recheck_Click(object sender, EventArgs e) // EKU (Extended Key Usage) doğrulamasını yapan metot. ---------------------------------------------------------
        {
            if (CurrentCertificate == null)
            {
                MessageBox.Show("No certificate is currently loaded.", "EKU Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            const string serverAuthenticationOid = "1.3.6.1.5.5.7.3.1";
            const string anyExtendedKeyUsageOid = "2.5.29.37.0";
            var ekuExtension = CurrentCertificate.Extensions["2.5.29.37"];
            if (ekuExtension == null)
            {
                MessageBox.Show("EKU extension is not present.\n\nThe certificate does not restrict its usage through EKU.\n\nServer Authentication: PERMITTED", "EKU Recheck", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                var eku = new X509EnhancedKeyUsageExtension(ekuExtension, false);
                var ekuOids = eku.EnhancedKeyUsages.Cast<Oid>().Where(x => !string.IsNullOrWhiteSpace(x.Value)).ToList();
                if (ekuOids.Any(x => x.Value == serverAuthenticationOid || x.Value == anyExtendedKeyUsageOid))
                {
                    MessageBox.Show("Server Authentication is permitted by the certificate EKU.\n\nEKU Check: OK", "EKU Recheck", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string ekuList = string.Join("\n", ekuOids.Select(x => GetEkuFriendlyName(x.Value) + " (" + x.Value + ")"));
                MessageBox.Show("Server Authentication is NOT permitted by the certificate EKU.\n\nAllowed usages:\n" + ekuList + "\n\nEKU Check: FAILED", "EKU Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("EKU could not be parsed.\n\n" + ex.Message, "EKU Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_san_recheck_Click(object sender, EventArgs e) // SAN (Subject Alternative Name) doğrulamasını yapan metot. ---------------------------------------------------------
        {
            if (CurrentCertificate == null)
            {
                MessageBox.Show("No certificate is currently loaded.", "SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var sanExtension = CurrentCertificate.Extensions["2.5.29.17"];
            if (sanExtension == null)
            {
                MessageBox.Show("The certificate does not contain a Subject Alternative Name (SAN) extension.", "SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("The certificate contains a SAN extension. Review the DNS/IP entries shown in the SAN field.","SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button_crl_recheck_Click(object sender, EventArgs e) // CRL (Certificate Revocation List) doğrulamasını yapan metot. ---------------------------------------------------------
        {
            try
            {
                if (CurrentCertificate == null)
                {
                    pictureBox_CDP.Image = Properties.Resources.warning;
                    PictureBox_Durum.Image = Properties.Resources.warning;
                    Textbox_Durum.Text = "Certificate is Valid with Warning!\r\n" + "No certificate is currently loaded.";
                    MessageBox.Show("No certificate is currently loaded.", "CRL Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<string> crlUrls = TextBox_CRL.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "No URI found" && x != "Not Found" && (x.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || x.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || x.StartsWith("file://", StringComparison.OrdinalIgnoreCase))).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                if (crlUrls.Count == 0)
                {
                    pictureBox_CDP.Image = Properties.Resources.warning;
                    PictureBox_Durum.Image = Properties.Resources.warning;
                    Textbox_Durum.Text = "Certificate is Valid with Warning!\r\n" + "CRL is not available.";
                    listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Warning", "No CRL address found", "-" }));
                    MessageBox.Show("No CRL address was found in the certificate.", "CRL Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var crlParser = new Org.BouncyCastle.X509.X509CrlParser();
                var certificateParser = new Org.BouncyCastle.X509.X509CertificateParser();
                var certificateBc = certificateParser.ReadCertificate(CurrentCertificate.RawData);
                Org.BouncyCastle.X509.X509Certificate issuerBc = null;
                try
                {
                    X509Chain chain = new X509Chain();
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;
                    chain.Build(CurrentCertificate);
                    foreach (X509ChainElement element in chain.ChainElements)
                    {
                        var candidate = certificateParser.ReadCertificate(element.Certificate.RawData);
                        if (candidate.SubjectDN.Equivalent(
                            certificateBc.IssuerDN))
                        {
                            issuerBc = candidate;
                            break;
                        }
                    }
                }
                catch
                {
                    issuerBc = null;
                }
                if (issuerBc == null)
                {
                    pictureBox_CDP.Image = Properties.Resources.error;
                    PictureBox_Durum.Image = Properties.Resources.error;
                    Textbox_Durum.Text = "Certificate is Invalid!\r\n" + "CRL issuer certificate could not be found.";
                    listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL issuer certificate not found", "-" }));
                    MessageBox.Show("CRL validation failed.\r\n\r\n" + "The certificate issuer could not be found.", "CRL Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var serial = new Org.BouncyCastle.Math.BigInteger(CurrentCertificate.SerialNumber, 16);
                bool anyReachable = false;
                bool anyValidCrl = false;
                bool certificateRevoked = false;
                List<string> unreachableUrls = new List<string>();
                foreach (string url in crlUrls)
                {
                    try
                    {
                        byte[] crlBytes;
                        if (url.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
                        {
                            string localPath = new Uri(url).LocalPath;

                            if (!File.Exists(localPath))
                            {
                                unreachableUrls.Add(url);
                                listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Warning", "CRL file not found", url }));
                                continue;
                            }
                            crlBytes = File.ReadAllBytes(localPath);
                        }
                        else
                        {
                            using (var client = new WebClient())
                            {
                                client.Headers.Add("User-Agent", "CertificateChecker/1.0");
                                client.Headers.Add("Accept", "*/*");
                                client.Headers.Add("Cache-Control", "no-cache");
                                crlBytes = client.DownloadData(url);
                            }
                        }
                        if (crlBytes == null ||
                            crlBytes.Length == 0)
                        {
                            unreachableUrls.Add(url);
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Warning", "CRL response is empty", url }));
                            continue;
                        }
                        anyReachable = true;
                        Org.BouncyCastle.X509.X509Crl crl;
                        try
                        {
                            crl = crlParser.ReadCrl(crlBytes);
                        }
                        catch
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Error", "Downloaded data is not a valid CRL", url }));
                            continue;
                        }
                        if (crl == null)
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL could not be parsed", url }));
                            continue;
                        }
                        if (!crl.IssuerDN.Equivalent(issuerBc.SubjectDN))
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL issuer mismatch", url }));
                            continue;
                        }
                        try
                        {
                            crl.Verify(issuerBc.GetPublicKey());
                        }
                        catch
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL signature invalid", url }));
                            continue;
                        }
                        anyValidCrl = true;
                        var revoked = crl.GetRevokedCertificate(serial);
                        if (revoked != null)
                        {
                            certificateRevoked = true;
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Error", "Certificate revoked", url }));
                            break;
                        }
                        listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "OK", "CRL verified", url }));
                        break;
                    }
                    catch (Exception ex)
                    {
                        unreachableUrls.Add(url);
                        listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Warning", "CRL could not be reached", url }));
                    }
                }
                if (certificateRevoked)
                {
                    pictureBox_CDP.Image = Properties.Resources.error;
                    PictureBox_Durum.Image = Properties.Resources.error;
                    Textbox_Durum.Text = "Certificate is Invalid!\r\n" + "Certificate is revoked according to CRL.";
                    MessageBox.Show("CRL recheck completed.\r\n\r\n" + "The certificate is REVOKED.", "CRL Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (anyValidCrl)
                {
                    pictureBox_CDP.Image = Properties.Resources.ok;
                    PictureBox_Durum.Image = Properties.Resources.ok;
                    Textbox_Durum.Text = "Certificate is Valid.\r\n" + "CRL validation successful.";
                    MessageBox.Show("CRL validation completed successfully.\r\n\r\n" + "The CRL was verified and the certificate is not revoked.", "CRL Recheck", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!anyReachable)
                {
                    pictureBox_CDP.Image = Properties.Resources.warning;
                    PictureBox_Durum.Image = Properties.Resources.warning;
                    Textbox_Durum.Text = "Certificate is Valid with Warning!\r\n" + "CRL could not be reached.";
                    MessageBox.Show("CRL recheck could not be completed.\r\n\r\n" + "None of the CRL addresses could be reached.", "CRL Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                pictureBox_CDP.Image = Properties.Resources.error;
                PictureBox_Durum.Image = Properties.Resources.error;
                Textbox_Durum.Text = "Certificate is Invalid!\r\n" + "CRL validation failed.";
                MessageBox.Show("CRL recheck failed.\r\n\r\n" + "A CRL was reached, but it could not be validated.", "CRL Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                pictureBox_CDP.Image = Properties.Resources.error;
                PictureBox_Durum.Image = Properties.Resources.error;
                Textbox_Durum.Text = "Certificate is Invalid!\r\n" + "CRL recheck error.";
                listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL recheck exception", "-" }));
                MessageBox.Show("CRL recheck failed.\r\n\r\n" + ex.Message, "CRL Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_aia_recheck_Click(object sender, EventArgs e) // AIA (Authority Information Access) doğrulamasını yapan metot. ---------------------------------------------------------
        {
            try
            {
                if (CurrentCertificate == null)
                {
                    pictureBox_AIA.Image = Properties.Resources.warning;
                    PictureBox_Durum.Image = Properties.Resources.warning;
                    Textbox_Durum.Text = "Certificate is Valid with Warning!\r\nNo certificate is currently loaded.";
                    MessageBox.Show("No certificate is currently loaded.", "AIA Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<string> aiaUrls = TextBox_AIA.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "No URI found" && x != "Not Found" && (x.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || x.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || x.StartsWith("file://", StringComparison.OrdinalIgnoreCase))).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                if (aiaUrls.Count == 0)
                {
                    pictureBox_AIA.Image = Properties.Resources.warning;
                    PictureBox_Durum.Image = Properties.Resources.warning;
                    Textbox_Durum.Text = "Certificate is Valid with Warning!\r\nAIA is not available.";

                    listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "AIA", "Warning", "No AIA address found", "-" }));

                    MessageBox.Show("No AIA address was found in the certificate.", "AIA Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var parser = new Org.BouncyCastle.X509.X509CertificateParser();
                var subjectBc = parser.ReadCertificate(CurrentCertificate.RawData);
                bool anyReachable = false;
                bool anyValidIssuer = false;
                bool anyInvalidIssuer = false;

                foreach (string url in aiaUrls)
                {
                    try
                    {
                        byte[] issuerBytes;

                        if (url.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
                        {
                            string localPath = new Uri(url).LocalPath;

                            if (!File.Exists(localPath))
                            {
                                listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "AIA", "Warning", "Issuer file not found", url }));
                                continue;
                            }

                            issuerBytes = File.ReadAllBytes(localPath);
                        }
                        else
                        {
                            using (var client = new WebClient())
                            {
                                client.Headers.Add("User-Agent", "CertificateChecker/1.0");
                                client.Headers.Add("Accept", "*/*");
                                client.Headers.Add("Cache-Control", "no-cache");
                                issuerBytes = client.DownloadData(url);
                            }
                        }

                        if (issuerBytes == null || issuerBytes.Length == 0)
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "AIA", "Warning", "Issuer response is empty", url }));
                            continue;
                        }

                        anyReachable = true;

                        Org.BouncyCastle.X509.X509Certificate issuerBc;

                        try
                        {
                            issuerBc = parser.ReadCertificate(issuerBytes);
                        }
                        catch
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "AIA", "Error", "Downloaded data is not a valid certificate", url }));
                            anyInvalidIssuer = true;
                            continue;
                        }

                        if (issuerBc == null)
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "AIA", "Error", "Issuer certificate could not be parsed", url }));
                            anyInvalidIssuer = true;
                            continue;
                        }
                        if (!subjectBc.IssuerDN.Equivalent(issuerBc.SubjectDN))
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "AIA", "Error", "Issuer subject does not match certificate issuer", url }));
                            anyInvalidIssuer = true;
                            continue;
                        }
                        try
                        {
                            subjectBc.Verify(issuerBc.GetPublicKey());
                        }
                        catch
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "AIA", "Error", "Issuer signature invalid", url }));
                            anyInvalidIssuer = true;
                            continue;
                        }
                        anyValidIssuer = true;
                        listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "AIA", "OK", "Issuer verified successfully", url }));
                        break;
                    }
                    catch
                    {
                        listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "AIA", "Warning", "Issuer could not be reached", url }));
                        continue;
                    }
                }
                if (anyValidIssuer)
                {
                    pictureBox_AIA.Image = Properties.Resources.ok;

                    MessageBox.Show("AIA validation completed successfully.\r\n\r\nThe issuer certificate was downloaded and verified.", "AIA Recheck", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (anyReachable && anyInvalidIssuer)
                {
                    pictureBox_AIA.Image = Properties.Resources.error;
                    PictureBox_Durum.Image = Properties.Resources.error;
                    Textbox_Durum.Text = "Certificate is Invalid!\r\nAIA issuer validation failed.";

                    MessageBox.Show("AIA validation failed.\r\n\r\nThe issuer certificate was reached but could not be verified.", "AIA Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                pictureBox_AIA.Image = Properties.Resources.warning;
                PictureBox_Durum.Image = Properties.Resources.warning;
                Textbox_Durum.Text = "Certificate is Valid with Warning!\r\nAIA issuer could not be reached.";

                MessageBox.Show("AIA validation could not be completed.\r\n\r\nNone of the AIA addresses could be reached.", "AIA Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                pictureBox_AIA.Image = Properties.Resources.error;
                PictureBox_Durum.Image = Properties.Resources.error;
                Textbox_Durum.Text = "Certificate is Invalid!\r\nAIA recheck error.";

                MessageBox.Show("AIA recheck failed.\r\n\r\n" + ex.Message, "AIA Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_ocsp_recheck_Click(object sender, EventArgs e) // OCSP (Online Certificate Status Protocol) doğrulamasını yapan metot. ---------------------------------------------------------
        {
            try
            {
                if (CurrentCertificate == null)
                {
                    pictureBox_OCSP.Image = Properties.Resources.warning;
                    PictureBox_Durum.Image = Properties.Resources.warning;
                    Textbox_Durum.Text = "Certificate is Valid with Warning!\r\nNo certificate is currently loaded.";

                    MessageBox.Show("No certificate is currently loaded.", "OCSP Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<string> ocspUrls = TextBox_OCSP.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "No URI found" && x != "Not Found" && (x.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || x.StartsWith("https://", StringComparison.OrdinalIgnoreCase))).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                if (ocspUrls.Count == 0)
                {
                    pictureBox_OCSP.Image = Properties.Resources.warning;
                    PictureBox_Durum.Image = Properties.Resources.warning;
                    Textbox_Durum.Text = "Certificate is Valid with Warning!\r\nOCSP is not available.";
                    listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "No OCSP address found", "-" }));
                    MessageBox.Show("No OCSP address was found in the certificate.", "OCSP Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var parser = new Org.BouncyCastle.X509.X509CertificateParser();
                var subjectBc = parser.ReadCertificate(CurrentCertificate.RawData);
                Org.BouncyCastle.X509.X509Certificate issuerBc = null;
                try
                {
                    X509Chain chain = new X509Chain();
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;
                    chain.Build(CurrentCertificate);
                    foreach (X509ChainElement element in chain.ChainElements)
                    {
                        var candidate = parser.ReadCertificate(element.Certificate.RawData);

                        if (candidate.SubjectDN.Equivalent(subjectBc.IssuerDN))
                        {
                            issuerBc = candidate;
                            break;
                        }
                    }
                }
                catch
                {
                    issuerBc = null;
                }
                if (issuerBc == null)
                {
                    pictureBox_OCSP.Image = Properties.Resources.error;
                    PictureBox_Durum.Image = Properties.Resources.error;
                    Textbox_Durum.Text = "Certificate is Invalid!\r\nOCSP issuer certificate could not be found.";
                    listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "OCSP issuer certificate not found", "-" }));
                    MessageBox.Show("OCSP validation failed.\r\n\r\nThe certificate issuer could not be found.", "OCSP Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var certId = new Org.BouncyCastle.Ocsp.CertificateID(Org.BouncyCastle.Ocsp.CertificateID.HashSha1, issuerBc, subjectBc.SerialNumber);
                bool anyResponderReached = false;
                bool certificateGood = false;
                bool certificateRevoked = false;
                bool definitiveResponse = false;
                foreach (string url in ocspUrls)
                {
                    try
                    {
                        var generator = new Org.BouncyCastle.Ocsp.OcspReqGenerator();
                        generator.AddRequest(certId);
                        var request = generator.Generate();
                        byte[] responseBytes;
                        using (var client = new HttpClient())
                        {
                            client.Timeout = TimeSpan.FromSeconds(20);
                            client.DefaultRequestHeaders.UserAgent.ParseAdd("CertificateChecker/1.0");
                            client.DefaultRequestHeaders.Accept.ParseAdd("application/ocsp-response");
                            using (var content = new ByteArrayContent(request.GetEncoded()))
                            {
                                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/ocsp-request");

                                HttpResponseMessage response = client.PostAsync(url, content).GetAwaiter().GetResult();

                                responseBytes = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();

                                if (!response.IsSuccessStatusCode)
                                {
                                    listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "Responder returned HTTP " + (int)response.StatusCode, url }));
                                    continue;
                                }
                            }
                        }
                        if (responseBytes == null || responseBytes.Length == 0)
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "Empty OCSP response", url }));
                            continue;
                        }
                        anyResponderReached = true;
                        Org.BouncyCastle.Ocsp.OcspResp ocspResp;
                        try
                        {
                            ocspResp = new Org.BouncyCastle.Ocsp.OcspResp(responseBytes);
                        }
                        catch
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "Invalid OCSP response", url }));
                            continue;
                        }
                        if (ocspResp.Status != Org.BouncyCastle.Ocsp.OcspRespStatus.Successful)
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "Responder returned status " + ocspResp.Status, url }));
                            continue;
                        }
                        var basicResp = ocspResp.GetResponseObject() as Org.BouncyCastle.Ocsp.BasicOcspResp;
                        if (basicResp == null)
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "Basic OCSP response not found", url }));
                            continue;
                        }
                        var matchingResponse = basicResp.Responses.FirstOrDefault(x => x.GetCertID().SerialNumber.Equals(subjectBc.SerialNumber));
                        if (matchingResponse == null)
                        {
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "No matching certificate status in response", url }));
                            continue;
                        }
                        var certStatus = matchingResponse.GetCertStatus();
                        if (certStatus == Org.BouncyCastle.Ocsp.CertificateStatus.Good)
                        {
                            certificateGood = true;
                            definitiveResponse = true;
                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "OK", "Responder returned GOOD", url }));
                            break;
                        }
                        if (certStatus is Org.BouncyCastle.Ocsp.RevokedStatus)
                        {
                            certificateRevoked = true;
                            definitiveResponse = true;

                            listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "Responder returned REVOKED", url }));
                            break;
                        }
                        definitiveResponse = true;
                        listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "Responder returned UNKNOWN", url }));
                        break;
                    }
                    catch
                    {
                        listView_VerificationDetail.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "OCSP responder could not be reached", url }));
                        continue;
                    }
                }
                if (certificateGood)
                {
                    pictureBox_OCSP.Image = Properties.Resources.ok;
                    MessageBox.Show("OCSP validation completed successfully.\r\n\r\nThe responder returned GOOD for the certificate.", "OCSP Recheck", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (certificateRevoked)
                {
                    pictureBox_OCSP.Image = Properties.Resources.error;
                    PictureBox_Durum.Image = Properties.Resources.error;
                    Textbox_Durum.Text = "Certificate is Invalid!\r\nOCSP responder reported that the certificate is revoked.";
                    MessageBox.Show("OCSP validation completed.\r\n\r\nThe certificate is REVOKED.", "OCSP Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (definitiveResponse)
                {
                    pictureBox_OCSP.Image = Properties.Resources.warning;
                    PictureBox_Durum.Image = Properties.Resources.warning;
                    Textbox_Durum.Text = "Certificate is Valid with Warning!\r\nOCSP returned no definitive GOOD status.";
                    MessageBox.Show("OCSP responder was reached, but it did not return a definitive GOOD status.", "OCSP Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!anyResponderReached)
                {
                    pictureBox_OCSP.Image = Properties.Resources.warning;
                    PictureBox_Durum.Image = Properties.Resources.warning;
                    Textbox_Durum.Text = "Certificate is Valid with Warning!\r\nOCSP responders could not be reached.";
                    MessageBox.Show("OCSP validation could not be completed.\r\n\r\nNone of the OCSP responders could be reached.", "OCSP Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                pictureBox_OCSP.Image = Properties.Resources.error;
                PictureBox_Durum.Image = Properties.Resources.error;
                Textbox_Durum.Text = "Certificate is Invalid!\r\nOCSP validation failed.";
                MessageBox.Show("OCSP validation failed.", "OCSP Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                pictureBox_OCSP.Image = Properties.Resources.error;
                PictureBox_Durum.Image = Properties.Resources.error;
                Textbox_Durum.Text = "Certificate is Invalid!\r\nOCSP recheck error.";
                MessageBox.Show("OCSP recheck failed.\r\n\r\n" + ex.Message, "OCSP Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_Result_Recheck_Click(object sender, EventArgs e) // "Recheck" butonuna tıklandığında tüm doğrulamaları yeniden çalıştıran metot. ---------------------------------------------------------
        {
            if (CurrentCertificate == null || string.IsNullOrWhiteSpace(File_Path))
            {
                MessageBox.Show("No certificate is currently loaded. Please select a certificate first.","Recheck",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            FileCertificateVerification();
        }
        private void button_crl_downloader_Click(object sender, EventArgs e) // CRL (Certificate Revocation List) dosyalarını indiren metot. ---------------------------------------------------------
        {
            DownloadCrlFiles();
        }
        private void button_aia_downloader_Click(object sender, EventArgs e) // AIA (Authority Information Access) dosyalarını indiren metot. ---------------------------------------------------------
        {
            DownloadAiaFiles();
        }
        private void button_ocsp_downloader_Click(object sender, EventArgs e) // OCSP (Online Certificate Status Protocol) yanıtlarını indiren metot. ---------------------------------------------------------
        {
            if (CurrentCertificate == null)
            {
                MessageBox.Show("No certificate is currently loaded.", "OCSP Downloader", MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }
            DownloadOcspResponses(CurrentCertificate);
        }
    }
}