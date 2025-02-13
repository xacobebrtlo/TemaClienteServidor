using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShiftServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Init();
        }
        string[] users;
        List<string> waitQueue;//Se añaden usuarios que estan en lista de espera
        private static readonly object l = new object();
        public void ReadName(string archivo)
        {
            using (StreamReader sr = new StreamReader(archivo))
            {
                string usuarios = sr.ReadToEnd();
                users = usuarios.Split(';');
                Array.ForEach(users, (u) => u.Trim());
            }

        }
        public int ReadPin(string archivo)
        {//leer Pin
            try
            {
                using (BinaryReader br = new BinaryReader(new FileStream(archivo, FileMode.Open)))
                {
                    int pin = br.ReadInt32();
                    if (pin >= 1000 && pin <= 9999)
                    {
                        return pin;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (ArgumentException)
            {
                return -1;
            }

        }

        int puerto = 31416;
        int nuevoPuerto = 1024;
        bool puertoOcupado = true;
        bool flag = true;
        Socket s;

        public void Init()//Falta que si llega la ultimo válido la app finaliza
        {
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                while (puertoOcupado)
                {
                    try
                    {
                        IPEndPoint ie = new IPEndPoint(IPAddress.Any, puerto);
                        s.Bind(ie);
                        puertoOcupado = false;
                        s.Listen(10);
                        Console.WriteLine($"Server listeninig at port {puerto}");
                        Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("userprofile"));

                        lock (l) //Lo meto en un lock porque esta accediendo a un archivo¿?
                        {
                            ReadName("usuarios.txt");
                        }
                    }
                    catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                    {
                        puerto = nuevoPuerto;
                        Console.WriteLine($"Port already in use, the new port is {puerto}");
                        if (puertoOcupado)
                        {
                            nuevoPuerto++;
                        }

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
                    catch (SocketException e)
                    {
                        flag = false;
                    }
                }

            }

        }

        public void clientThread(object socket)
        {
            string message;
            string username;
            bool connected = true;
            int adminPin;
            int pin;//pin devuelto por la funcion Readpin()
            bool isAdmin = false;
            Socket client = (Socket)socket;
            IPEndPoint ipcliente = (IPEndPoint)client.RemoteEndPoint;

            Console.WriteLine($"{ipcliente} has been connected");

            using (NetworkStream ns = new NetworkStream(client))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                sw.WriteLine("Welcome to the \"ShiftServer\"");
                sw.Flush();
                sw.WriteLine("Write your name to log in");
                sw.Flush();
                username = sr.ReadLine();

                if (username != null)
                {
                    lock (l)
                    {
                        foreach (string user in users)
                        {
                            if (username != user && username != "admin")//   
                            {
                                message = "Unknown user";
                                client.Close();
                                break;
                            }

                        }
                    }

                    switch (username)
                    {
                        case "admin":
                            //Voy por el segundo puto de la pagina 33
                            isAdmin = true;
                            sw.WriteLine("Write the pin");
                            sw.Flush();
                            adminPin = Convert.ToInt32(sr.ReadLine());
                            Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("userprofile"));

                            lock (l) //Meto lock porque abre el archivo
                            {
                                pin = ReadPin("pin.bin");
                                if (pin == -1)
                                {
                                    pin = 1234;
                                    Console.WriteLine($"El archivo no se pudo abrir, el nuevo pin es {pin}");
                                }
                            }
                            if (adminPin == pin)
                            {
                                sw.WriteLine("HOLA");
                                sw.Flush();
                            }
                            else
                            {
                                Console.WriteLine("Contraseña incorrecta");
                                client.Close();
                            }
                            goto default;

                        default:
                            sw.WriteLine("Write the command");
                            sw.Flush();
                            break;
                    }

                }
            }
        }
    }
}
