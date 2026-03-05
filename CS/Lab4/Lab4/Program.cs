using System;
using System.Diagnostics;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            // получить все запущенные процессы
            Console.WriteLine("=== Запущенные процессы ===");

            foreach (Process process in Process.GetProcesses()) // это массив его перебирает форич и выводит из него элементы
            {
                Console.WriteLine($"ID: {process.Id} | Name: {process.ProcessName}");
            }

            Console.WriteLine("\nНажмите Enter, чтобы перейти к потокам");
            Console.ReadLine();

            // получить все потоки VS
            Console.WriteLine("\n=== Потоки процесса Visual Studio ===");
            try
            {
                Process proc = Process.GetProcessesByName("devenv")[0]; // процесс визуалки
                ProcessThreadCollection processThreads = proc.Threads; // выводим коллекцию потоков этого процесса (vs)

                foreach (ProcessThread thread in processThreads) // перебираю коллекцию и вывожу id каждого потока
                {
                    Console.WriteLine($"ThreadId: {thread.Id}");
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Ошибка: Visual Studio сейчас не запущена");
            }

            Console.WriteLine("\nНажмите Enter, чтобы запустить сторонние процессы");
            Console.ReadLine();

            // запустить несколько сторонних процессов
            Console.WriteLine("\n=== Запуск сторонних процессов ===");

            Console.WriteLine("Запускаем блокнот");
            Process.Start("mspaint.exe");

            Console.WriteLine("Запускаем яндекс с открытием сайта");
            try
            {
                ProcessStartInfo procInfo = new ProcessStartInfo(); // сразу даем ему путь к браузеру и аргументом сайт, который нужно открыть
                procInfo.FileName = @"C:\Program Files\Yandex\YandexBrowser\Application\browser.exe";
                procInfo.Arguments = "https://metanit.com";
                Process.Start(procInfo);
            }
            catch (Exception)
            {
                Console.WriteLine("не удалось");
            }
        }
    }
}