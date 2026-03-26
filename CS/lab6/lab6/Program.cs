using System;
using System.Net;
using System.Threading;
using Microsoft.Office.Interop.Excel;

namespace Lab6
{
    class Program
    {
        //для синхронизации вывода в консоль
        static object consoleLock = new object();

        static void Main(string[] args)
        {
            string uri = "https://docs.google.com/spreadsheets/d/1YXo9JeQ-stiU_dlAt10VGIWtMy4EwaJ6XyHUDeDDFEE/export?format=xlsx";
            string localPath = @"C:\Users\Mops\Desktop\google_data.xlsx";

            Console.WriteLine("скачиваем файл");
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(uri, localPath);
                    Console.WriteLine("файл скачан");
                }
                catch (Exception)
                {
                    Console.WriteLine("файл не скачан");
                    return;
                }
            }


            // Создаем два потока, передаем им путь к файлу, номер листа и задержку
            Thread thread1 = new Thread(() => ReadExcelSheet(localPath, 1, false));
            thread1.Name = "Поток 1 (Лист 1)";

            Thread thread2 = new Thread(() => ReadExcelSheet(localPath, 2, true));
            thread2.Name = "Поток 2 (Лист 2)";

            // до запуска
            Console.WriteLine($"{thread1.Name} статус: {thread1.ThreadState}");
            Console.WriteLine($"{thread2.Name} статус: {thread2.ThreadState}\n");

            // Запуск
            thread1.Start();
            thread2.Start();

            // во время работы
            Console.WriteLine($"{thread1.Name} статус: {thread1.ThreadState}");
            Console.WriteLine($"{thread2.Name} статус: {thread2.ThreadState}\n");

            // ожидание завершения всех потоков
            thread1.Join();
            thread2.Join();

            // после работы
            Console.WriteLine($"\n{thread1.Name} статус: {thread1.ThreadState}");
            Console.WriteLine($"{thread2.Name} статус: {thread2.ThreadState}");

            Console.WriteLine("конец");
            Console.ReadLine();
        }

        //будет выполняться внутри каждого потока
        static void ReadExcelSheet(string path, int sheetIndex, bool applyDelay)
        {
            // если нужна задержка то поток на 2 секунды засыпает
            if (applyDelay)
            {
                Thread.Sleep(2000);
            }

            Application excelApp = new Application();
            if (excelApp == null) return;

            Workbook excelBook = null;
            try
            {
                //файл в режиме ReadOnly, чтобы потоки не заблокировали его друг от друга
                excelBook = excelApp.Workbooks.Open(path, ReadOnly: true);
                _Worksheet excelSheet = excelBook.Sheets[sheetIndex];
                Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

                int rows = Math.Min(excelRange.Rows.Count, 15);
                int cols = Math.Min(excelRange.Columns.Count, 5);

                // Блокируем консоль, чтобы таблица вывелась ровно
                lock (consoleLock)
                {
                    Console.WriteLine($"поток: {Thread.CurrentThread.Name}");
                    for (int i = 1; i <= rows; i++)
                    {
                        for (int j = 1; j <= cols; j++)
                        {
                            Console.Write($"{excelRange.Cells[i, j]?.Value2}\t| ");
                        }
                        Console.WriteLine();
                    }
                }
            }
            
            finally
            {
                if (excelBook != null) excelBook.Close(false);
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }
    }
}