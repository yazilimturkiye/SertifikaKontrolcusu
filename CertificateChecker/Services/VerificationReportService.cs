using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CertificateChecker.Models;

namespace CertificateChecker.Services
{
    public static class VerificationReportService
    {
        public static void Render(ListView listView,IEnumerable<VerificationCheckResult> results)
        {
            if (listView == null)
                throw new ArgumentNullException(nameof(listView));
            listView.BeginUpdate();
            try
            {
                listView.Items.Clear();
                if (results == null)
                    return;
                foreach (VerificationCheckResult result in results)
                {
                    if (result == null)
                        continue;
                    AddResult(listView,result.Component,result.Status,result.Description,result.Source);
                }
            }
            finally
            {
                listView.EndUpdate();
            }
        }
        public static void AddResult(ListView listView,string component,string status,string description,string source = "-")
        {
            if (listView == null)
                throw new ArgumentNullException(nameof(listView));
            var item = new ListViewItem(component ?? string.Empty);
            item.SubItems.Add(status ?? "UNKNOWN");
            item.SubItems.Add(description ?? string.Empty);
            item.SubItems.Add(source ?? "-");
            item.UseItemStyleForSubItems = false;
            item.SubItems[1].ForeColor = GetStatusColor(status);
            listView.Items.Add(item);
        }
        public static void Clear(ListView listView)
        {
            if (listView == null)
                throw new ArgumentNullException(nameof(listView));

            listView.Items.Clear();
        }
        public static void BeginUpdate(ListView listView)
        {
            listView?.BeginUpdate();
        }
        public static void EndUpdate(ListView listView)
        {
            listView?.EndUpdate();
        }
        public static Color GetStatusColor(string status)
        {
            switch (status?.Trim().ToUpperInvariant())
            {
                case "OK":
                case "SUCCESS":
                case "VALID":
                    return Color.Green;
                case "WARNING":
                case "WARN":
                    return Color.DarkOrange;
                case "ERROR":
                case "CRITICAL":
                case "FAILED":
                case "FAIL":
                    return Color.Red;
                case "INFO":
                    return Color.DodgerBlue;
                case "UNKNOWN":
                case "NOT CHECKED":
                    return Color.Gray;
                case "REVIEW":
                    return Color.DarkViolet;
                default:
                    return Color.Black;
            }
        }
    }
}