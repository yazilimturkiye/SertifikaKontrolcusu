using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.IO;
using System.Data.SQLite;
using System.Windows.Forms;

namespace CertificateChecker
{
    public partial class CA_Verification_UserControl : UserControl
    {
        public CA_Verification_UserControl()
        {
            InitializeComponent();
            LoadCATreeView();
        }
        private class CARecord
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
        private void LoadCATreeView()
        {
            string dbPath = Path.Combine(
                Application.StartupPath,
                "Data",
                "CertificateAuthority.db"
            );

            if (!File.Exists(dbPath))
            {
                MessageBox.Show("CA database not found:\n" + dbPath);
                return;
            }

            Dictionary<int, CARecord> certificates =
                new Dictionary<int, CARecord>();

            Dictionary<int, List<int>> children =
                new Dictionary<int, List<int>>();

            string connectionString =
                $"Data Source={dbPath};Version=3;Read Only=True;";

            using (SQLiteConnection connection =
                   new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Sertifikaları oku
                string certificateQuery = @"
            SELECT certificate_id, display_name, certificate_type
            FROM ca_certificates
            ORDER BY certificate_id;";

                using (SQLiteCommand command =
                       new SQLiteCommand(certificateQuery, connection))
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

                        certificates[certificate.Id] = certificate;
                    }
                }

                // Parent - Child ilişkilerini oku
                string issuerQuery = @"
            SELECT certificate_id, issuer_certificate_id
            FROM certificate_issuers
            ORDER BY issuer_certificate_id, certificate_id;";

                using (SQLiteCommand command =
                       new SQLiteCommand(issuerQuery, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int childId =
                            Convert.ToInt32(reader["certificate_id"]);

                        int parentId =
                            Convert.ToInt32(reader["issuer_certificate_id"]);

                        if (!certificates.ContainsKey(childId) ||
                            !certificates.ContainsKey(parentId))
                            continue;

                        if (!children.ContainsKey(parentId))
                            children[parentId] = new List<int>();

                        if (!children[parentId].Contains(childId))
                            children[parentId].Add(childId);
                    }
                }
            }

            TreeView_CA.BeginUpdate();

            try
            {
                TreeView_CA.Nodes.Clear();

                // Root CA kayıtlarını ID sırasına göre ekle
                var roots = certificates.Values
                    .Where(x => x.Type.Equals(
                        "Root",
                        StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Id);

                foreach (CARecord root in roots)
                {
                    TreeNode rootNode = new TreeNode(root.Name)
                    {
                        Tag = root.Id
                    };

                    TreeView_CA.Nodes.Add(rootNode);

                    AddCAChildren(
                        rootNode,
                        root.Id,
                        certificates,
                        children,
                        new HashSet<int>()
                    );
                }
            }
            finally
            {
                TreeView_CA.EndUpdate();
            }
        }
        private void AddCAChildren(
    TreeNode parentNode,
    int parentId,
    Dictionary<int, CARecord> certificates,
    Dictionary<int, List<int>> children,
    HashSet<int> path)
        {
            // Döngüsel ilişkileri engelle
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

                    AddCAChildren(
                        childNode,
                        childId,
                        certificates,
                        children,
                        path
                    );
                }
            }

            path.Remove(parentId);
        }
    }
}
