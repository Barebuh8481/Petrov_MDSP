using System;
using System.IO;
using System.IO.Pipes;

namespace PipeServer
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Сервер");
            // создание канала сервером
            using (NamedPipeServerStream pipestream = new NamedPipeServerStream("Pipe"))
            {
                Console.WriteLine("Ожидание клиента");
                pipestream.WaitForConnection();
                Console.WriteLine("Клиент подключен \n");

                using (StreamReader reader = new StreamReader(pipestream))
                using (StreamWriter writer = new StreamWriter(pipestream))
                {
                    writer.AutoFlush = true; // чтобы сообщения сразу приходили
                    string ss = "";
                    int fl = 0; // поочередный обмен (0 - сервер, 1 - клиент)

                    // 3. Обмен данными до слова "ДОМОЙ
                    while (ss != "ДОМОЙ")
                    {
                        if (fl == 0)
                        {
                            Console.Write("Ваше сообщение ? ");
                            ss = Console.ReadLine();
                            writer.WriteLine(ss);
                            fl = 1; // передаем ход клиенту
                        }
                        else
                        {
                            ss = reader.ReadLine();
                            Console.WriteLine("Получено сообщение от клиента: " + ss);
                            fl = 0; // наш ход
                        }
                    }
                }
            }
            Console.WriteLine("Конец сеанса");
            Console.ReadLine();
        }
    }
}