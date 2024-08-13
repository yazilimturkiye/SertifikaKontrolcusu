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

        public void SertifikaDogrula()//seçtiğimiz sertifikamızı doğrulamamızı sağlayan metot.
        {
            try
            {
                X509Certificate2 sertifika = new X509Certificate2(Dosya_Yolu);
                byte[] rawdata = sertifika.RawData;
                Textbox_Veren.Text = sertifika.Issuer.ToString();                                           // Sertifikayı Veren Makam.
                Textbox_Verilen.Text = sertifika.Subject.ToString();                                        // Sertifikanın Konu Adı
                Textbox_Baslangic.Text = sertifika.NotBefore.ToString();                                    // Başlangıç Tarihi.
                Textbox_Bitis.Text = sertifika.NotAfter.ToString();                                         // Bitiş Tarihi.
                Textbox_Serino.Text = sertifika.SerialNumber.ToString();                                    // Sertifika Seri no.
                Textbox_Algoritma.Text = sertifika.SignatureAlgorithm.FriendlyName.ToString();              // sertifikanın algoritması.
                Textbox_Durum.Text = sertifika.Verify().ToString();                                         // Sertifika doğrulama, true ise başarılı, false ise başarısız.
                if (Textbox_DosyaAdi.TextLength > 0) Textbox_DosyaAdi.DeselectAll();
                if (Textbox_Durum.Text == "True")
                {
                    Textbox_Durum.Text = "Valid";
                    PictureBox_Durum.Image = Properties.Resources.ok;
                    timer1.Stop();
                }
                else //eğer sertifika geçersiz ise aşağıdaki işlemler ile elenerek neden geçersiz olduğu kullanıcıya bildirilir.
                {
                    DateTime simdiki_zaman = DateTime.Now;
                    DateTime baslangic_zamani = DateTime.Parse(Textbox_Baslangic.Text);
                    DateTime bitis_zamani = DateTime.Parse(Textbox_Bitis.Text);
                    if (baslangic_zamani > simdiki_zaman)
                    {
                        timer1.Stop();
                        PictureBox_Durum.Image = Properties.Resources.error;
                        Textbox_Durum.Text = "Time Ahead";
                    }
                    else if (bitis_zamani < simdiki_zaman)
                    {
                        timer1.Stop();
                        PictureBox_Durum.Image = Properties.Resources.error;
                        Textbox_Durum.Text = "Expired";
                    }
                    else
                    {
                        Textbox_Durum.Text = "Revoked";
                        PictureBox_Durum.Image = Properties.Resources.error;
                    }

                }

            }
            catch (Exception) //sertifika dosyası dışında farklı bir dosya seçmesi durumunda kullanıcıyı uyaran bölüm.
            {
                timer1.Stop();
                MessageBox.Show("Please select a valid certificate file. Valid file extensions:\n.cer, .cert, .exe.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Hunter_KaranlikModu(Control parent)//Hunter adındaki karanlık mod açan metot.
        {
            parent.BackColor = Color.FromArgb(34, 40, 49);
            parent.ForeColor = Color.White;

            foreach (Control control in parent.Controls)
            {
                if (control is System.Windows.Forms.TextBox || control is System.Windows.Forms.ComboBox)
                {
                    control.BackColor = Color.FromArgb(49, 54, 63);
                    control.ForeColor = Color.White;
                }
                else if (control is System.Windows.Forms.Button)
                {
                    control.BackColor = Color.FromArgb(49, 54, 63);
                    control.ForeColor = Color.White;
                    Buton_Ayarlar.FlatAppearance.BorderColor = Color.DimGray;
                    Buton_Goruntule.FlatAppearance.BorderColor = Color.DimGray;
                    Buton_Dosya_Sec.FlatAppearance.BorderColor = Color.DimGray;
                }
                else
                {
                    control.BackColor = Color.FromArgb(34, 40, 49);
                    control.ForeColor = Color.White;
                    PictureBox_Durum.BackColor = Color.FromArgb(49, 54, 64);
                    Label_Baslik.ForeColor = Color.Goldenrod;
                    Label_Baslik.BackColor = Color.FromArgb(40, 44, 51);
                    Label_AltBaslik.BackColor = Color.FromArgb(40, 44, 51);
                    label1.BackColor = Color.FromArgb(40, 44, 51);
                }
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                if (control.HasChildren)
                {
                    Hunter_KaranlikModu(control);
                }
            }
        }
        private void Argent_AydinlikModu(Control parent)//Argent adındaki Aydinlik mod açan metot.
        {
            parent.BackColor = SystemColors.Control;
            parent.ForeColor = Color.FromArgb(64, 64, 64);

            foreach (Control control in parent.Controls)
            {
                if (control is System.Windows.Forms.TextBox || control is System.Windows.Forms.ComboBox)
                {
                    control.BackColor = SystemColors.Control;
                    control.ForeColor = SystemColors.WindowText;
                }
                else if (control is System.Windows.Forms.Button)
                {
                    control.BackColor = SystemColors.ControlLight;
                    control.ForeColor = Color.FromArgb(64, 64, 64);
                    Buton_Ayarlar.FlatAppearance.BorderColor = Color.Silver;
                    Buton_Goruntule.FlatAppearance.BorderColor = Color.Silver;
                    Buton_Dosya_Sec.FlatAppearance.BorderColor = Color.Silver;
                    Buton_Ayarlar.BackColor = SystemColors.Control;
                    Buton_Goruntule.BackColor = SystemColors.Control;
                }
                else
                {
                    control.BackColor = SystemColors.Control;
                    control.ForeColor = Color.FromArgb(64, 64, 64);
                    PictureBox_Durum.BackColor = SystemColors.Control;
                    Label_Baslik.ForeColor = Color.Goldenrod;
                    Label_Baslik.BackColor = SystemColors.ControlLight;
                    Label_AltBaslik.ForeColor = Color.FromArgb(64, 64, 64);
                    Label_AltBaslik.BackColor = SystemColors.ControlLight;
                    label1.BackColor = SystemColors.ControlLight;
                }
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.Fixed3D;
                }
                if (control.HasChildren)
                {
                    Argent_AydinlikModu(control);
                }
            }
        }
        private void Stunner_SariModu(Control parent)//Stunner adındaki Sari mod açan metot.
        {
            parent.BackColor = Color.FromArgb(248, 240, 229);
            parent.ForeColor = Color.FromArgb(63, 35, 5);

            foreach (Control control in parent.Controls)
            {
                if (control is System.Windows.Forms.TextBox || control is System.Windows.Forms.ComboBox)
                {
                    control.BackColor = Color.FromArgb(248, 240, 229);
                    control.ForeColor = Color.FromArgb(63, 35, 5);
                }
                else if (control is System.Windows.Forms.Button)
                {
                    control.BackColor = Color.FromArgb(234, 219, 200);
                    control.ForeColor = Color.FromArgb(63, 35, 5);
                    Buton_Ayarlar.FlatAppearance.BorderColor = Color.Tan;
                    Buton_Goruntule.FlatAppearance.BorderColor = Color.Tan;
                    Buton_Dosya_Sec.FlatAppearance.BorderColor = Color.Tan;
                }
                else
                {
                    control.BackColor = Color.FromArgb(248, 240, 229);
                    control.ForeColor = Color.FromArgb(63, 35, 5);
                    PictureBox_Durum.BackColor = Color.FromArgb(248, 240, 229);
                    Label_Baslik.ForeColor = Color.Goldenrod;
                    Label_Baslik.BackColor = Color.FromArgb(234, 219, 200);
                    Label_AltBaslik.BackColor = Color.FromArgb(234, 219, 200);
                    label1.BackColor = Color.FromArgb(234, 219, 200);
                }
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.Fixed3D;
                }
                if (control.HasChildren)
                {
                    Stunner_SariModu(control);
                }
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
                Buton_Dosya_Sec.Text = "Select Certificate";
                Buton_Dosya_Sec.Enabled = true;
                Buton_Goruntule.Enabled = true;
                return;
            }
            Buton_Dosya_Sec.Enabled = false;

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
            panel_Ayarlar.Visible = false;
        }

        private void radioButton_darkmode_CheckedChanged(object sender, EventArgs e)//HUnter Karanlık Modu Radiobuttonu
        {
            Hunter_KaranlikModu(this);
        }

        private void radioButton_lightmode_CheckedChanged(object sender, EventArgs e)
        {
            Argent_AydinlikModu(this);
        }

        private void radioButton_stunner_CheckedChanged(object sender, EventArgs e)
        {
            Stunner_SariModu(this);
        }

        private void Buton_Ayarlar_Click(object sender, EventArgs e)//Ayarlar butonu.
        {
            if (Panel_SertifikaKontrol.Visible == true)
            {
                panel_Ayarlar.Visible = true;
                Panel_SertifikaKontrol.Visible = false;
            }
            else if (panel_Ayarlar.Visible == true)
            {
                panel_Ayarlar.Visible = false;
                Panel_SertifikaKontrol.Visible = true;
            }
        }
    }

}