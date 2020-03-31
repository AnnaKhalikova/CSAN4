using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TcpClient
{
    class Client
    {
        private const int SIZE = 512;

        static void Main(string[] args)
        {
            try
            {
                Console.Write("Server port: ");
                int port = int.Parse(Console.ReadLine());
                Socket socket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(
                    IPAddress.Parse("127.0.0.1"), port);
                socket.Connect(endPoint);

                string message;
                string date;

                while (true)
                {
                    Console.Write("Message: ");
                    message = Console.ReadLine();
                    byte[] byteSend = new byte[SIZE];
                    byteSend = Encoding.ASCII.GetBytes(message);
                    socket.Send(byteSend);

                    if (message.IndexOf("<date>") > -1)
                    {
                        byte[] byteReceive = new byte[SIZE];
                        int lenReceive = socket.Receive(byteReceive);
                        date = Encoding.ASCII.GetString(byteReceive, 0, lenReceive);
                        Console.WriteLine("date: " + date);
                    }

                    if (message.IndexOf("<end>") > -1) break;
                }

                socket.Shutdown(SocketShutdown.Send);
                socket.Close();
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
