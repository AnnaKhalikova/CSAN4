using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace task1client
{
    class Client
    {
        private string host;
        private int port;

        public Client(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public void sendMessageFromSocket()
        {
            byte[] bytes = new byte[1024];

            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(ipEndPoint);

            Console.WriteLine("Socket is connecting to {0} ", sender.RemoteEndPoint.ToString());

            byte[] message = Encoding.UTF8.GetBytes(DateTime.Now.ToString() + " from client");
            sender.Send(message);
            int bytesRec = sender.Receive(bytes);

            Console.WriteLine("Server responded with: {0}", Encoding.UTF8.GetString(bytes, 0, bytesRec));

            string request = Console.ReadLine();
            if (!request.Equals("<TheEnd>"))
                sendMessageFromSocket();
            message = Encoding.UTF8.GetBytes(request);
            sender.Send(message);

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
