using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System; //yazilimturkiye.com 03.07.2021 Tüm hakları saklıdır.
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Windows.Security.Cryptography.Certificates;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CertificateChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
        }
        string File_Path;//Seçilen dosyanın bilgilerini sakladığımız değişkenler.
        string File_Name;
        string File_Size;
        string File_Created;
        string File_Hash;
        private X509Certificate2 CurrentCertificate;
        private void CertificateDatesCheck(DateTime baslangic, DateTime bitis) //Sertifikanın başlangıç ve bitiş tarihlerini kontrol eden metot.
        {
            DateTime simdi = DateTime.Now;

            // Bitiş tarihi kontrolü (30 günden az kaldıysa kırmızı ve kalın)
            if ((bitis - simdi).TotalDays <= 30)
            {
                Textbox_Bitis.ForeColor = Color.Red;
            }
            else
            {
                Textbox_Bitis.ForeColor = SystemColors.WindowText;
            }

            // Başlangıç tarihi kontrolü (henüz başlamadıysa kırmızı)
            if (baslangic > simdi)
            {
                Textbox_Baslangic.ForeColor = Color.Red;
            }
            else
            {
                Textbox_Baslangic.ForeColor = SystemColors.WindowText;
            }
        }
        private string CalculateSHA256(string filePath) //Dosyanın SHA256 hash değerini hesaplayan metot.
        {
            using (FileStream stream = File.OpenRead(filePath))
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(stream);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        private string GetBasicConstraints(X509Certificate2 ConstrationsCert)//Basic Constraints bilgisini çeken metot.
        {
            foreach (var extension in ConstrationsCert.Extensions)
            {
                if (extension is X509BasicConstraintsExtension basicConstraints)
                {
                    return $"CA: {basicConstraints.CertificateAuthority}, " +
                           $"PathLength: {(basicConstraints.HasPathLengthConstraint ? basicConstraints.PathLengthConstraint.ToString() : "None")}";
                }
            }
            return "No constraints found in certificate.";
        }
        private string GetKeyUsage(X509Certificate2 ConstrationsCertUsage)//Key Usage bilgisini çeken metot.
        {
            string result = "";

            foreach (var extension in ConstrationsCertUsage.Extensions)//Sertifika uzantıları arasında Key Usage varsa bulan metot.
            {
                if (extension is X509KeyUsageExtension keyUsage)
                {
                    if (keyUsage.KeyUsages.HasFlag(X509KeyUsageFlags.DigitalSignature))
                        result += "Digital Signature, ";
                    if (keyUsage.KeyUsages.HasFlag(X509KeyUsageFlags.NonRepudiation))
                        result += "Non Repudiation, ";
                    if (keyUsage.KeyUsages.HasFlag(X509KeyUsageFlags.KeyEncipherment))
                        result += "Key Encipherment, ";
                    if (keyUsage.KeyUsages.HasFlag(X509KeyUsageFlags.DataEncipherment))
                        result += "Data Encipherment, ";
                    if (keyUsage.KeyUsages.HasFlag(X509KeyUsageFlags.KeyAgreement))
                        result += "Key Agreement, ";
                    if (keyUsage.KeyUsages.HasFlag(X509KeyUsageFlags.KeyCertSign))
                        result += "Certificate Signing, ";
                    if (keyUsage.KeyUsages.HasFlag(X509KeyUsageFlags.CrlSign))
                        result += "CRL Signing, ";
                    if (keyUsage.KeyUsages.HasFlag(X509KeyUsageFlags.EncipherOnly))
                        result += "Encipher Only, ";
                    if (keyUsage.KeyUsages.HasFlag(X509KeyUsageFlags.DecipherOnly))
                        result += "Decipher Only, ";
                    if (result.EndsWith(", "))
                        result = result.Substring(0, result.Length - 2);
                    return result;
                }
            }
            return "Not Found";
        }
        private string GetKeySize(X509Certificate2 cert) //Sertifikanın anahtar tipi ve boyunutu alan metot.
        {
            var rsaKey = cert.GetRSAPublicKey(); //RSA anahtar boyutunu almak
            if (rsaKey != null)
            {
                return $"RSA{rsaKey.KeySize}";
            }
            var ecKey = cert.GetECDsaPublicKey(); //EC anahtar boyutunu almak
            if (ecKey != null)
            {
                return $"ECC{ecKey.KeySize}";
            }
            return "Not Found"; //Desteklenmeyen anahtar türleri veya anahtar bulunamaması durumunda
        }
        private string GetSubjectKeyIdentifier(X509Certificate2 cert) //Sertifika Subjeck Key Identifier (SKI) bilgisini çeken metot.
        {
            foreach (System.Security.Cryptography.X509Certificates.X509Extension ext in cert.Extensions)
            {
                if (ext.Oid.Value == "2.5.29.14") // SKI OID
                {
                    var ski = (X509SubjectKeyIdentifierExtension)ext;
                    return ski.SubjectKeyIdentifier;
                }
            }
            return "Not Found";
        }
        private string GetAuthorityKeyIdentifier(X509Certificate2 cert) //Sertifika Authority Key Identifier (AKI) bilgisini çeken metot.
        {
            foreach (System.Security.Cryptography.X509Certificates.X509Extension ext in cert.Extensions)
            {
                if (ext.Oid.Value == "2.5.29.35") //AKI OID
                {
                    return BitConverter.ToString(ext.RawData).Replace("-", "");
                }
            }
            return "Not Found";
        }
        public void ParseCRLAddresses(X509Certificate2 cert) //Sertifikadan CRL adreslerini çeken metot.
        {
            try
            {
                var bcCert = new X509CertificateParser().ReadCertificate(cert.RawData);
                TextBox_CRL.Clear();
                var crlExt = bcCert.GetExtensionValue(X509Extensions.CrlDistributionPoints);
                if (crlExt != null)
                {
                    var asn1 = Asn1Object.FromByteArray(crlExt.GetOctets());
                    var crlDist = CrlDistPoint.GetInstance(asn1);

                    foreach (DistributionPoint dp in crlDist.GetDistributionPoints())
                    {
                        var dpName = dp.DistributionPointName;
                        if (dpName?.Type == DistributionPointName.FullName)
                        {
                            var genNames = GeneralNames.GetInstance(dpName.Name);
                            foreach (GeneralName name in genNames.GetNames())
                            {
                                if (name.TagNo == GeneralName.UniformResourceIdentifier)
                                {
                                    string uri = DerIA5String.GetInstance(name.Name).GetString();
                                    TextBox_CRL.AppendText(uri + Environment.NewLine);
                                }
                            }
                        }
                    }
                    if (string.IsNullOrWhiteSpace(TextBox_CRL.Text))
                        TextBox_CRL.Text = "No URI found";
                }
                else
                {
                    TextBox_CRL.Text = "Not Found";
                }
            }
            catch (Exception ex)
            {
                TextBox_CRL.Text = "Error: " + ex.Message;
            }
        }
        public void ParseAIAAddresses(X509Certificate2 cert) //Sertifikadan AIA adreslerini çeken metot.
        {
            try
            {
                var bcCert = new X509CertificateParser().ReadCertificate(cert.RawData);
                TextBox_AIA.Clear();

                var aiaExt = bcCert.GetExtensionValue(X509Extensions.AuthorityInfoAccess);
                if (aiaExt != null)
                {
                    var aiaAsn1 = Asn1Object.FromByteArray(aiaExt.GetOctets());
                    var aia = AuthorityInformationAccess.GetInstance(aiaAsn1);

                    foreach (AccessDescription ad in aia.GetAccessDescriptions())
                    {
                        if (ad.AccessMethod.Equals(AccessDescription.IdADCAIssuers))
                        {
                            if (ad.AccessLocation.TagNo == GeneralName.UniformResourceIdentifier)
                            {
                                string uri = DerIA5String.GetInstance(ad.AccessLocation.Name).GetString();
                                TextBox_AIA.AppendText(uri + Environment.NewLine);
                            }
                        }
                    }

                    if (string.IsNullOrWhiteSpace(TextBox_AIA.Text))
                        TextBox_AIA.Text = "No CA Issuers URI found";
                }
                else
                {
                    TextBox_AIA.Text = "Not Found";
                }
            }
            catch (Exception ex)
            {
                TextBox_AIA.Text = "Error: " + ex.Message;
            }
        }
        public void ParseOCSPAddresses(X509Certificate2 cert)
        {
            try
            {
                var bcCert = new X509CertificateParser().ReadCertificate(cert.RawData);
                TextBox_OCSP.Clear();

                var aiaExt = bcCert.GetExtensionValue(X509Extensions.AuthorityInfoAccess);
                if (aiaExt != null)
                {
                    var aiaAsn1 = Asn1Object.FromByteArray(aiaExt.GetOctets());
                    var aia = AuthorityInformationAccess.GetInstance(aiaAsn1);

                    foreach (AccessDescription ad in aia.GetAccessDescriptions())
                    {
                        if (ad.AccessMethod.Equals(AccessDescription.IdADOcsp))
                        {
                            if (ad.AccessLocation.TagNo == GeneralName.UniformResourceIdentifier)
                            {
                                string uri = DerIA5String.GetInstance(ad.AccessLocation.Name).GetString();
                                TextBox_OCSP.AppendText(uri + Environment.NewLine);
                            }
                        }
                    }

                    if (string.IsNullOrWhiteSpace(TextBox_OCSP.Text))
                        TextBox_OCSP.Text = "No OCSP URI found";
                }
                else
                {
                    TextBox_OCSP.Text = "Not Found";
                }
            }
            catch (Exception ex)
            {
                TextBox_OCSP.Text = "Error: " + ex.Message;
            }
        }
        private string GetEkuFriendlyName(string oid)//Extended Key Usage (EKU) OID'lerini kullanıcı dostu isimlere çeviren metot.
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
        private void ParseEKU(X509Certificate2 cert)//Sertifikadan Extended Key Usage (EKU) bilgilerini çeken metot.
        {
            try
            {
                var bcCert = new X509CertificateParser().ReadCertificate(cert.RawData);

                TextBox_EKU.Clear();

                var ekuList = bcCert.GetExtendedKeyUsage();
                if (ekuList != null && ekuList.Count > 0)
                {
                    foreach (DerObjectIdentifier oid in ekuList)
                    {
                        string friendly = GetEkuFriendlyName(oid.Id);
                        TextBox_EKU.AppendText($"{friendly} ({oid.Id}){Environment.NewLine}");
                    }
                }
                else
                {
                    TextBox_EKU.Text = "Not Found";
                }
            }
            catch (Exception ex)
            {
                TextBox_EKU.Text = "Error: " + ex.Message;
            }
        }
        private void ParseSAN(X509Certificate2 cert)//Sertifikadan Subject Alternative Name (SAN) bilgilerini çeken metot.
        {
            try
            {
                var bcCert = new X509CertificateParser().ReadCertificate(cert.RawData);
                var sanList = bcCert.GetSubjectAlternativeNames();

                TextBox_SAN.Clear();

                if (sanList == null || sanList.Count == 0)
                {
                    TextBox_SAN.Text = "Not Found";
                    return;
                }

                List<string> sanEntries = new List<string>();

                foreach (var rawEntry in sanList)
                {
                    if (rawEntry is IList<object> entry && entry.Count >= 2)
                    {
                        int tag = Convert.ToInt32(entry[0]);
                        string value = entry[1]?.ToString();

                        if (tag == 2 || tag == 7)
                        {
                            sanEntries.Add(value);
                        }
                    }
                }
                if (sanEntries.Count > 0)
                    TextBox_SAN.Text = string.Join(Environment.NewLine, sanEntries);
                else
                    TextBox_SAN.Text = "Not Found";
            }
            catch (Exception ex)
            {
                TextBox_SAN.Text = "Error: " + ex.Message;
            }
        }
        private void UpdateDownloadButtons() //CRL, AIA ve OCSP adreslerinin geçerliliğini kontrol eden metot.
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
        private List<string> GetUrlsFromTextBox(System.Windows.Forms.TextBox textBox) //CRL, AIA ve OCSP adreslerini TextBox'tan alıp geçerli URL'leri listeleyen metot.
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
        private string GetDesktopPath() //Kullanıcının masaüstü dizin yolunu döndüren metot.
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }
        private string GetSafeFileName(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(c, '_');

            return fileName;
        }
        private string GetUniqueFilePath(string directory, string fileName) //Belirtilen dizinde benzersiz bir dosya yolu döndüren metot. Eğer dosya zaten varsa, dosya adının sonuna bir sayı ekleyerek benzersiz bir ad oluşturur.
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
        private string GetFileNameFromUrl(string url, string defaultName) //URL'den dosya adını çıkaran metot. Eğer URL geçerli değilse veya dosya adı alınamıyorsa, varsayılan adı döndürür.
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
        private byte[] DownloadHttpData(string url) //HTTP veya HTTPS URL'sinden veri indiren metot.
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(20);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("CertificateChecker/1.0");
                client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
                return client.GetByteArrayAsync(url).GetAwaiter().GetResult();
            }
        }
        private void DownloadCrlFiles() //CRL dosyalarını indiren metot.
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
        private void DownloadAiaFiles() //AIA dosyalarını indiren metot.
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
        private void DownloadOcspResponses(X509Certificate2 cert) //OCSP yanıtlarını indiren metot.
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
        private void ClearUI() //Kullanıcı arayüzünü temizleyen metot.
        {
            Textbox_DosyaAdi.Clear();
            Textbox_DosyaYolu.Clear();
            TextBox_FileSize.Clear();
            TextBox_FileCreated.Clear();
            TextBox_FileHash.Clear();
            Textbox_Veren.Clear();
            Textbox_Verilen.Clear();
            Textbox_Baslangic.Clear();
            Textbox_Bitis.Clear();
            Textbox_Serino.Clear();
            TextBox_aki.Clear();
            TextBox_ski.Clear();
            Textbox_Algoritma.Clear();
            PictureBox_Durum.Image = null;
            pictureBox_AIA.Image = null;
            pictureBox_CDP.Image = null;
            pictureBox_OCSP.Image = null;
            Textbox_Durum.Clear();
            Textbox_Constraints.Clear();
            TextBox_Publickey.Clear();
            Textbox_Usage.Clear();
            TextBox_WebAdress.Clear();
            TextBox_IPAddress.Clear();
            TextBox_TLS.Clear();
            textBox_converter.Clear();
            TreeView_Chain.Nodes.Clear();
            TextBox_AIA.Clear();
            TextBox_CRL.Clear();
            TextBox_EKU.Clear();
            TextBox_OCSP.Clear();
            TextBox_SAN.Clear();
            Label_TotalChain.Text = "-";
            listView_DetailLog.Items.Clear();
            button_CPS.Enabled = false;
            Buton_Goruntule.Enabled = false;
            Button_GoToAddress.Enabled = false;
            CurrentCertificate = null;
            button_crl_downloader.Enabled = false;
            button_aia_downloader.Enabled = false;
            button_ocsp_downloader.Enabled = false;
            button_crl_recheck.Enabled = false;
            button_aia_recheck.Enabled = false;
            button_ocsp_recheck.Enabled = false;
            button_terminal.Enabled = false;
        }
        private void PlaceHolderText() //Bazı bileşenlere PlaceHolder metni ekleyen ve silen metot.
        {
            try
            {
                textBox_converter.Text = "Paste PEM or Base64 certificate text...";
                textBox_converter.ForeColor = Color.Gray;

                textBox_converter.Enter += (s, ev) =>
                {
                    if (textBox_converter.Text == "Paste PEM or Base64 certificate text...")
                    {
                        textBox_converter.Text = "";
                        textBox_converter.ForeColor = Color.Black;
                    }
                };

                textBox_converter.Leave += (s, ev) =>
                {
                    if (string.IsNullOrWhiteSpace(textBox_converter.Text))
                    {
                        textBox_converter.Text = "Paste PEM or Base64 certificate text...";
                        textBox_converter.ForeColor = Color.Gray;
                    }
                };
                TextBox_WebAdress.Text = "Paste Web Address with https://";
                TextBox_WebAdress.ForeColor = Color.Gray;

                TextBox_WebAdress.Enter += (s, ev) =>
                {
                    if (TextBox_WebAdress.Text == "Paste Web Address with https://")
                    {
                        TextBox_WebAdress.Text = "";
                        TextBox_WebAdress.ForeColor = Color.Black;
                    }
                };

                TextBox_WebAdress.Leave += (s, ev) =>
                {
                    if (string.IsNullOrWhiteSpace(TextBox_WebAdress.Text))
                    {
                        TextBox_WebAdress.Text = "Paste Web Address with https://";
                        TextBox_WebAdress.ForeColor = Color.Gray;
                    }
                };
            }
            catch (Exception)
            {
                ClearUI();
            }
        }
        private void CertificateChain(X509Certificate2 sertifika, System.Windows.Forms.TreeView treeView) //Sertifika Zinciri oluşturan metot.
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
        private string GetCommonName(string subject) //Sertifika konusundan Common Name (CN) bilgisini çeken metot.
        {
            var match = System.Text.RegularExpressions.Regex.Match(subject, @"CN\s*=\s*([^,]+)");
            return match.Success ? match.Groups[1].Value.Trim() : subject;
        }
        public void FileCertificateVerification()//Seçtiğimiz sertifikamızı doğrulamamızı sağlayan metot.
        {
            try
            {
                X509Certificate2 sertifika = new X509Certificate2(File_Path);
                CurrentCertificate = sertifika;
                Textbox_Veren.Text = sertifika.Issuer.ToString(); //Sertifikayı Veren Makam.
                Textbox_Verilen.Text = sertifika.Subject.ToString(); //Sertifikanın Konu Adı
                TextBox_ski.Text = GetSubjectKeyIdentifier(sertifika); //Sertifika Subject Key Identifier (SKI).
                TextBox_aki.Text = GetAuthorityKeyIdentifier(sertifika); //Sertifika Authority Key Identifier (AKI).
                Textbox_Baslangic.Text = sertifika.NotBefore.ToString(); //Başlangıç Tarihi.
                Textbox_Bitis.Text = sertifika.NotAfter.ToString(); //Bitiş Tarihi.
                Textbox_Serino.Text = sertifika.SerialNumber.ToString(); //Sertifika Seri no.
                Textbox_Algoritma.Text = sertifika.SignatureAlgorithm.FriendlyName.ToString(); // sertifikanın algoritması.
                Textbox_Constraints.Text = GetBasicConstraints(sertifika); //Sertifika Temel Kısıtları.
                Textbox_Usage.Text = GetKeyUsage(sertifika); //Anahtar Kullanım amaçları.
                TextBox_Publickey.Text = GetKeySize(sertifika); //Anahtar tipi ve boyutu.
                X509Chain chain = new X509Chain(); //Sertifika zincirini oluştur
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online; //Sertifika iptal durumu kontrolü (revocation check)
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                CertificateChain(sertifika, TreeView_Chain); //Zinciri ekleyen metodu çağır.
                ParseCRLAddresses(sertifika);//CDP adreslerini çeken metot.
                ParseAIAAddresses(sertifika); //AIA adreslerini çeken metot.
                ParseOCSPAddresses(sertifika); //OCSP adreslerini çeken metot.
                ParseEKU(sertifika); //Extended Key Usage (EKU) bilgilerini çeken metot.
                ParseSAN(sertifika); //Subject Alternative Name (SAN) bilgilerini çeken metot.
                CertificateDatesCheck(sertifika.NotBefore, sertifika.NotAfter); //Sertifikanın bitiş tarihini kontrol eden metot.
                CheckCdpValidation(sertifika); //CDP adreslerinin geçerliliğini kontrol eden metot.
                CheckAiaValidation(sertifika); //AIA adreslerinin geçerliliğini kontrol eden metot.
                CheckOcspValidation(sertifika); //OCSP adreslerinin geçerliliğini kontrol eden metot.
                UpdateDownloadButtons(); //CRL, AIA ve OCSP adreslerinin geçerliliğini kontrol eden metot.
                bool isChainValid = chain.Build(sertifika);
                if (isChainValid) //Sertifika Geçerli
                {
                    Textbox_Durum.Text = "Certificate is Valid.";
                    PictureBox_Durum.Image = Properties.Resources.ok;
                }
                else //Zincir geçersiz, sebebi açıklanıyor.
                {
                    Textbox_Durum.Text = "Certificate is Invalid!";
                    PictureBox_Durum.Image = Properties.Resources.error;
                    foreach (X509ChainStatus status in chain.ChainStatus)
                    {
                        if (status.Status == X509ChainStatusFlags.NotTimeValid) //eğer expired ise bu adıma geçecek.
                        {
                            DateTime simdiki_zaman = DateTime.Now;
                            DateTime baslangic_zamani = DateTime.Parse(Textbox_Baslangic.Text);
                            DateTime bitis_zamani = DateTime.Parse(Textbox_Bitis.Text);
                            if (baslangic_zamani > simdiki_zaman) //zaman ileride ise.
                            {
                                PictureBox_Durum.Image = Properties.Resources.ok;
                                Textbox_Durum.Text = "Certificate is Not Yet Valid!";
                            }
                            else if (bitis_zamani < simdiki_zaman) //zaman geride ise.
                            {
                                PictureBox_Durum.Image = Properties.Resources.error;
                                Textbox_Durum.Text = "Certificate is Expired!";
                            }
                            break;
                        }
                        else if (status.Status == X509ChainStatusFlags.Revoked) //sertifika iptal ise.
                        {
                            Textbox_Durum.Text = "Certificate is Revoked!";
                            break;
                        }
                        else if (status.Status == X509ChainStatusFlags.UntrustedRoot) //kök güvenilmez ise.
                        {
                            Textbox_Durum.Text = "Untrusted Root!";
                            break;
                        }
                        else
                        {
                            Textbox_Durum.Text = "Invalid: " + status.StatusInformation;
                        }
                    }
                }
            }
            catch (Exception) //sertifika dosyası dışında farklı bir dosya seçmesi durumunda kullanıcıyı uyaran bölüm.
            {
                MessageBox.Show("Please select a valid certificate file. Valid file extensions:\n.cer, .crt, .exe, .pem, etc.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void CheckCdpValidation(X509Certificate2 cert) //CDP adreslerinin geçerliliğini kontrol eden metot.
        {
            bool anyCdpFound = false;
            bool anyCdpReachable = false;
            try
            {
                var lines = TextBox_CRL.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l) && l != "No URI found" && l != "Not Found" && !l.StartsWith("ldap://", StringComparison.OrdinalIgnoreCase)).ToList();
                if (lines.Count == 0)
                {
                    pictureBox_CDP.Image = Properties.Resources.warning;
                    listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Warning", "CDP not available", "-" }));
                    return;
                }
                anyCdpFound = true;
                var parser = new Org.BouncyCastle.X509.X509CrlParser();
                var bcParser = new Org.BouncyCastle.X509.X509CertificateParser();
                foreach (var url in lines)
                {
                    try
                    {
                        if (!(url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("file://", StringComparison.OrdinalIgnoreCase)))
                            continue;
                        byte[] crlBytes;
                        if (url.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
                        {
                            crlBytes = File.ReadAllBytes(new Uri(url).LocalPath);
                        }
                        else
                        {
                            using (var wc = new WebClient())
                            {
                                wc.Headers.Add("User-Agent", "CertificateChecker/1.0");
                                crlBytes = wc.DownloadData(url);
                            }
                        }
                        if (crlBytes == null || crlBytes.Length == 0)
                            continue;
                        anyCdpReachable = true;
                        var crl = parser.ReadCrl(crlBytes);
                        var serial = new Org.BouncyCastle.Math.BigInteger(cert.SerialNumber, 16);
                        var revoked = crl.GetRevokedCertificate(serial);
                        Org.BouncyCastle.X509.X509Certificate issuerBc = null;
                        try
                        {
                            X509Chain chain = new X509Chain();
                            chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                            chain.Build(cert);
                            foreach (X509ChainElement elem in chain.ChainElements)
                            {
                                var bc = bcParser.ReadCertificate(elem.Certificate.RawData);
                                if (bc.SubjectDN.Equivalent(crl.IssuerDN))
                                {
                                    issuerBc = bc;
                                    break;
                                }
                            }
                        }
                        catch { }
                        if (issuerBc == null)
                        {
                            pictureBox_CDP.Image = Properties.Resources.error;
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "Issuer not found", url }));
                            return;
                        }
                        try
                        {
                            crl.Verify(issuerBc.GetPublicKey());
                        }
                        catch
                        {
                            pictureBox_CDP.Image = Properties.Resources.error;
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL signature invalid", url }));
                            return;
                        }
                        if (revoked != null)
                        {
                            pictureBox_CDP.Image = Properties.Resources.error;
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "Certificate revoked", url }));
                            return;
                        }
                        pictureBox_CDP.Image = Properties.Resources.ok;
                        listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "OK", "CRL verified", url }));
                        return;
                    }
                    catch
                    {
                        continue;
                    }
                }
                if (anyCdpFound && !anyCdpReachable)
                {
                    pictureBox_CDP.Image = Properties.Resources.warning;
                    listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Warning", "CRL unreachable", "-" }));
                    return;
                }

                pictureBox_CDP.Image = Properties.Resources.error;
                listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "Validation failed", "-" }));
            }
            catch
            {
                pictureBox_CDP.Image = Properties.Resources.error;
                listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "Unexpected error", "-" }));
            }
        }
        private void CheckAiaValidation(X509Certificate2 cert)// AIA doğrulamasını yapan metot
        {
            List<string> aiaUrls = new List<string>();
            bool windowsTrusted = false;
            try
            {
                aiaUrls = TextBox_AIA.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l) && l != "No URI found" && l != "Not Found").Select(l => { int http = l.IndexOf("http://", StringComparison.OrdinalIgnoreCase); int https = l.IndexOf("https://", StringComparison.OrdinalIgnoreCase); int idx = http >= 0 ? http : https; return idx >= 0 ? l.Substring(idx) : null; }).Where(u => !string.IsNullOrWhiteSpace(u)).ToList();
                X509Chain chain = new X509Chain();
                chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;
                chain.ChainPolicy.UrlRetrievalTimeout = TimeSpan.FromSeconds(10);
                windowsTrusted = chain.Build(cert);
                if (windowsTrusted)
                {
                    if (aiaUrls.Count == 0)
                        pictureBox_AIA.Image = Properties.Resources.warning;
                    else
                        pictureBox_AIA.Image = Properties.Resources.ok;
                }
                else
                {
                    pictureBox_AIA.Image = Properties.Resources.error;
                }
                if (aiaUrls.Count > 0)
                {
                    foreach (var url in aiaUrls)
                    {
                        listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", windowsTrusted ? (aiaUrls.Count > 0 ? "OK" : "Warning") : "Error", windowsTrusted ? "Issuer trusted (Windows policy)" : "Issuer not trusted", url }));
                    }
                }
                else
                {
                    listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", windowsTrusted ? "Warning" : "Error", windowsTrusted ? "No AIA URL, Windows policy used" : "AIA not available", "-" }));
                }
            }
            catch
            {
                pictureBox_AIA.Image = Properties.Resources.error;
                listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", "Error", "AIA validation error", "-" }));
            }
        }
        private void CheckOcspValidation(X509Certificate2 cert) // OCSP doğrulamasını yapan metot
        {
            List<string> ocspUrls = new List<string>();
            bool anyResponderReached = false;
            bool ocspGood = false;
            bool ocspRevoked = false;
            try
            {
                ocspUrls = TextBox_OCSP.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l) && l != "No URI found" && l != "Not Found").Select(l => { int http = l.IndexOf("http://", StringComparison.OrdinalIgnoreCase); int https = l.IndexOf("https://", StringComparison.OrdinalIgnoreCase); int idx = http >= 0 ? http : https; return idx >= 0 ? l.Substring(idx) : null; }).Where(u => !string.IsNullOrWhiteSpace(u)).ToList();
                var bcParser = new Org.BouncyCastle.X509.X509CertificateParser();
                var subjectBc = bcParser.ReadCertificate(cert.RawData);
                Org.BouncyCastle.X509.X509Certificate issuerBc = null;
                try
                {
                    X509Chain chain = new X509Chain();
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                    chain.Build(cert);
                    if (chain.ChainElements.Count > 1) issuerBc = bcParser.ReadCertificate(chain.ChainElements[1].Certificate.RawData);
                }
                catch { }
                if (issuerBc != null && ocspUrls.Count > 0)
                {
                    foreach (var url in ocspUrls)
                    {
                        try
                        {
                            var certId = new Org.BouncyCastle.Ocsp.CertificateID(Org.BouncyCastle.Ocsp.CertificateID.HashSha1, issuerBc, subjectBc.SerialNumber);
                            var gen = new Org.BouncyCastle.Ocsp.OcspReqGenerator();
                            gen.AddRequest(certId);
                            var req = gen.Generate();
                            byte[] respBytes;
                            using (var wc = new WebClient())
                            {
                                wc.Headers.Add("Content-Type", "application/ocsp-request");
                                wc.Headers.Add("Accept", "application/ocsp-response");
                                wc.Headers.Add("User-Agent", "CertificateChecker/1.0");
                                respBytes = wc.UploadData(url, req.GetEncoded());
                            }
                            anyResponderReached = true;
                            var ocspResp = new Org.BouncyCastle.Ocsp.OcspResp(respBytes);
                            if (ocspResp.Status != 0)
                                continue;
                            var basic = (Org.BouncyCastle.Ocsp.BasicOcspResp)ocspResp.GetResponseObject();
                            var single = basic.Responses.FirstOrDefault();
                            if (single == null)
                                continue;
                            var status = single.GetCertStatus();
                            if (status == Org.BouncyCastle.Ocsp.CertificateStatus.Good)
                            {
                                ocspGood = true;
                                listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "OK", "Responder returned GOOD", url }));
                                break;
                            }
                            else
                            {
                                ocspRevoked = true;
                                listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "Responder returned REVOKED", url }));
                                break;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                if (ocspGood)
                {
                    pictureBox_OCSP.Image = Properties.Resources.ok;
                    return;
                }
                if (ocspRevoked)
                {
                    pictureBox_OCSP.Image = Properties.Resources.error;
                    return;
                }
                X509Chain winChain = new X509Chain();
                winChain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                winChain.ChainPolicy.RevocationFlag = X509RevocationFlag.EndCertificateOnly;
                winChain.ChainPolicy.UrlRetrievalTimeout = TimeSpan.FromSeconds(10);
                bool windowsOk = winChain.Build(cert);
                if (windowsOk)
                    pictureBox_OCSP.Image = Properties.Resources.warning;
                else
                    pictureBox_OCSP.Image = Properties.Resources.error;

                if (ocspUrls.Count == 0)
                {
                    listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", windowsOk ? "Warning" : "Error", "No OCSP URL, Windows policy used", "-" }));
                }
                else
                {
                    foreach (var url in ocspUrls)
                    {
                        listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", windowsOk ? "Warning" : "Error", anyResponderReached ? "No definitive OCSP result, Windows policy used" : "OCSP unreachable, Windows policy used", url }));
                    }
                }
            }
            catch
            {
                pictureBox_OCSP.Image = Properties.Resources.error;
                listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "OCSP validation error", "-" }));
            }
        }
        private async void Buton_Dosya_Sec_Click(object sender, EventArgs e)//dosya seçme işleminin yapıldığı kısım.
        {
            OpenFileDialog DosyaAc = new OpenFileDialog();
            DosyaAc.Title = "Please Select Certificate File...";
            DosyaAc.Filter = "Certificate Files (*.cer;*.cert;*.crt;*.pem;*.der;*.p7b;*.p7c;*.pfx;*.key)|*.cer;*.cert;*.crt;*.pem;*.der;*.p7b;*.p7c;*.pfx;*.key|All Files (*.*)|*.*";
            DosyaAc.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DosyaAc.Multiselect = false;
            if (DosyaAc.ShowDialog() == DialogResult.OK)
            {
                ClearUI();
                File_Name = DosyaAc.SafeFileName;
                File_Path = DosyaAc.FileName;
                FileInfo DosyaAcInfo = new FileInfo(File_Path);
                File_Size = DosyaAcInfo.Length.ToString() + " bytes";
                File_Created = DosyaAcInfo.CreationTime.ToString();
                File_Hash = CalculateSHA256(File_Path);
                Textbox_DosyaAdi.Text = File_Name;
                Textbox_DosyaYolu.Text = File_Path;
                TextBox_FileSize.Text = File_Size;
                TextBox_FileCreated.Text = File_Created;
                TextBox_FileHash.Text = File_Hash;
                FileCertificateVerification();
                Buton_Goruntule.Enabled = true;
                button_CPS.Enabled = true;
                button_terminal.Enabled = true;
            }
        }
        private void button_Goruntule_Click(object sender, EventArgs e)//Sertifika görüntüleme
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
        private void Form1_Load(object sender, EventArgs e)//form başlangıç.
        {
            Buton_Goruntule.Enabled = false;
            button_CPS.Enabled = false;
            Button_GoToAddress.Enabled = false;
            button_terminal.Enabled = false;
            comboBox_verifymethod.Items.Add("File Verification");
            comboBox_verifymethod.Items.Add("Web Address Verification");
            comboBox_verifymethod.Items.Add("PEM/Base64 Verification");
            comboBox_verifymethod.SelectedIndex = 0; //İlk seçeneği varsayılan olarak seç.
            GroupBox_PemBase64.Visible = false; //PEM-Base64 seçeneği için grup başlangıçta gizli olacak.
            GroupBox_ScanWebAddress.Visible = false; //Web adresi tarama seçeneği için grup başlangıçta gizli olacak.
        }
        private void button_CPS_Click(object sender, EventArgs e)//Eğer sertifika bir "Issuer Statement" değerine sahip ise bunu tarayıcıda açan kod bloğu.
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
        private string GetCertificatePolicyUrl(X509Certificate2 cert)//Sertifika politikalarından URL'yi ayıklayan metot.
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
        private string ExtractBase64FromPem(string pem)//PEM formatındaki sertifikadan base64 kısmını ayıklayan metot.
        {
            var lines = pem.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var base64Lines = lines.Where(line => !line.StartsWith("-----BEGIN") && !line.StartsWith("-----END")).ToArray();
            return string.Join("", base64Lines);
        }
        private void textBox_converter_TextChanged(object sender, EventArgs e)//PEM/Base64 formatındaki sertifikayı doğrulayan metot.
        {
            try
            {
                string pem = textBox_converter.Text.Trim();
                if (string.IsNullOrWhiteSpace(pem) || pem.Length < 100)
                    return;
                string base64 = ExtractBase64FromPem(pem);
                byte[] certBytes = Convert.FromBase64String(base64);
                X509Certificate2 sertifika = new X509Certificate2(certBytes);
                CurrentCertificate = sertifika;
                listView_DetailLog.Items.Clear();
                Textbox_Veren.Text = sertifika.Issuer;
                Textbox_Verilen.Text = sertifika.Subject;
                Textbox_Baslangic.Text = sertifika.NotBefore.ToString();
                Textbox_Bitis.Text = sertifika.NotAfter.ToString();
                Textbox_Serino.Text = sertifika.SerialNumber;
                Textbox_Algoritma.Text = sertifika.SignatureAlgorithm.FriendlyName;
                Textbox_Constraints.Text = GetBasicConstraints(sertifika);
                Textbox_Usage.Text = GetKeyUsage(sertifika);
                TextBox_Publickey.Text = GetKeySize(sertifika);
                TextBox_ski.Text = GetSubjectKeyIdentifier(sertifika);
                TextBox_aki.Text = GetAuthorityKeyIdentifier(sertifika);
                X509Chain chain = new X509Chain(); //Sertifika zincirini oluştur
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                CertificateChain(sertifika, TreeView_Chain); //Zinciri ekleyen metodu çağır.
                ParseCRLAddresses(sertifika);//CRL adreslerini çeken metot.
                ParseAIAAddresses(sertifika); //AIA adreslerini çeken metot.
                ParseOCSPAddresses(sertifika); //OCSP adreslerini çeken metot.
                ParseEKU(sertifika); //Extended Key Usage (EKU) bilgilerini çeken metot.
                ParseSAN(sertifika); //Subject Alternative Name (SAN) bilgilerini çeken metot.
                CertificateDatesCheck(sertifika.NotBefore, sertifika.NotAfter);//Sertifikanın bitiş tarihini kontrol eden metot.
                CheckCdpValidation(sertifika); //CDP adreslerinin geçerliliğini kontrol eden metot.
                CheckAiaValidation(sertifika); //AIA adreslerinin geçerliliğini kontrol eden metot.
                CheckOcspValidation(sertifika); //OCSP adreslerinin geçerliliğini kontrol eden metot.
                UpdateDownloadButtons(); //CRL, AIA ve OCSP adreslerinin geçerliliğini kontrol eden metot.
                bool isChainValid = chain.Build(sertifika);
                if (isChainValid) //Sertifika Geçerli
                {
                    Textbox_Durum.Text = "Certificate is Valid.";
                    PictureBox_Durum.Image = Properties.Resources.ok;
                }
                else //Zincir geçersiz, sebebi açıklanıyor.
                {
                    Textbox_Durum.Text = "Certificate is Invalid!";
                    PictureBox_Durum.Image = Properties.Resources.error;

                    foreach (X509ChainStatus status in chain.ChainStatus)
                    {
                        if (status.Status == X509ChainStatusFlags.NotTimeValid) //eğer expired ise bu adıma geçecek.
                        {
                            DateTime simdiki_zaman = DateTime.Now;
                            DateTime baslangic_zamani = DateTime.Parse(Textbox_Baslangic.Text);
                            DateTime bitis_zamani = DateTime.Parse(Textbox_Bitis.Text);
                            if (baslangic_zamani > simdiki_zaman) //zaman ileride ise.
                            {
                                PictureBox_Durum.Image = Properties.Resources.error;
                                Textbox_Durum.Text = "Certificate is Not Yet Valid!";
                            }
                            else if (bitis_zamani < simdiki_zaman) //zaman geride ise.
                            {
                                PictureBox_Durum.Image = Properties.Resources.error;
                                Textbox_Durum.Text = "Certificate is Expired!";
                            }
                            break;
                        }
                        else if (status.Status == X509ChainStatusFlags.Revoked) //sertifika iptal ise.
                        {
                            Textbox_Durum.Text = "Certificate is Revoked!";
                            break;
                        }
                        else if (status.Status == X509ChainStatusFlags.UntrustedRoot) //kök güvenilmez ise.
                        {
                            Textbox_Durum.Text = "Untrusted Root!";
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
                MessageBox.Show("Please select a valid certificate format.\nValid formats: Pem or Base64", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void comboBox_verifymethod_SelectedIndexChanged(object sender, EventArgs e)//Kullanıcının seçtiği doğrulama yöntemine göre arayüzü güncelleyen metot.
        {
            if (comboBox_verifymethod.SelectedIndex == 0)
            {
                GroupBox_FileSelect.Visible = true;
                GroupBox_PemBase64.Visible = false;
                GroupBox_ScanWebAddress.Visible = false;
                ClearUI();
                PlaceHolderText();
            }
            if (comboBox_verifymethod.SelectedIndex == 1)
            {
                GroupBox_ScanWebAddress.Visible = true;
                GroupBox_PemBase64.Visible = false;
                GroupBox_FileSelect.Visible = false;
                ClearUI();
                PlaceHolderText();
            }
            if (comboBox_verifymethod.SelectedIndex == 2)
            {
                GroupBox_PemBase64.Visible = true;
                GroupBox_FileSelect.Visible = false;
                GroupBox_ScanWebAddress.Visible = false;
                ClearUI();
                PlaceHolderText();
            }
            else
            {

            }
        }
        private (X509Certificate2 cert, SslProtocols tlsVersiyon) GetRemoteCertificateWithTls(string host, int port) //Web sunucusuna bağlan, SSL sertifikasını ve TLS sürümünü çek
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(host, port);
                using (SslStream sslStream = new SslStream(client.GetStream(), false, (sender, cert, chain, errors) => true))
                {
                    sslStream.AuthenticateAsClient(host);
                    System.Security.Cryptography.X509Certificates.X509Certificate cert = sslStream.RemoteCertificate;
                    SslProtocols protokol = sslStream.SslProtocol;
                    return (new X509Certificate2(cert), protokol);
                }
            }
        }
        private void Button_ScanWebAddress_Click(object sender, EventArgs e) //Kullanıcının girdiği web adresini tarayan ve sertifika bilgilerini çeken metot.
        {
            try
            {
                string url = TextBox_WebAdress.Text.Trim();

                if (!url.StartsWith("https://"))
                {
                    MessageBox.Show("Please enter start with https://", "Wrong URL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Uri uri = new Uri(url);
                IPAddress[] ipAddresses = Dns.GetHostAddresses(uri.Host);
                if (ipAddresses.Length > 0)
                    TextBox_IPAddress.Text = ipAddresses[0].ToString();
                else
                    TextBox_IPAddress.Text = "Not Found";

                var (sertifika, tlsVersiyon) = GetRemoteCertificateWithTls(uri.Host, uri.Port == -1 ? 443 : uri.Port);
                TextBox_TLS.Text = tlsVersiyon.ToString();

                if (sertifika == null)
                {
                    Textbox_Durum.Text = "Certificate is not taken.";
                    PictureBox_Durum.Image = Properties.Resources.error;
                    return;
                }
                CurrentCertificate = sertifika;
                listView_DetailLog.Items.Clear();
                Textbox_Veren.Text = sertifika.Issuer;
                Textbox_Verilen.Text = sertifika.Subject;
                Textbox_Baslangic.Text = sertifika.NotBefore.ToString();
                Textbox_Bitis.Text = sertifika.NotAfter.ToString();
                Textbox_Serino.Text = sertifika.SerialNumber;
                Textbox_Algoritma.Text = sertifika.SignatureAlgorithm.FriendlyName;
                Textbox_Constraints.Text = GetBasicConstraints(sertifika);
                Textbox_Usage.Text = GetKeyUsage(sertifika);
                TextBox_Publickey.Text = GetKeySize(sertifika);
                TextBox_ski.Text = GetSubjectKeyIdentifier(sertifika);
                TextBox_aki.Text = GetAuthorityKeyIdentifier(sertifika);
                X509Chain chain = new X509Chain();
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                CertificateChain(sertifika, TreeView_Chain); //Zinciri ekleyen metodu çağır.
                ParseCRLAddresses(sertifika);//CRL adreslerini çeken metot.
                ParseAIAAddresses(sertifika); //AIA adreslerini çeken metot.
                ParseOCSPAddresses(sertifika); //OCSP adreslerini çeken metot.
                ParseEKU(sertifika); //Extended Key Usage (EKU) bilgilerini çeken metot.
                ParseSAN(sertifika); //Subject Alternative Name (SAN) bilgilerini çeken metot.
                CheckCdpValidation(sertifika);//CDP adreslerinin geçerliliğini kontrol eden metot.
                CheckAiaValidation(sertifika);//AIA adreslerinin geçerliliğini kontrol eden metot.
                CheckOcspValidation(sertifika);//OCSP adreslerinin geçerliliğini kontrol eden metot.
                CertificateDatesCheck(sertifika.NotBefore, sertifika.NotAfter);//Sertifikanın bitiş tarihini kontrol eden metot.
                UpdateDownloadButtons(); //CRL, AIA ve OCSP adreslerinin geçerliliğini kontrol eden metot.
                bool isChainValid = chain.Build(sertifika);
                if (isChainValid)
                {
                    Textbox_Durum.Text = "Certificate is Valid.";
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
                            DateTime now = DateTime.Now;
                            if (sertifika.NotBefore > now)
                                Textbox_Durum.Text = "Certificate is Not Yet Valid!";
                            else if (sertifika.NotAfter < now)
                                Textbox_Durum.Text = "Certificate is Expired!";
                        }
                        else if (status.Status == X509ChainStatusFlags.Revoked)
                        {
                            Textbox_Durum.Text = "Certificate is Revoked!";
                        }
                        else if (status.Status == X509ChainStatusFlags.UntrustedRoot)
                        {
                            Textbox_Durum.Text = "Untrusted Root!";
                        }
                        else
                        {
                            Textbox_Durum.Text = "Invalid: " + status.StatusInformation;
                        }
                    }
                }
                Button_GoToAddress.Enabled = true; //Web adresi tarama başarılı ise butonu aktif et.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Conneciton Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Button_GoToAddress_Click(object sender, EventArgs e) //Kullanıcının girdiği web adresini varsayılan tarayıcıda açan metot.
        {
            try
            {
                Process sayfa_ac = new Process();
                sayfa_ac.StartInfo.UseShellExecute = true;
                sayfa_ac.StartInfo.FileName = TextBox_WebAdress.Text;
                sayfa_ac.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Invalid Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void button_crl_downloader_Click(object sender, EventArgs e) //CRL dosyalarını indiren metot.
        {
            DownloadCrlFiles();
        }

        private void button_aia_downloader_Click(object sender, EventArgs e)
        {
            DownloadAiaFiles();
        }

        private void button_ocsp_downloader_Click(object sender, EventArgs e)
        {
            if (CurrentCertificate == null)
            {
                MessageBox.Show("No certificate is currently loaded.", "OCSP Downloader", MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }
            DownloadOcspResponses(CurrentCertificate);
        }

        private void button_crl_recheck_Click(object sender, EventArgs e) //CRL adreslerini yeniden kontrol eden metot.
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
                    listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Warning", "No CRL address found", "-" }));
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
                    listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL issuer certificate not found", "-" }));
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
                                listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Warning", "CRL file not found", url }));
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
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Warning", "CRL response is empty", url }));
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
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "Downloaded data is not a valid CRL", url }));
                            continue;
                        }
                        if (crl == null)
                        {
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL could not be parsed", url }));
                            continue;
                        }
                        if (!crl.IssuerDN.Equivalent(issuerBc.SubjectDN))
                        {
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL issuer mismatch", url }));
                            continue;
                        }
                        try
                        {
                            crl.Verify(issuerBc.GetPublicKey());
                        }
                        catch
                        {
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL signature invalid", url }));
                            continue;
                        }
                        anyValidCrl = true;
                        var revoked = crl.GetRevokedCertificate(serial);
                        if (revoked != null)
                        {
                            certificateRevoked = true;
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "Certificate revoked", url }));
                            break;
                        }
                        listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "OK", "CRL verified", url }));
                        break;
                    }
                    catch (Exception ex)
                    {
                        unreachableUrls.Add(url);
                        listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Warning", "CRL could not be reached", url }));
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
                listView_DetailLog.Items.Add(new ListViewItem(new[] { "CDP", "Error", "CRL recheck exception", "-" }));
                MessageBox.Show("CRL recheck failed.\r\n\r\n" + ex.Message, "CRL Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_aia_recheck_Click(object sender, EventArgs e) //AIA adreslerini yeniden kontrol eden metot.
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

                    listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", "Warning", "No AIA address found", "-" }));

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
                                listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", "Warning", "Issuer file not found", url }));
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
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", "Warning", "Issuer response is empty", url }));
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
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", "Error", "Downloaded data is not a valid certificate", url }));
                            anyInvalidIssuer = true;
                            continue;
                        }

                        if (issuerBc == null)
                        {
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", "Error", "Issuer certificate could not be parsed", url }));
                            anyInvalidIssuer = true;
                            continue;
                        }
                        if (!subjectBc.IssuerDN.Equivalent(issuerBc.SubjectDN))
                        {
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", "Error", "Issuer subject does not match certificate issuer", url }));
                            anyInvalidIssuer = true;
                            continue;
                        }
                        try
                        {
                            subjectBc.Verify(issuerBc.GetPublicKey());
                        }
                        catch
                        {
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", "Error", "Issuer signature invalid", url }));
                            anyInvalidIssuer = true;
                            continue;
                        }
                        anyValidIssuer = true;
                        listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", "OK", "Issuer verified successfully", url }));
                        break;
                    }
                    catch
                    {
                        listView_DetailLog.Items.Add(new ListViewItem(new[] { "AIA", "Warning", "Issuer could not be reached", url }));
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

        private void button_ocsp_recheck_Click(object sender, EventArgs e) //OCSP adreslerini yeniden kontrol eden metot.
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
                    listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "No OCSP address found", "-" }));
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
                    listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "OCSP issuer certificate not found", "-" }));
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
                                    listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "Responder returned HTTP " + (int)response.StatusCode, url }));
                                    continue;
                                }
                            }
                        }
                        if (responseBytes == null || responseBytes.Length == 0)
                        {
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "Empty OCSP response", url }));
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
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "Invalid OCSP response", url }));
                            continue;
                        }
                        if (ocspResp.Status != Org.BouncyCastle.Ocsp.OcspRespStatus.Successful)
                        {
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "Responder returned status " + ocspResp.Status, url }));
                            continue;
                        }
                        var basicResp = ocspResp.GetResponseObject() as Org.BouncyCastle.Ocsp.BasicOcspResp;
                        if (basicResp == null)
                        {
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "Basic OCSP response not found", url }));
                            continue;
                        }
                        var matchingResponse = basicResp.Responses.FirstOrDefault(x => x.GetCertID().SerialNumber.Equals(subjectBc.SerialNumber));
                        if (matchingResponse == null)
                        {
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "No matching certificate status in response", url }));
                            continue;
                        }
                        var certStatus = matchingResponse.GetCertStatus();
                        if (certStatus == Org.BouncyCastle.Ocsp.CertificateStatus.Good)
                        {
                            certificateGood = true;
                            definitiveResponse = true;
                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "OK", "Responder returned GOOD", url }));
                            break;
                        }
                        if (certStatus is Org.BouncyCastle.Ocsp.RevokedStatus)
                        {
                            certificateRevoked = true;
                            definitiveResponse = true;

                            listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Error", "Responder returned REVOKED", url }));
                            break;
                        }
                        definitiveResponse = true;
                        listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "Responder returned UNKNOWN", url }));
                        break;
                    }
                    catch
                    {
                        listView_DetailLog.Items.Add(new ListViewItem(new[] { "OCSP", "Warning", "OCSP responder could not be reached", url }));
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

        private void button_eku_recheck_Click(object sender, EventArgs e) //EKU adreslerini yeniden kontrol eden metot.
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

        private void button_san_recheck_Click(object sender, EventArgs e) //SAN adreslerini yeniden kontrol eden metot.
        {
            if (CurrentCertificate == null)
            {
                MessageBox.Show("No certificate is currently loaded.", "SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var sanExtension = CurrentCertificate.Extensions["2.5.29.17"];
            if (sanExtension == null)
            {
                MessageBox.Show("The certificate does not contain a Subject Alternative Name (SAN) extension.\n\nSAN Check: FAILED", "SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var dnsNames = new List<string>();
            var ipAddresses = new List<string>();
            try
            {
                var bcCert = DotNetUtilities.FromX509Certificate(CurrentCertificate);
                var sanEntries = bcCert.GetSubjectAlternativeNames();

                foreach (var entry in sanEntries)
                {
                    if (entry.Count < 2)
                        continue;

                    int type = Convert.ToInt32(entry[0]);
                    string value = entry[1]?.ToString()?.Trim();

                    if (string.IsNullOrWhiteSpace(value))
                        continue;

                    if (type == 2)
                    {
                        string dns = value.TrimEnd('.').ToLowerInvariant();

                        if (System.Net.Dns.GetHostName() != null)
                            dnsNames.Add(dns);
                    }
                    else if (type == 7)
                    {
                        if (System.Net.IPAddress.TryParse(value, out _))
                            ipAddresses.Add(value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SAN could not be parsed.\n\n" + ex.Message, "SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dnsNames.Count == 0 && ipAddresses.Count == 0)
            {
                MessageBox.Show("The SAN extension exists, but no valid DNS or IP entries were found.\n\nSAN Check: FAILED", "SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string webAddress = TextBox_WebAdress.Text.Trim();
            bool hasTargetHostname = !string.IsNullOrWhiteSpace(webAddress) && !webAddress.Equals("Enter web address...", StringComparison.OrdinalIgnoreCase);
            if (!hasTargetHostname)
            {
                string sanList = string.Join("\n", dnsNames.Select(x => "DNS: " + x).Concat(ipAddresses.Select(x => "IP: " + x)));
                MessageBox.Show("The certificate contains a valid Subject Alternative Name extension.\n\n" + sanList + "\n\nSAN Structure: OK\nHostname Match: NOT PERFORMED\nReason: No target hostname was provided.", "SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string hostname;
            try
            {
                string address = webAddress.Contains("://") ? webAddress : "https://" + webAddress;
                var uri = new Uri(address);
                hostname = uri.Host.Trim().TrimEnd('.').ToLowerInvariant();
            }
            catch
            {
                MessageBox.Show("The web address is not valid.\n\nSAN Structure: OK\nHostname Match: NOT PERFORMED", "SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool isIpAddress = System.Net.IPAddress.TryParse(hostname, out var requestedIp);
            bool matched = false;
            string matchedSan = null;
            if (isIpAddress)
            {
                foreach (string ip in ipAddresses)
                {
                    if (System.Net.IPAddress.TryParse(ip, out var certificateIp) && requestedIp.Equals(certificateIp))
                    {
                        matched = true;
                        matchedSan = ip;
                        break;
                    }
                }
            }
            else
            {
                foreach (string dns in dnsNames)
                {
                    if (string.Equals(dns, hostname, StringComparison.OrdinalIgnoreCase))
                    {
                        matched = true;
                        matchedSan = dns;
                        break;
                    }
                    if (dns.StartsWith("*.", StringComparison.Ordinal))
                    {
                        string suffix = dns.Substring(1);
                        if (hostname.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                        {
                            string leftPart = hostname.Substring(0, hostname.Length - suffix.Length).TrimEnd('.');
                            if (!string.IsNullOrWhiteSpace(leftPart) && !leftPart.Contains("."))
                            {
                                matched = true;
                                matchedSan = dns;
                                break;
                            }
                        }
                    }
                }
            }
            if (matched)
            {
                MessageBox.Show("The requested hostname matches the certificate SAN.\n\nHostname: " + hostname + "\nMatched SAN: " + matchedSan + "\n\nSAN Structure: OK\nHostname Match: OK", "SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string sanList = string.Join("\n", dnsNames.Select(x => "DNS: " + x).Concat(ipAddresses.Select(x => "IP: " + x)));

                MessageBox.Show("The requested hostname does NOT match the certificate SAN.\n\nHostname: " + hostname + "\n\nCertificate SAN entries:\n" + sanList + "\n\nSAN Structure: OK\nHostname Match: FAILED", "SAN Recheck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_terminal_Click(object sender, EventArgs e)
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
    }
}