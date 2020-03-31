using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        private string host;
        private int port;
        private IPAddress ipAddr;

        public Server(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public void run()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            ipAddr = ipHost.AddressList[0]; // связанные с узлом
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                sListener.Bind(ipEndPoint); // привязка к сокету

                while (true)
                {

                    Console.WriteLine("\nОжидаем соединение через порт {0}", ipEndPoint);

                    EndPoint remoteIp = new IPEndPoint(ipAddr, port);

                    printSocketInfo("Сервер", sListener);

                    byte[] bytes = new byte[1024];
                    int bytesRec = sListener.ReceiveFrom(bytes, ref remoteIp);
                    string data = Encoding.UTF8.GetString(bytes, 2, bytesRec);
                    IPEndPoint remoteFullIp = remoteIp as IPEndPoint;

                    Console.WriteLine("Клиент");
                    Console.WriteLine("\t[IP]::порт - " + remoteFullIp.Address +
                        ":" + remoteFullIp.Port);
    

                    Console.Write("Полученный текст: " + data + "\n\n");
                    byte[] msg = Encoding.UTF8.GetBytes(DateTime.Now.ToString() + " от сервера");
                    sListener.SendTo(msg, remoteFullIp);
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

        void printSocketInfo(String socketName, Socket socket)
        {
            Console.WriteLine(socketName);
            Console.WriteLine("\tДескриптор - " + socket.Handle.ToString());
            Console.WriteLine("\t[IP]::порт - " + socket.LocalEndPoint.ToString());
        }
    }
}
