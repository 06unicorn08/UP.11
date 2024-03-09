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

        ~ReportModel()
        {
            Dispose(false);
        }
    }
}
