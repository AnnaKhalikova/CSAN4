// SocketServer.cs
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCP_IP_Server

{
    public class Server
    {
        private string host;
        private int port;

        public Server(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public void run()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0]; // связанные с узлом
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sListener.Bind(ipEndPoint); // привязка к сокету
                sListener.Listen(10); //перевод сокета в режим прослушивания (10-количество клиентов в очереди)

                while (true)
                {

                    Console.WriteLine("\nОжидаем соединение через порт {0}", ipEndPoint);
                    Socket client = sListener.Accept();

                    printSocketInfo("Сервер", sListener);
                    printSocketInfo("Клиент", client);

                    byte[] bytes = new byte[1024];
                    int bytesRec = client.Receive(bytes);
                    string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    Console.Write("Полученный текст: " + data + "\n\n");
                    byte[] msg = Encoding.UTF8.GetBytes(DateTime.Now.ToString() + " от сервера");
                    client.Send(msg);
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }

        }

        void printSocketInfo(String socketName,Socket socket)
        {
            Console.WriteLine(socketName);
            Console.WriteLine("\tДескриптор - " + socket.Handle.ToString());
            Console.WriteLine("\t[IP]::порт - " + socket.LocalEndPoint.ToString());
        }
    }
}