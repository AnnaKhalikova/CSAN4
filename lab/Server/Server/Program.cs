using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TcpServer
{
    class Server
    {
        private const int PORT = 5555;
        private const int SIZE = 512;
        private const int LEN = 10;

        static void Main(string[] args)
        {
            try
            {
                Socket listenerSocket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(
                    IPAddress.Parse("0.0.0.0"), PORT);

                listenerSocket.Bind(endPoint);
                listenerSocket.Listen(LEN);

                Console.WriteLine("Description: "
                    + listenerSocket.Handle);
                Console.WriteLine("IP address and port: "
                    + listenerSocket.LocalEndPoint);

                String date;
                String clientMessage;

                while (true)
                {
                    Socket workSocket = listenerSocket.Accept();
                    Console.WriteLine("Description: "
                        + workSocket.Handle);
                    Console.WriteLine("IP address and port: "
                        + workSocket.RemoteEndPoint);

                    while (true)
                    {
                        byte[] byteReceive = new byte[SIZE];
                        int lenReceive = workSocket.Receive(byteReceive);
                        clientMessage = Encoding.ASCII.GetString(byteReceive, 0, lenReceive);
                        Console.WriteLine("Client say: " + clientMessage);

                        if (clientMessage.IndexOf("<date>") > -1)
                        {
                            byte[] byteSend = new byte[SIZE];
                            date = DateTime.Now.ToString();
                            byteSend = Encoding.ASCII.GetBytes(date);
                            workSocket.Send(byteSend);
                            Console.WriteLine("Send: " + date);
                        }

                        if (clientMessage.IndexOf("<end>") > -1) break;
                    }

                    workSocket.Shutdown(SocketShutdown.Both);
                    workSocket.Close();
                }
            }
            catch (SocketException exception)
            {
                Console.WriteLine("Vse ploho, ochen\' ploho");
            }
            finally
            {
                Console.WriteLine("Press \'Enter\' to exit: ");
                Console.ReadLine();
            }
        }
    }
}
