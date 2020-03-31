using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace task2client
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            int port = 48000;

            try
            {
                Client client = new Client(host, port);
                Console.WriteLine("Connecting through port {0}", port);
                client.sendMessageFromSocket();
                Console.WriteLine("Session finished");

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
