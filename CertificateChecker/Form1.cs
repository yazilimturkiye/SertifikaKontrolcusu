using System; //yazilimturkiye.com 03.07.2021 Tüm hakları saklıdır.
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.IO;

namespace CertificateChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //formu kenarlıkları olmadan Label_Altbaslik ile sürüklüyerek taşımamızı sağlayan bölüm.
        bool formTasiniyor = false;
        Point baslangicNoktasi = new Point(0, 0);
        //Seçilen dosyanın adının ve yolunu sakladığımız değişkenler.
        string Dosya_Yolu;
        string Dosya_Adi;
        //forma kenarlıklar olmadan gölge efekti kazandıran bölüm
        private const int CS_DROPSHADOW = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        //seçtiğimiz sertifikamızı doğrulamamızı sağlayan metot.
        public void SertifikaDogrula()
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
                if (Textbox_Durum.Text == "True")
                {
                    Textbox_Durum.Text = "Geçerli";
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
                        Textbox_Durum.Text = "Geçersiz, Süresi İleri";
                    }
                    else if (bitis_zamani < simdiki_zaman)
                    {
                        timer1.Stop();
                        PictureBox_Durum.Image = Properties.Resources.error;
                        Textbox_Durum.Text = "Geçersiz, Süresi Bitmiş";
                    }
                    else
                    {
                        Textbox_Durum.Text = "Geçersiz, İptal Edilmiş";
                        PictureBox_Durum.Image = Properties.Resources.error;
                    }
                    
                }

            }
            catch (Exception) //sertifika dosyası dışında farklı bir dosya seçmesi durumunda kullanıcıyı uyaran bölüm.
            {
                timer1.Stop();
                MessageBox.Show("Lütfen geçerli bir sertifika dosyası seçiniz. Geçerli dosya uzantılar:\n.cer, .cert, .exe.","Geçersiz Dosya", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void Buton_Dosya_Sec_Click(object sender, EventArgs e)//dosya seçme işleminin yapıldığı kısım.
        {
            OpenFileDialog DosyaAc = new OpenFileDialog();
            DosyaAc.Title = "Sertifika Dosyasını Seçiniz...";
            DosyaAc.Filter = ".cer Dosyası |*.cer|.cert Dosyası|*.cert|.exe Dosyası|*.exe| Tüm Dosyalar|*.*";
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
                Buton_Dosya_Sec.Text = "Sertifika Seç";
                Buton_Dosya_Sec.Enabled = true;
                return;
            }
            Buton_Dosya_Sec.Enabled = false;

            Buton_Dosya_Sec.Text = "Sertifika Doğrulanıyor...";
            progressBar1.Value += 1;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//linklabel kaynak web sitesine yönlendirme yapıyor.
        {
            Process sayfa_ac = new Process();
            sayfa_ac.StartInfo.UseShellExecute = true;
            sayfa_ac.StartInfo.FileName = "https://www.yazilimturkiye.com/";
            sayfa_ac.Start(); 
        }

        private void Buton_Kapat_Click(object sender, EventArgs e)//uygulamayı kapatan buton.
        {
            Application.Exit();
        }

        private void Buton_Kucult_Click(object sender, EventArgs e)//Uygulamayı simge durumuna küçülten buton.
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Label_Arkaplan_MouseDown(object sender, MouseEventArgs e)//label sayasinde formumuzu sürükleyebiliyoruz.
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }

        private void Label_Arkaplan_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
        }

        private void Label_Arkaplan_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
        }

        private void Buton_Yardim_Click(object sender, EventArgs e)//yardım butonu
        {
            MessageBox.Show("Sertifika Kontrolcüsü, sistemden seçilen bir dijital sertifikayı kontrol ederek kullanıcıya sertifikanın geçerli ya da geçersiz olduğu bilgisini vermekte olan ücretsiz bir yazılımdır. Sertifika kontrolcüsü, .Net platformunda açık kaynak olarak geliştirilen bir yazılımdır. Destek olmak ve daha fazla bilgi sahibi olmak için web sitemizi ziyaret ediniz. www.yazilimturkiye.com", "Sertifika Kontrolcüsü Ücretsiz Versiyon Hakkında", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
    }

    }


