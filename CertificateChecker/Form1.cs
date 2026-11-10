using System; //yazilimturkiye.com 03.07.2021 Tüm hakları saklıdır.
using System.Diagnostics;
using System.Windows.Forms;
namespace CertificateChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
        }
        public void SetPageTitle(string pageTitle) //Form başlığını ayarlayan metot.
        {
            Label_AltBaslik.Text = pageTitle;
        }
        private void Form1_Load(object sender, EventArgs e)//form başlangıç.
        {
            File_Verification_UserControl uc = new File_Verification_UserControl();
            panel_main.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel_main.Controls.Add(uc);
        }
        private void about_ToolStripMenuItem_Click(object sender, EventArgs e) //Hakkında menü öğesi tıklandığında çalışacak metot.
        {
            MessageBox.Show("Certificate Checker is a free and open-source tool for verifying digital certificates.\n\n" + "Built on the .NET platform with BouncyCastle, it helps users identify certificate details and validate their authenticity.\n\n" + "© 2021 yazilimturkiye.com – All rights reserved.\n\n" + "Visit our website for updates and support.\n\n" + "Version: 8.0.0", "About Certificate Checker", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void github_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/yazilimturkiye/SertifikaKontrolcusu",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("GitHub page could not be opened.\n\n" + ex.Message, "GitHub", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void officalWebSite_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://www.yazilimturkiye.com/",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Offical Web page could not be opened.\n\n" + ex.Message, "Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PEM_toolStripMenuItem_Click(object sender, EventArgs e) //PEM menü öğesi tıklandığında çalışacak metot.
        {
            PEM_Verification_UserControl uc = new PEM_Verification_UserControl();
            panel_main.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel_main.Controls.Add(uc);
        }
        private void FILE_toolStripMenuItem_Click(object sender, EventArgs e) //FILE menü öğesi tıklandığında çalışacak metot.
        {
            File_Verification_UserControl uc = new File_Verification_UserControl();
            panel_main.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel_main.Controls.Add(uc);
        }
        private void SSLTLS_toolStripMenuItem_Click(object sender, EventArgs e) //SSL/TLS menü öğesi tıklandığında çalışacak metot.
        {
            SSL_Verification_UserControl uc = new SSL_Verification_UserControl();
            panel_main.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel_main.Controls.Add(uc);
        }
        private void CAChecker_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            CA_Verification_UserControl uc = new CA_Verification_UserControl();
            panel_main.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel_main.Controls.Add(uc);
        }
    }
}