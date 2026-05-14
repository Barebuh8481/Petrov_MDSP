using System;
using System.IO;
using System.IO.Pipes;

namespace PipeClient
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Клиент");
            string ss = "";

            // именованный канал клиентом (подключется к "Pipe")
            using (NamedPipeClientStream pipestream = new NamedPipeClientStream(".", "Pipe", PipeDirection.InOut))
            {
                //Соединение клиента с каналом
                pipestream.Connect();
                Console.WriteLine("Успешно подключились к серверу!\n");

                using (StreamReader reader = new StreamReader(pipestream))
                using (StreamWriter writer = new StreamWriter(pipestream))
                {
                    writer.AutoFlush = true;
                    int fl = 0;

                    // обмен
                    do
                    {
                        if (fl == 0)
                        {
                            ss = reader.ReadLine();
                            Console.WriteLine("сообщение от сервера: " + ss);
                            fl = 1; // очередь клиента
                        }
                        else
                        {
                            Console.Write("сообщение от клиента ? ");
                            ss = Console.ReadLine();
                            writer.WriteLine(ss);
                            fl = 0; // очередь сервера
                        }
                    }
                    while (ss != "ДОМОЙ");
                }
            }
            Console.WriteLine("Конец сеанса");
            Console.ReadLine();
        }
    }
}