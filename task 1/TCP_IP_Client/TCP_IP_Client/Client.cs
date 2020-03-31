// SocketClient.cs
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCP_IP_Client
{
    public class Client
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

            IPHostEntry ipHost = Dns.GetHostEntry(host); // v6
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);   //порт должен быть > 1023

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(ipEndPoint);

            Console.WriteLine("Сокет соединяется с {0} ", sender.RemoteEndPoint.ToString());

            byte[] message = Encoding.UTF8.GetBytes(DateTime.Now.ToString() + " от клиента");
            sender.Send(message);
            int bytesRec = sender.Receive(bytes); 

            Console.WriteLine("Ответ от сервера: {0}", Encoding.UTF8.GetString(bytes, 0, bytesRec));

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