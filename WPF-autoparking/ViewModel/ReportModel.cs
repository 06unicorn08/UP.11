using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace WPF_autoparking.ViewModel
{
    internal class ReportModel : IDisposable
    {
        Word.Application app = new Word.Application();
        Word.Document doc;


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Освобождение управляемых ресурсов
                if (doc != null)
                {
                    try
                    {
                        doc.Saved = true;
                        doc.Close();
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(doc);
                        doc = null;
                    }
                }

                if (app != null)
                {
                    try
                    {
                        app.Quit();
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(app);
                        app = null;
                    }
                }
            }
        }

        public async Task CarGenAsync()
        {
            await Task.Run(() =>
            {
                var DataList = AutoParkEntities.GetContext().CarWithCategories.Take(10).ToList();
                var application = new Word.Application();
                var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "Cars.docx");
                Word.Document document = application.Documents.Add(Template: templatePath, Visible: true);

                Word.Table table = document.Tables[1];


                int rowIndex = 2;
                foreach (var data in DataList)
                {
                    var newRow = table.Rows.Add();

                    newRow.Cells[1].Range.Text = $"{data.Марка} {data.Модель}, {data.Цвет}";
                    newRow.Cells[2].Range.Text = data.Год.ToString();
                    newRow.Cells[3].Range.Text = data.Цена_за_сутки.ToString();
                    newRow.Cells[4].Range.Text = data.Адрес.ToString();
                }
                document.Bookmarks["Table"].Range.Tables[1].Rows[2].Delete();

                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Документ Word (*.docx)|*.docx",
                    Title = "Сохранить отчет"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    document.SaveAs2(saveFileDialog.FileName);
                    document.Close();
                }

                application.Quit();
            });
        }

        public async Task PayGenAsync(int id)
        {
            await Task.Run(() =>
            {
                var DataList = AutoParkEntities.GetContext().GetPaymentsByStatus(id).ToList();
                var application = new Word.Application();
                var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "Payment.docx");
                Word.Document document = application.Documents.Add(Template: templatePath, Visible: true);

                Word.Table table = document.Tables[1];


                int rowIndex = 2;
                foreach (var data in DataList)
                {
                    var newRow = table.Rows.Add();

                    newRow.Cells[1].Range.Text = $"{data.payment_id}";
                    newRow.Cells[2].Range.Text = $"{data.rental_id}";
                    newRow.Cells[3].Range.Text = $"{data.payment_status}";
                }
                document.Bookmarks["Table"].Range.Tables[1].Rows[2].Delete();

                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Документ Word (*.docx)|*.docx",
                    Title = "Сохранить отчет"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    document.SaveAs2(saveFileDialog.FileName);
                    document.Close();
                }

                application.Quit();
            });
        }
        public async Task PayFullGenAsync()
        {
            await Task.Run(() =>
            {
                var excelApp = new Excel.Application();
                var workbook = excelApp.Workbooks.Add();

                Excel.Worksheet worksheetPaid = (Excel.Worksheet)workbook.Worksheets.Add();
                worksheetPaid.Name = "Оплачено";

                Excel.Worksheet worksheetPending = (Excel.Worksheet)workbook.Worksheets.Add();
                worksheetPending.Name = "Ожидание";

                Excel.Worksheet worksheetUnpaid = (Excel.Worksheet)workbook.Worksheets.Add();
                worksheetUnpaid.Name = "Не оплачено";

                FillSheet(worksheetPaid, 1);
                FillSheet(worksheetPending, 2);
                FillSheet(worksheetUnpaid, 3);

                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Документ Excel (*.xlsx)|*.xlsx",
                    Title = "Сохранить отчет"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    workbook.Close();
                }

                excelApp.Quit();
            });
        }

        private void FillSheet(Excel.Worksheet sheet, int status)
        {
            var context = AutoParkEntities.GetContext();
            var payments = context.GetPaymentsByStatus(status).ToList();

            sheet.Cells[1, 1].Value = "№ оплаты";
            sheet.Cells[1, 2].Value = "№ аренды";
            sheet.Cells[1, 3].Value = "Статус оплаты";

            sheet.Cells[1, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            sheet.Cells[1, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            sheet.Cells[1, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            int rowIndex = 2;
            foreach (var payment in payments)
            {
                sheet.Cells[rowIndex, 1].Value = payment.payment_id;
                sheet.Cells[rowIndex, 2].Value = payment.rental_id;
                sheet.Cells[rowIndex, 3].Value = payment.payment_status;

                sheet.Cells[rowIndex, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                sheet.Cells[rowIndex, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                sheet.Cells[rowIndex, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                rowIndex++;
            }
            sheet.Columns.AutoFit();
        }

        ~ReportModel()
        {
            Dispose(false);
        }
    }
}
