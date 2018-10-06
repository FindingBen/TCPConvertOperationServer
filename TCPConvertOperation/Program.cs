using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPConvertOperation
{
    class Program
    {
        private static readonly int PORT = 8;
        private static StreamWriter _sWriter = null;
        private static StreamReader _sReader = null;
        static void Main(string[] args)
        {

            IPAddress localAddress = IPAddress.Loopback;  //127.0.0.1
            TcpListener serverSocket = new TcpListener(localAddress, PORT);
            serverSocket.Start();
            Console.WriteLine("TP Server running on port" + PORT);

            while (true) //server loop, keeps server alive forever
            {
                try
                {
                    //waiting for clients
                    TcpClient clients = serverSocket.AcceptTcpClient();
                    Console.WriteLine("Incoming client");
                    Task.Run(() => DoIt(clients));


                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }
        //Read something from Client input
        private static void DoIt(TcpClient clients)
        {
            NetworkStream stream = clients.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            //accepts more clients
            while (true)
            {
                string request = reader.ReadLine();
                if (request == "bye") break;


                Console.WriteLine("Request: " + request);
                var splitRequest = request.Split();
                double resultConvert = 0;
                float num = float.Parse(splitRequest[1]);

                switch (splitRequest[0])
                {
                    case ("TOOUNCES"):

                         double result = ComponentConvert.Class1.ConvertGramsToOunces(num);
                        Console.WriteLine("Result:"+" "+result);
                        break;
                    case ("TOGRAMS"):

                        double result2 = ComponentConvert.Class1.ConvertOuncesToGram(num);
                        Console.WriteLine("Result:" +" "+ result2);
                        break;
                }


                writer.WriteLine(request);
                writer.Flush();
            }
            clients.Close();
        }
    }
}
