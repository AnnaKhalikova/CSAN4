using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace task1client
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            int port = 11000;

            try
            {
                Client client = new Client(host, port);
                Console.WriteLine("Session started");
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
