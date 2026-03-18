using System;
using System.Net;
using Microsoft.Office.Interop.Excel;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            // ссылка для прямого скачивания таблицы в формате .xlsx
            string uri = "https://docs.google.com/spreadsheets/d/1YXo9JeQ-stiU_dlAt10VGIWtMy4EwaJ6XyHUDeDDFEE/export?format=xlsx&gid=335190530";

            string localPath = @"C:\Users\Mops\Desktop\google_data.xlsx";

            //скачиваем файл
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(uri, localPath);
                    Console.WriteLine("файл скачан");
                }
                catch (Exception)
                {
                    Console.WriteLine("ошибка при скачивании");
                    return;
                }
            }

            // Читаем файл
            Application excelApp = new Application();

            Workbook excelBook = excelApp.Workbooks.Open(localPath);
            _Worksheet excelSheet = excelBook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

            // вывод только начальных строк
            int rows = Math.Min(excelRange.Rows.Count, 15);
            int cols = Math.Min(excelRange.Columns.Count, 5);

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    Console.Write($"{excelRange.Cells[i, j]?.Value2}\t| ");
                }
                Console.WriteLine();
            }

            // закрыть эксель
            excelBook.Close(false);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

        }
    }
}