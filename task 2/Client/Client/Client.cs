using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
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

            IPHostEntry ipHost = Dns.GetHostEntry(host); // v6
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);   //порт должен быть > 1023

            Socket socket = new Socket(ipAddr.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            EndPoint remotePoint = new IPEndPoint(ipAddr, port);

            byte[] message = Encoding.UTF8.GetBytes(DateTime.Now.ToString() + " от клиента");
            socket.SendTo(message, remotePoint);
            int bytesRec = socket.ReceiveFrom(bytes, ref remotePoint);

            Console.WriteLine("Ответ от сервера: {0}", Encoding.UTF8.GetString(bytes, 0, bytesRec));

            string request = Console.ReadLine();
            if (!request.Equals("<TheEnd>"))
                sendMessageFromSocket();
            message = Encoding.UTF8.GetBytes(request);
            socket.SendTo(message, remotePoint);

            socket.Close();
        }
    }
}
