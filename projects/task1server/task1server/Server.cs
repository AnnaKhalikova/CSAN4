using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace task1server
{
    class Server
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
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                while (true)
                {

                    Console.WriteLine("\nWaiting for connection from {0}", ipEndPoint);
                    Socket client = sListener.Accept();

                    printSocketInfo("Server", sListener);
                    printSocketInfo("Client", client);

                    byte[] bytes = new byte[1024];
                    int bytesRec = client.Receive(bytes);
                    string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    Console.Write("Received data: " + data + "\n\n");
                    byte[] msg = Encoding.UTF8.GetBytes(DateTime.Now.ToString() + " from server");
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

        void printSocketInfo(String socketName, Socket socket)
        {
            Console.WriteLine(socketName);
            Console.WriteLine("\tDescriptor - " + socket.Handle.ToString());
            Console.WriteLine("\tIP:port - " + socket.LocalEndPoint.ToString());
        }
    }
}
