using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
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
                Console.WriteLine("Начало сеанса");
                client.sendMessageFromSocket();
                Console.WriteLine("Завершение сеанса");

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
