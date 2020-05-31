using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer1
{
    class Program
    {
        //private const int portNum = 13;

        public static void Main(String[] args)
        {
            #region
            //bool done = false;

            //var listener = new TcpListener(IPAddress.Any, portNum);

            //listener.Start();

            //while (!done)
            //{
            //    Console.Write("Waiting for connection...");
            //    TcpClient client = listener.AcceptTcpClient();

            //    Console.WriteLine("Connection accepted.");
            //    NetworkStream ns = client.GetStream();

            //    byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

            //    try
            //    {
            //        ns.Write(byteTime, 0, byteTime.Length);
            //        ns.Close();
            //        client.Close();
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.ToString());
            //    }
            //}

            //listener.Stop();

            //return 0;
            #endregion

            try
            {
                bool status = true;
                string serverMessage = "";
                string clientMessage = "";

                TcpListener tcpListener = new TcpListener(IPAddress.Any,8100);
                tcpListener.Start();
                Console.WriteLine("Server Started.");
                Socket socketForClient = tcpListener.AcceptSocket();
                Console.WriteLine("server Connected.");
                NetworkStream networkStream = new NetworkStream(socketForClient);
                StreamWriter streamWriter = new StreamWriter(networkStream);
                StreamReader streamReader = new StreamReader(networkStream);

                while(status)
                {
                    if(socketForClient.Connected)
                    {
                        serverMessage = streamReader.ReadLine();
                        Console.WriteLine($"Client: {serverMessage}");
                        if(serverMessage == "bye")
                        {
                            status = false;
                            streamReader.Close();
                            streamWriter.Close();
                            networkStream.Close();
                            return;
                        }
                        Console.Write("Server:");
                        clientMessage = Console.ReadLine();
                        streamWriter.WriteLine(clientMessage);
                        streamWriter.Flush();
                    }
                }

                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
                socketForClient.Close();
                Console.WriteLine("Exiting...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
