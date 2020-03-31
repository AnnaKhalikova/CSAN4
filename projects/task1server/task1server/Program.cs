using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace task1server
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            int port = 11000;

            Server server = new Server(host, port);
            server.run();
        }
    }
}
