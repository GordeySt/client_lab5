using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ConsoleClient
{
    class Program
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        static void Main(string[] args)
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                BinaryWriter bw = new BinaryWriter(stream);
                BinaryReader br = new BinaryReader(stream);

                
                    SendDataToServer(bw);
                    GetMessagesFromServer(br);
                    Console.ReadKey();
                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }

        static void SendDataToServer(BinaryWriter stream)
        {
            Console.Write("Enter your username: ");
            string currentUserName = Console.ReadLine();
            Console.Write("Enter username to send message: ");
            string userNameToSend = Console.ReadLine();
            Console.Write("Enter subject of message: ");
            string subject = Console.ReadLine();
            Console.WriteLine("Enter your message below: ");
            string message = Console.ReadLine();


            // отправка сообщения
            stream.Write(currentUserName);
            stream.Write(userNameToSend);
            stream.Write(subject);
            stream.Write(message);
        }

        static void GetMessagesFromServer(BinaryReader stream)
        {
            List<string> messages = new List<string>();
            int count = stream.ReadInt32();

            for(int i = 0; i < count; i++)
            {
                string message = stream.ReadString();
                messages.Add(message);
            }

            Console.WriteLine("\nYour messages");
            foreach (var item in messages)
            {
                
                Console.WriteLine($"\n{item}");
            }
        }
    }
}