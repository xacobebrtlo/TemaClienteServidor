using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace sevidor
{
    internal class Program//   isbackground
    {
        private bool flag = true;
        Socket s;
        int port = 31416;//49664;
        bool puertoOcupado = true;
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Conexion();

        }
        public void Conexion()
        {
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                while (puertoOcupado)
                {
                    try
                    {
                        IPEndPoint ie = new IPEndPoint(IPAddress.Any, port);
                        s.Bind(ie);
                        puertoOcupado = false;
                        s.Listen(10);
                        Console.WriteLine($"Server listening at port{ie.Port}");
                    }
                    catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                    {
                        port++;
                        Console.WriteLine($"Port already in use, the new port is:{port}");
                    }
                }

                while (flag)
                {
                    try
                    {
                        Socket cliente = s.Accept();
                        Thread hilo = new Thread(clientThread);
                        hilo.IsBackground = true;
                        hilo.Start(cliente);
                    }
                    catch (SocketException ex)
                    {
                        flag = false;
                    }
                }
            }
        }

        public void clientThread(object socket)
        {
            string message;
            Socket cliente = (Socket)socket;
            IPEndPoint ieCliente = (IPEndPoint)cliente.RemoteEndPoint;
            Console.WriteLine("Connected with client {0} at port {1}", ieCliente.Address, ieCliente.Port);
            using (NetworkStream ns = new NetworkStream(cliente))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                String welcome = "Wellcome to ~XaCoBe´S~ server";
                sw.WriteLine(welcome);
                sw.Flush();
                try
                {
                    message = sr.ReadLine();
                    if (message != null)
                    {
                        string[] messages = message.Split(' ');
                        Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("PROGRAMDATA"));
                        using (StreamReader str = new StreamReader("Contraseña.txt"))
                        {

                            switch (messages[0])
                            {
                                case "time":
                                    sw.Write(DateTime.Now.ToLongTimeString());
                                    break;

                                case "date":
                                    sw.Write(DateTime.Now.Date);
                                    break;
                                case "all":
                                    sw.Write(DateTime.Now);
                                    break;
                                case "close":
                                    if (messages.Length > 1)
                                    {
                                        if (messages[1] == str.ReadToEnd())
                                        {
                                            sw.Write("XaCoBe´S server is closing");
                                            sw.Flush();
                                            s.Close();

                                        }
                                        else if (messages[1] != str.ReadToEnd())
                                        {
                                            sw.Write("Incorrect password");
                                            sw.Flush();
                                        }
                                    }
                                    else
                                    {

                                        sw.Write("Missing password");
                                        sw.Flush();
                                    }


                                    break;
                            }
                            sw.Flush();

                        }
                    }
                }
                catch (IOException ex)
                {

                }
                cliente.Close();


            }
        }
    }
}
