using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace task2client
{
    class Client
    {
        private string host;

        public Client(string host)
        {
            this.host = host;
        }

        public void checkPorts()
        {
            int port = 45000;
            byte[] bytes = new byte[1024];

            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket socket = new Socket(ipAddr.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            EndPoint remotePoint = new IPEndPoint(ipAddr, port);

            Console.WriteLine(ipAddr + ":" + port);

            byte[] message = Encoding.UTF8.GetBytes("PORTS");
            socket.SendTo(message, remotePoint);
            int bytesRec = socket.ReceiveFrom(bytes, ref remotePoint);

            Console.WriteLine("Server responded: {0}", Encoding.UTF8.GetString(bytes, 0, bytesRec));
            socket.Close();
        }
    }
}