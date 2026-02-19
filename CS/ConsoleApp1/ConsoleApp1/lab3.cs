using System;
using Microsoft.Office.Interop.Excel;

namespace Lab3
{
    class lab3
    {
        static void Main(string[] args)
        {
            //COM объект
            Application excelApp = new Application();
            if (excelApp == null)
            {
                return;
            }

            string filePath = @"C:\Users\Barebuh\source\repos\Barebuh8481\Petrov_MDSP\a.xlsx";

            //книгу и получаем первый лист
            Workbook excelBook = excelApp.Workbooks.Open(filePath);
            _Worksheet excelSheet = excelBook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

            // количество строк и колонок
            int rows = excelRange.Rows.Count;
            int cols = excelRange.Columns.Count;

            //выводим данные 
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    if (excelRange.Cells[i, j] != null && excelRange.Cells[i, j].Value2 != null)
                    {
                        //значение ячейки
                        Console.Write(excelRange.Cells[i, j].Value2.ToString() + "\t| ");
                    }
                }
                Console.WriteLine(); // Переход на новую строку
            }

            excelBook.Close(false);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            Console.ReadLine();
        }
    }
}