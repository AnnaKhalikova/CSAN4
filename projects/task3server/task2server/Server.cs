using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace task2server
{
    class Server
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
            ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                sListener.Bind(ipEndPoint);

                while (true)
                {
                    EndPoint remoteIp = new IPEndPoint(ipAddr, port);
                    Console.WriteLine("Waiting for connection on " + ipAddr + ":" + port);

                    byte[] bytes = new byte[1024];
                    int bytesRec = sListener.ReceiveFrom(bytes, ref remoteIp);
                    string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    IPEndPoint remoteFullIp = remoteIp as IPEndPoint;

                    string Ports = "";

                    for (int i = 1; i < 65535; i++)
                    {
                        Socket socket = new Socket(ipAddr.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

                        try
                        {
                            EndPoint remotePoint = new IPEndPoint(ipAddr, i);
                            socket.Bind(remotePoint);
                        }
                        catch (SocketException)
                        {
                            Console.WriteLine(i);
                            Ports += i + " ";
                        }

                        socket.Close();
                    }


                    byte[] msg = Encoding.UTF8.GetBytes(Ports);
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
    }
}
