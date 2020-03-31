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
                    Console.WriteLine("Waiting for connection from port {0}", ipEndPoint);
                    Console.WriteLine("Descriptor - " + sListener.Handle.ToString());
                    EndPoint remoteIp = new IPEndPoint(ipAddr, port);

                    byte[] bytes = new byte[1024];
                    int bytesRec = sListener.ReceiveFrom(bytes, ref remoteIp);
                    string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    IPEndPoint remoteFullIp = remoteIp as IPEndPoint;

                    Console.WriteLine("Client: - " + remoteFullIp.Address + ":" + remoteFullIp.Port);
                    Console.Write("Received: " + data + "\n\n");
                    byte[] msg = Encoding.UTF8.GetBytes(DateTime.Now.ToString() + " from server");
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
