using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using System.IO;
using System.Windows.Forms;
using System.Windows;

namespace WindowsFormsLibrary.TelerikWinform.GridViewCommon.GridViewDataExport
{
    class ExportData
    {
        public static void RunExportToExcelML(string fileName, RadGridView radGridView)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(radGridView);
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;

            //set export settings
            //excelExporter.ExportVisualSettings = this.radCheckBoxExportVisual.IsChecked;
            //excelExporter.ExportHierarchy = this.radCheckBoxExportHierarchy.IsChecked;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                excelExporter.RunExport(fileName);

                RadMessageBox.SetThemeName(radGridView.ThemeName);
                DialogResult dr = RadMessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                    catch (Exception ex)
                    {
                        string message = String.Format("The file cannot be opened on your system.\nError message: {0}", ex.Message);
                        RadMessageBox.Show(message, "Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                MessageBox.Show(ex.Message,"ERR",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        public static void RunExportToCSV(string fileName, RadGridView radGridView)
        {
            ExportToCSV csvExporter = new ExportToCSV(radGridView);
            csvExporter.CSVCellFormatting += csvExporter_CSVCellFormatting;
            csvExporter.SummariesExportOption = SummariesOption.ExportAll;

            //set export settings
            //csvExporter.ExportHierarchy = this.radCheckBoxExportHierarchy.IsChecked;
            csvExporter.HiddenColumnOption = HiddenOption.DoNotExport;

            try
            {
                //this.Cursor = Cursors.WaitCursor;

                csvExporter.RunExport(fileName);

                RadMessageBox.SetThemeName(radGridView.ThemeName);
                DialogResult dr = RadMessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to CSV", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                    catch (Exception ex)
                    {
                        string message = String.Format("The file cannot be opened on your system.\nError message: {0}", ex.Message);
                        RadMessageBox.Show(message, "Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                //RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                MessageBox.Show(ex.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private static void csvExporter_CSVCellFormatting(object sender, Telerik.WinControls.UI.Export.CSV.CSVCellFormattingEventArgs e)
        {
            if (e.GridCellInfo.ColumnInfo is GridViewDateTimeColumn)
            {
                e.CSVCellElement.Value = FormatDate(e.CSVCellElement.Value);
            }
        }

        private static string FormatDate(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.ToString("d MMM yyyy");
            }

            return value.ToString();
        }

        public static void RunExportToHTML(string fileName, RadGridView radGridView)
        {
            ExportToHTML htmlExporter = new ExportToHTML(radGridView);
            htmlExporter.HTMLCellFormatting += htmlExporter_HTMLCellFormatting;

            htmlExporter.SummariesExportOption = SummariesOption.ExportAll;

            //set export settings
            //htmlExporter.ExportVisualSettings = this.radCheckBoxExportVisual.IsChecked;
            //htmlExporter.ExportHierarchy = this.radCheckBoxExportHierarchy.IsChecked;
            htmlExporter.HiddenColumnOption = HiddenOption.DoNotExport;

            try
            {
                //this.Cursor = Cursors.WaitCursor;

                htmlExporter.RunExport(fileName);

                RadMessageBox.SetThemeName(radGridView.ThemeName);
                DialogResult dr = RadMessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to HTML", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                    catch (Exception ex)
                    {
                        string message = String.Format("The file cannot be opened on your system.\nError message: {0}", ex.Message);
                        RadMessageBox.Show(message, "Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                //RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                MessageBox.Show(ex.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private static void htmlExporter_HTMLCellFormatting(object sender, Telerik.WinControls.UI.Export.HTML.HTMLCellFormattingEventArgs e)
        {
            if (e.GridCellInfo.ColumnInfo is GridViewDateTimeColumn)
            {
                e.HTMLCellElement.Value = FormatDate(e.HTMLCellElement.Value);
            }
        }

        public static void RunExportToPDF(string fileName, RadGridView radGridView)
        {
            ExportToPDF pdfExporter = new ExportToPDF(radGridView);
            pdfExporter.PdfExportSettings.Title = "My PDF Title";
            pdfExporter.PdfExportSettings.PageWidth = 297;
            pdfExporter.PdfExportSettings.PageHeight = 210;
            pdfExporter.FitToPageWidth = true;
            pdfExporter.HTMLCellFormatting += pdfExporter_HTMLCellFormatting;

            pdfExporter.SummariesExportOption = SummariesOption.ExportAll;

            //set export settings
            //pdfExporter.ExportVisualSettings = this.radCheckBoxExportVisual.IsChecked;
            //pdfExporter.ExportHierarchy = this.radCheckBoxExportHierarchy.IsChecked;
            pdfExporter.HiddenColumnOption = HiddenOption.DoNotExport;

            try
            {
                //this.Cursor = Cursors.WaitCursor;

                pdfExporter.RunExport(fileName);

                RadMessageBox.SetThemeName(radGridView.ThemeName);
                DialogResult dr = RadMessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to PDF", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                    catch (Exception ex)
                    {
                        string message = String.Format("The file cannot be opened on your system.\nError message: {0}", ex.Message);
                        RadMessageBox.Show(message, "Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }

            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                //RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                MessageBox.Show(ex.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private static void pdfExporter_HTMLCellFormatting(object sender, Telerik.WinControls.UI.Export.HTML.HTMLCellFormattingEventArgs e)
        {
            if (e.GridCellInfo.ColumnInfo is GridViewDateTimeColumn)
            {
                e.HTMLCellElement.Value = FormatDate(e.HTMLCellElement.Value);
            }
        }
    }
}
