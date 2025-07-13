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
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X500;

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
                    sb.Append(b.ToString("x2")); //Hex format
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

                    if (result.EndsWith(", "))//Fazladan virgülü kaldır
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
            button_CPS.Enabled = false;
            Buton_Goruntule.Enabled = false;
            Button_GoToAddress.Enabled = false;
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

            // Zinciri sondan başa doğru işliyoruz
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
                ParseCRLAddresses(sertifika);//CRL adreslerini çeken metot.
                ParseAIAAddresses(sertifika); //AIA adreslerini çeken metot.
                ParseOCSPAddresses(sertifika); //OCSP adreslerini çeken metot.
                ParseEKU(sertifika); //Extended Key Usage (EKU) bilgilerini çeken metot.
                ParseSAN(sertifika); //Subject Alternative Name (SAN) bilgilerini çeken metot.

                CertificateDatesCheck(sertifika.NotBefore, sertifika.NotAfter);//Sertifikanın bitiş tarihini kontrol eden metot.

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
            catch (Exception) //sertifika dosyası dışında farklı bir dosya seçmesi durumunda kullanıcıyı uyaran bölüm.
            {
                MessageBox.Show("Please select a valid certificate file. Valid file extensions:\n.cer, .crt, .exe.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Buton_Dosya_Sec_Click(object sender, EventArgs e)//dosya seçme işleminin yapıldığı kısım.
        {
            OpenFileDialog DosyaAc = new OpenFileDialog();
            DosyaAc.Title = "Please Select Certificate File...";
            DosyaAc.Filter = "Certificate Files (*.cer;*.cert;*.crt)|*.cer;*.cert;*.crt|All Files (*.*)|*.*";
            DosyaAc.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DosyaAc.Multiselect = false;
            if (DosyaAc.ShowDialog() == DialogResult.OK)
            {
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
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//linklabel kaynak web sitesine yönlendirme yapıyor.
        {
            Process sayfa_ac = new Process();
            sayfa_ac.StartInfo.UseShellExecute = true;
            sayfa_ac.StartInfo.FileName = "https://www.yazilimturkiye.com/";
            sayfa_ac.Start();
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
            var base64Lines = lines
                .Where(line => !line.StartsWith("-----BEGIN") && !line.StartsWith("-----END"))
                .ToArray();

            return string.Join("", base64Lines);
        }
        private void textBox_converter_TextChanged(object sender, EventArgs e)//PEM/Base64 formatındaki sertifikayı doğrulayan metot.
        {
            try
            {
                string pem = textBox_converter.Text.Trim();

                if (string.IsNullOrWhiteSpace(pem) || pem.Length < 100)
                    return;

                //PEM bloğundaki base64 kısmı ayıkla (başlıklar kalsın)
                string base64 = ExtractBase64FromPem(pem);

                byte[] certBytes = Convert.FromBase64String(base64);
                X509Certificate2 sertifika = new X509Certificate2(certBytes);

                //Sertifika bilgilerini doldur
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
                //IP adresini al       
                IPAddress[] ipAddresses = Dns.GetHostAddresses(uri.Host);
                if (ipAddresses.Length > 0)
                    TextBox_IPAddress.Text = ipAddresses[0].ToString();
                else
                    TextBox_IPAddress.Text = "Not Found";

                //Sertifika ve TLS sürümünü al
                var (sertifika, tlsVersiyon) = GetRemoteCertificateWithTls(uri.Host, uri.Port == -1 ? 443 : uri.Port);
                TextBox_TLS.Text = tlsVersiyon.ToString();

                if (sertifika == null)
                {
                    Textbox_Durum.Text = "Certificate is not taken.";
                    PictureBox_Durum.Image = Properties.Resources.error;
                    return;
                }
                //Sertifika bilgileri
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
                //Zincir doğrulaması
                X509Chain chain = new X509Chain();
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;

                CertificateChain(sertifika, TreeView_Chain); //Zinciri ekleyen metodu çağır.
                ParseCRLAddresses(sertifika);//CRL adreslerini çeken metot.
                ParseAIAAddresses(sertifika); //AIA adreslerini çeken metot.
                ParseOCSPAddresses(sertifika); //OCSP adreslerini çeken metot.
                ParseEKU(sertifika); //Extended Key Usage (EKU) bilgilerini çeken metot.
                ParseSAN(sertifika); //Subject Alternative Name (SAN) bilgilerini çeken metot.

                CertificateDatesCheck(sertifika.NotBefore, sertifika.NotAfter);//Sertifikanın bitiş tarihini kontrol eden metot.

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
        private void Buton_Ayarlar_Click(object sender, EventArgs e)//Ayarlar butonuna tıklandığında kullanıcıya bilgi veren metot.
        {
            MessageBox.Show(
                "Certificate Checker is a free and open-source tool for verifying digital certificates.\n\n" +
                "Built on the .NET platform with BouncyCastle, it helps users identify certificate details and validate their authenticity.\n\n" +
                "© 2021 yazilimturkiye.com – All rights reserved.\n\n" +
                "Visit our website for updates and support.\n\n" +
                "Version: 5.0",
                "About Certificate Checker",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}