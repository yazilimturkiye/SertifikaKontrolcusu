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
        private readonly DeleteRootCA _deleteRootCA = new DeleteRootCA();
        private class CARecord
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
        private void ClearCAInformation()
        {
            Textbox_Verilen.Clear();
            Textbox_Veren.Clear();
            TextBox_aki.Clear();
            TextBox_ski.Clear();
            Textbox_Serino.Clear();
            Textbox_Usage.Clear();
            Textbox_Constraints.Clear();
            Textbox_Baslangic.Clear();
            Textbox_Bitis.Clear();
            Textbox_Algoritma.Clear();
            TextBox_Publickey.Clear();
            TextBox_Thumbprint.Clear();
            TextBox_Policy.Clear();
            TextBox_CRL.Clear();
            TextBox_AIA.Clear();
            TextBox_OCSP.Clear();
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
                dialog.Filter = "Certificate Files (*.cer;*.crt;*.pem;*.der;*.p7b;*.p7c)|*.cer;*.crt;*.pem;*.der;*.p7b;*.p7c|" + "All Files (*.*)|*.*";
                dialog.Multiselect = true;
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                try
                {
                    CAImportResult result = _importRootCA.ImportCertificates(dialog.FileNames);
                    if (result.Success)
                    {
                        LoadCATreeView();

                        MessageBox.Show(result.Message, "Import CA", MessageBoxButtons.OK, result.RejectedCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(string.IsNullOrWhiteSpace(result.ErrorMessage) ? result.Message : result.ErrorMessage, "Import CA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import failed:\n" + ex.Message, "Import CA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button_DeleteCA_Click(object sender, EventArgs e)
        {
            if (TreeView_CA.SelectedNode == null || TreeView_CA.SelectedNode.Tag == null)
            {
                MessageBox.Show("Please select a certificate authority first.", "Delete CA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int certificateId = Convert.ToInt32(TreeView_CA.SelectedNode.Tag);
            string certificateName = TreeView_CA.SelectedNode.Text;
            DialogResult confirmation = MessageBox.Show("Are you sure you want to delete '" + certificateName + "'?\n\n" + "All subordinate certificate authorities under this certificate will also be deleted.\n\n" + "This operation cannot be undone.", "Confirm CA Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmation != DialogResult.Yes)
                return;
            int deletedCount;
            string errorMessage;
            bool success = _deleteRootCA.DeleteCertificate(certificateId, out deletedCount, out errorMessage);
            if (!success)
            {
                MessageBox.Show("Certificate deletion failed:\n" + errorMessage, "Delete CA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ClearCAInformation();
            LoadCATreeView();
            MessageBox.Show(deletedCount + " certificate authority record(s) deleted successfully.", "Delete CA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button_ExportCA_Click(object sender, EventArgs e)
        {
            if (TreeView_CA.SelectedNode == null || TreeView_CA.SelectedNode.Tag == null)
            {
                MessageBox.Show("Please select a certificate authority first.","Export CA",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            int certificateId = Convert.ToInt32(TreeView_CA.SelectedNode.Tag);
            string certificateName = TreeView_CA.SelectedNode.Text;
            try
            {
                string dbPath = Path.Combine(Application.StartupPath,"Data","CertificateAuthority.db");
                byte[] certificateData;
                using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;Read Only=True;"))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand("SELECT certificate_data FROM ca_certificates WHERE certificate_id = @id",connection))
                    {
                        command.Parameters.AddWithValue("@id", certificateId);
                        object value = command.ExecuteScalar();
                        if (value == null || value == DBNull.Value)
                            throw new InvalidOperationException(
                                "Certificate data could not be found.");
                        certificateData = Convert.FromBase64String(value.ToString());
                    }
                }
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Title = "Export Certificate Authority";
                    dialog.FileName = certificateName + ".cer";
                    dialog.Filter ="CER Certificate (*.cer)|*.cer|" +"CRT Certificate (*.crt)|*.crt|" +"DER Certificate (*.der)|*.der|" +"PEM Certificate (*.pem)|*.pem";
                    dialog.FilterIndex = 1;
                    dialog.AddExtension = true;
                    dialog.DefaultExt = "cer";
                    dialog.OverwritePrompt = true;
                    if (dialog.ShowDialog() != DialogResult.OK)
                        return;
                    byte[] outputData = certificateData;
                    string extension = Path.GetExtension(dialog.FileName).ToLowerInvariant();
                    if (extension == ".pem")
                    {
                        string base64 = Convert.ToBase64String(certificateData,Base64FormattingOptions.InsertLineBreaks);
                        string pem ="-----BEGIN CERTIFICATE-----" + Environment.NewLine + base64 + Environment.NewLine + "-----END CERTIFICATE-----" + Environment.NewLine;
                        outputData = Encoding.ASCII.GetBytes(pem);
                    }
                    File.WriteAllBytes(dialog.FileName, outputData);
                    MessageBox.Show("Certificate exported successfully.","Export CA",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Certificate export failed:\n" + ex.Message,"Export CA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
