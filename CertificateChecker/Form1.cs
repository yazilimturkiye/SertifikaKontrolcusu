using System; //yazilimturkiye.com 03.07.2021 Tüm hakları saklıdır.
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.VisualStyles;
using Windows.Security.Cryptography.Certificates;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.ConstrainedExecution;
using System.Net;
using System.Security.Policy;
using System.Net.Security;

namespace CertificateChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        string Dosya_Yolu;//Seçilen dosyanın adının ve yolunu sakladığımız değişkenler.
        string Dosya_Adi;

        private string GetBasicConstraints(X509Certificate2 ConstrationsCert)// Basic Constraints bilgisini çeken metot.
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
        private string GetKeyUsage(X509Certificate2 ConstrationsCertUsage)// Key Usage bilgisini çeken metot.
        {
            string result = "";

            foreach (var extension in ConstrationsCertUsage.Extensions)// Sertifika uzantıları arasında Key Usage varsa bulan metot.
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

                    if (result.EndsWith(", "))// Fazladan virgülü kaldır
                        result = result.Substring(0, result.Length - 2);
                    return result;
                }
            }
            return "No Key Usage found in certificate.";
        }
        private string GetKeySize(X509Certificate2 cert) // Sertifikanın anahtar tipi ve boyunutu alan metot.
        {
            var rsaKey = cert.GetRSAPublicKey(); // RSA anahtar boyutunu almak
            if (rsaKey != null)
            {
                return $"RSA{rsaKey.KeySize}";
            }
            var ecKey = cert.GetECDsaPublicKey(); // EC (ECC) anahtar boyutunu almak
            if (ecKey != null)
            {
                return $"ECC{ecKey.KeySize}";
            }
            return "Not Found"; // Desteklenmeyen anahtar türleri veya anahtar bulunamaması durumunda
        }
        public void SertifikaDogrula()// Seçtiğimiz sertifikamızı doğrulamamızı sağlayan metot.
        {
            try
            {
                X509Certificate2 sertifika = new X509Certificate2(Dosya_Yolu);
                Textbox_Veren.Text = sertifika.Issuer.ToString(); // Sertifikayı Veren Makam.
                Textbox_Verilen.Text = sertifika.Subject.ToString(); // Sertifikanın Konu Adı
                Textbox_Baslangic.Text = sertifika.NotBefore.ToString(); // Başlangıç Tarihi.
                Textbox_Bitis.Text = sertifika.NotAfter.ToString(); // Bitiş Tarihi.
                Textbox_Serino.Text = sertifika.SerialNumber.ToString(); // Sertifika Seri no.
                Textbox_Algoritma.Text = sertifika.SignatureAlgorithm.FriendlyName.ToString(); // sertifikanın algoritması.
                Textbox_Constraints.Text = GetBasicConstraints(sertifika); // Sertifika Temel Kısıtları.
                Textbox_Usage.Text = GetKeyUsage(sertifika); // Anahtar Kullanım amaçları.
                TextBox_Publickey.Text = GetKeySize(sertifika); // Anahtar tipi ve boyutu.

                X509Chain chain = new X509Chain(); // Sertifika zincirini oluştur
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online; // Sertifika iptal durumu kontrolü (revocation check)
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;

                bool isChainValid = chain.Build(sertifika);
                if (isChainValid) // Sertifika Geçerli
                {
                    Textbox_Durum.Text = "Certificate is Valid.";
                    PictureBox_Durum.Image = Properties.Resources.ok;
                }
                else // Zincir geçersiz, sebebi açıklanıyor.
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
                                timer1.Stop();
                                PictureBox_Durum.Image = Properties.Resources.error;
                                Textbox_Durum.Text = "Certificate is Not Yet Valid!";
                            }
                            else if (bitis_zamani < simdiki_zaman) //zaman geride ise.
                            {
                                timer1.Stop();
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
                timer1.Stop();
                MessageBox.Show("Please select a valid certificate file. Valid file extensions:\n.cer, .cert, .exe.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Buton_Dosya_Sec_Click(object sender, EventArgs e)//dosya seçme işleminin yapıldığı kısım.
        {
            OpenFileDialog DosyaAc = new OpenFileDialog();
            DosyaAc.Title = "Please Select Certificate File...";
            DosyaAc.Filter = "All Files|*.*| .cer File|*.cer| .cert File|*.cert| .crt File|*.crt";
            DosyaAc.Multiselect = false;
            if (DosyaAc.ShowDialog() == DialogResult.OK)
            {
                Dosya_Adi = DosyaAc.SafeFileName.ToString();
                Dosya_Yolu = DosyaAc.FileName.ToString();
                Textbox_DosyaAdi.Text = Dosya_Adi;
                Textbox_DosyaYolu.Text = Dosya_Yolu;
                timer1.Start();//dosya seçiminden sonra timer1 burada çalıştırılır.
                Textbox_Veren.Clear();
                Textbox_Verilen.Clear();
                Textbox_Baslangic.Clear();
                Textbox_Bitis.Clear();
                Textbox_Serino.Clear();
                Textbox_Algoritma.Clear();
                PictureBox_Durum.Image = null;
                Textbox_Durum.Clear();
                Textbox_Constraints.Clear();
                TextBox_Publickey.Clear();
                Textbox_Usage.Clear();
                progressBar1.Value = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)//timer1'de progressbar tetiklenir ve progressbar'ın değeri maximum olduktan sonra sertifika doğrulama adımına geçilir.
        {
            if (progressBar1.Value == progressBar1.Maximum)
            {
                SertifikaDogrula();//sertifika doğrulama metotu burada çağırılıyor.
                timer1.Stop();
                progressBar1.Value = 100;
                Buton_Dosya_Sec.Text = "Select Certificate File";
                Buton_Dosya_Sec.Enabled = true;
                Buton_Goruntule.Enabled = true;
                button_CPS.Enabled = true;
                return;
            }
            Buton_Dosya_Sec.Enabled = false;
            button_CPS.Enabled = false;
            Buton_Goruntule.Enabled = false;
            Buton_Dosya_Sec.Text = "Certificate Verifying...";
            progressBar1.Value += 1;
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
                Process.Start(new ProcessStartInfo(Dosya_Yolu) { UseShellExecute = true });
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
            panel_Ayarlar.Visible = false;
        }

        private void Buton_Ayarlar_Click(object sender, EventArgs e)//Ayarlar butonu, diğer panelleri kapatacak ve ayarlar panelini açacak.
        {
            panel_Ayarlar.Visible = true;
            Panel_SertifikaKontrol.Visible = false;

        }

        private void button_CPS_Click(object sender, EventArgs e)//Eğer sertifika bir "Issuer Statement" değerine sahip ise bunu tarayıcıda açan kod bloğu.
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(Dosya_Yolu);

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

        private string GetCertificatePolicyUrl(X509Certificate2 cert)
        {
            foreach (var extension in cert.Extensions)
            {
                if (extension.Oid.Value == "2.5.29.32") // OID for Certificate Policies
                {
                    var asnData = new AsnEncodedData(extension.Oid, extension.RawData);
                    string policies = asnData.Format(true);

                    var urlStartIndex = policies.IndexOf("http");// URL arama işlemi
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

        private void button_Sertifikakontrol_Click(object sender, EventArgs e) //Dosya ile Sertifika Kontrol butonu, diğer panelleri kapatacak ve ayarlar panelini açacak.
        {
            Panel_SertifikaKontrol.Visible = true;
            panel_Ayarlar.Visible = false;
        }
    }
}