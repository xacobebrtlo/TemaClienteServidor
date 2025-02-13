using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace Servidor
{
    internal class Program//Locks.  
    {
        private readonly static object l = new object();
        Socket s;
        List<StreamWriter> StreamWriters = new List<StreamWriter>();
        List<string> usernames = new List<string>();
        int port = 31416;//49664;
        bool puertoOcupado = true;
        bool flag = true;
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
                        Thread hilo = new Thread(clienteThread);
                        hilo.IsBackground = true;
                        hilo.Start(cliente);
                    }
                    catch (SocketException e)
                    {
                        flag = false;
                    }
                }
            }
        }

        public void clienteThread(object socket)
        {
            string message;
            string userName;
            bool connected = true;
            Socket cliente = (Socket)socket;
            IPEndPoint ieCliente = (IPEndPoint)cliente.RemoteEndPoint;
            Console.WriteLine("Connected");

            using (NetworkStream ns = new NetworkStream(cliente))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                //Estaba añadiendo el sw a la coleccion de StreamWriters
                string welcome = "Write your name to log in";
                sw.WriteLine(welcome);
                sw.Flush();
                userName = sr.ReadLine();//Lo pongo fuera del try, porque asi al cerrar bruscamente se borra el User de la lista
                if (userName != null)
                {
                    lock (l)
                    {
                        usernames.Add(userName);
                        StreamWriters.Add(sw);
                        foreach (StreamWriter stw in StreamWriters)
                        {
                            stw.WriteLine($"{userName} Has been connected");
                            stw.Flush();
                        }
                    }
                }

                try
                {
                    while (connected)
                    {
                        message = sr.ReadLine();

                        if (message != null)
                        {
                            if (message == "#exit" || message == "#lista")
                            {

                                if (message == "#exit")
                                {
                                    connected = false;
                                }
                                if (message == "#lista")
                                {
                                    string connectedUsers = "Connected users:";
                                    lock (l)
                                    {
                                        foreach (String user in usernames)
                                        {
                                            connectedUsers += "\n\t" + user;
                                        }
                                    }
                                    message = connectedUsers;
                                    sw.WriteLine(message);
                                    sw.Flush();
                                }
                            }
                            else
                            {
                                lock (l)
                                {
                                    foreach (StreamWriter stw in StreamWriters)
                                    {
                                        if (StreamWriters.Contains(stw))
                                        {
                                            stw.WriteLine($"{userName}@{ieCliente.Address}: {message}");
                                            stw.Flush();

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            connected = false;
                        }

                    }
                }
                catch (IOException ex)
                {
                    connected = false;
                }


                if (!connected)
                {
                    lock (l)
                    {
                        StreamWriters.Remove(sw);
                        usernames.Remove(userName);
                        cliente.Close();
                        foreach (StreamWriter stw in StreamWriters)
                        {
                            if (StreamWriters.Contains(stw))
                            {
                                stw.WriteLine($"{userName} has been loged out");
                                stw.Flush();
                            }
                        }
                    }
                }
            }


        }
    }

}
