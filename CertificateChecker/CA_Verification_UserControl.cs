using CertificateChecker.Models;
using CertificateChecker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace CertificateChecker
{
    public partial class CA_Verification_UserControl : UserControl
    {
        public CA_Verification_UserControl()
        {
            InitializeComponent();
            LoadCATreeView();
            this.Load += (s, e) =>
            {
                (this.ParentForm as Form1)?.SetPageTitle("Verification >  Certification Authority Verification");
            };
        }
        private readonly ImportRootCA _importRootCA = new ImportRootCA();
        private readonly Dictionary<int, CARecord> _caCertificates = new Dictionary<int, CARecord>();
        private readonly Dictionary<int, List<int>> _caChildren = new Dictionary<int, List<int>>();
        private bool _isFilteringTree = false;
        private class CARecord
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }

        private void LoadCATreeView()
        {
            string dbPath = Path.Combine(Application.StartupPath, "Data", "CertificateAuthority.db");
            if (!File.Exists(dbPath))
            {
                MessageBox.Show("CA database not found:\n" + dbPath);
                return;
            }
            _caCertificates.Clear();
            _caChildren.Clear();
            string connectionString = $"Data Source={dbPath};Version=3;Read Only=True;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string certificateQuery = @"SELECT certificate_id, display_name, certificate_type FROM ca_certificates ORDER BY certificate_id;";
                using (SQLiteCommand command = new SQLiteCommand(certificateQuery, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CARecord certificate = new CARecord
                        {
                            Id = Convert.ToInt32(reader["certificate_id"]),
                            Name = reader["display_name"].ToString(),
                            Type = reader["certificate_type"].ToString()
                        };
                        _caCertificates[certificate.Id] = certificate;
                    }
                }
                string issuerQuery = @"SELECT certificate_id, issuer_certificate_id FROM certificate_issuers ORDER BY issuer_certificate_id, certificate_id;";
                using (SQLiteCommand command = new SQLiteCommand(issuerQuery, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int childId = Convert.ToInt32(reader["certificate_id"]);
                        int parentId = Convert.ToInt32(reader["issuer_certificate_id"]);
                        if (!_caCertificates.ContainsKey(childId) ||
                            !_caCertificates.ContainsKey(parentId))
                            continue;
                        if (!_caChildren.ContainsKey(parentId))
                            _caChildren[parentId] = new List<int>();
                        if (!_caChildren[parentId].Contains(childId))
                            _caChildren[parentId].Add(childId);
                    }
                }
            }
            ApplyCATreeFilter();
        }
        private void ApplyCATreeFilter()
        {
            string searchText = TextBox_SearchCA.Text.Trim();
            _isFilteringTree = true;
            TreeView_CA.BeginUpdate();
            try
            {
                TreeView_CA.Nodes.Clear();
                TreeView_CA.Sorted = false;
                var roots = _caCertificates.Values.Where(x => x.Type.Equals("Root", StringComparison.OrdinalIgnoreCase)).OrderBy(x => x.Id);
                foreach (CARecord root in roots)
                {
                    TreeNode rootNode = CreateFilteredNode(
                        root.Id,
                        searchText,
                        new HashSet<int>()
                    );
                    if (rootNode != null)
                        TreeView_CA.Nodes.Add(rootNode);
                }
            }
            finally
            {
                TreeView_CA.EndUpdate();
                _isFilteringTree = false;
            }
        }
        private TreeNode CreateFilteredNode(int certificateId, string searchText, HashSet<int> path)
        {
            if (path.Contains(certificateId))
                return null;
            if (!_caCertificates.TryGetValue(
                certificateId, out CARecord certificate))
                return null;
            path.Add(certificateId);
            bool isMatch = string.IsNullOrWhiteSpace(searchText) || certificate.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
            TreeNode node = new TreeNode(certificate.Name)
            {
                Tag = certificate.Id
            };
            if (_caChildren.ContainsKey(certificateId))
            {
                foreach (int childId in _caChildren[certificateId].OrderBy(x => x))
                {
                    TreeNode childNode = CreateFilteredNode(
                        childId,
                        isMatch ? string.Empty : searchText,
                        path
                    );
                    if (childNode != null)
                        node.Nodes.Add(childNode);
                }
            }
            path.Remove(certificateId);
            if (!isMatch && node.Nodes.Count == 0)
                return null;
            return node;
        }
        private void AddCAChildren(TreeNode parentNode, int parentId, Dictionary<int, CARecord> certificates, Dictionary<int, List<int>> children, HashSet<int> path)
        {
            if (!path.Add(parentId))
                return;
            if (children.ContainsKey(parentId))
            {
                foreach (int childId in children[parentId].OrderBy(x => x))
                {
                    if (path.Contains(childId))
                        continue;
                    CARecord child = certificates[childId];
                    TreeNode childNode = new TreeNode(child.Name)
                    {
                        Tag = child.Id
                    };
                    parentNode.Nodes.Add(childNode);
                    AddCAChildren(childNode, childId, certificates, children, path);
                }
            }
            path.Remove(parentId);
        }
        private readonly CertificateParsingService _certificateParsingService = new CertificateParsingService();
        private void TreeView_CA_AfterSelect(
            object sender,
            TreeViewEventArgs e)
        {
            if (_isFilteringTree || e.Node?.Tag == null)
                return;
            int certificateId = Convert.ToInt32(e.Node.Tag);
            LoadSelectedCA(certificateId);
        }
        private void LoadSelectedCA(int certificateId)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Data", "CertificateAuthority.db");
            string connectionString = $"Data Source={dbPath};Version=3;Read Only=True;";
            try
            {
                string certificateData = null;
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT certificate_data FROM ca_certificates WHERE certificate_id = @CertificateId;";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CertificateId", certificateId);
                        certificateData = command.ExecuteScalar()?.ToString();
                    }
                }
                if (string.IsNullOrWhiteSpace(certificateData))
                    return;
                byte[] certificateBytes = Convert.FromBase64String(certificateData);

                using (X509Certificate2 certificate = new X509Certificate2(certificateBytes))
                {
                    CertificateDetails details = _certificateParsingService.Parse(certificate);
                    Textbox_Verilen.Text = details.Subject;
                    Textbox_Veren.Text = details.Issuer;
                    TextBox_aki.Text = details.AuthorityKeyIdentifier;
                    TextBox_ski.Text = details.SubjectKeyIdentifier;
                    Textbox_Serino.Text = details.SerialNumber;
                    Textbox_Usage.Text = details.KeyUsage;
                    Textbox_Constraints.Text = details.BasicConstraints;
                    Textbox_Baslangic.Text = certificate.NotBefore.ToString("dd.MM.yyyy HH:mm:ss");
                    Textbox_Bitis.Text = certificate.NotAfter.ToString("dd.MM.yyyy HH:mm:ss");
                    Textbox_Algoritma.Text = details.SignatureAlgorithm;
                    TextBox_Publickey.Text = details.PublicKeyInfo;
                    TextBox_Thumbprint.Text = certificate.Thumbprint;
                    TextBox_Policy.Text = details.CertificatePolicy;
                    TextBox_CRL.Text = details.CrlText;
                    TextBox_AIA.Text = details.AiaText;
                    TextBox_OCSP.Text = details.OcspText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Certificate could not be loaded:\n" + ex.Message, "Certificate Authority", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBox_SearchCA_TextChanged(object sender, EventArgs e)
        {
            ApplyCATreeFilter();
        }
        private void button_ImportCA_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Import Certificate Authority";

                dialog.Filter =
                    "Certificate Files (*.cer;*.crt;*.pem;*.der;*.p7b;*.p7c)|*.cer;*.crt;*.pem;*.der;*.p7b;*.p7c|" +
                    "All Files (*.*)|*.*";

                dialog.Multiselect = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    CAImportResult result =
                        _importRootCA.ImportCertificates(dialog.FileNames);

                    if (result.Success)
                    {
                        LoadCATreeView();

                        MessageBox.Show(
                            result.Message,
                            "Import CA",
                            MessageBoxButtons.OK,
                            result.RejectedCount > 0
                                ? MessageBoxIcon.Warning
                                : MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            string.IsNullOrWhiteSpace(result.ErrorMessage)
                                ? result.Message
                                : result.ErrorMessage,
                            "Import CA",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Import failed:\n" + ex.Message,
                        "Import CA",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
