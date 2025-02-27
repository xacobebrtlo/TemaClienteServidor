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
    internal class Program    //     Comporbación rango al eliminar (no elimina).
    {// Revisión de introd. de pin, revisar chpin (admite no meter nada), solo 4 digitos.

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Init();
        }
        string[] users;
        List<string> waitQueue = new List<string>();//Se añaden usuarios que estan en lista de espera
        private static readonly object l = new object();
        public void ReadName(string archivo)
        {
            try
            {

                using (StreamReader sr = new StreamReader(archivo))
                {
                    string usuarios = sr.ReadToEnd();
                    users = usuarios.Split(';');
                    Array.ForEach(users, (u) => u.Trim());
                }
            }
            catch (ArgumentException) { }
            catch (IOException) { }


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
            catch (IOException)
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
                try
                {

                    //Añadir archivo de waitQeue a lista de waitQeue
                    Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("userprofile"));
                    lock (l)
                    {

                        if (File.Exists("WaitQeue.txt"))
                        {
                            using (StreamReader sr = new StreamReader("WaitQeue.txt"))
                            {
                                string usuarios = sr.ReadToEnd();
                                if (usuarios != null)
                                {
                                    waitQueue.Add(usuarios);
                                }
                            }
                        }
                    }
                }
                catch (IOException) { }
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
            string[] realCommands = { };
            string command;
            string message;
            string username;

            bool bandera = true;
            int adminPin = 0;
            int pin;//pin devuelto por la funcion Readpin()
            bool isAdmin = true;
            Socket client = (Socket)socket;
            IPEndPoint ipcliente = (IPEndPoint)client.RemoteEndPoint;

            Console.WriteLine($"{ipcliente} has been connected");

            try
            {

                using (NetworkStream ns = new NetworkStream(client))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    sw.WriteLine("Welcome to the \"ShiftServer\"");
                    sw.Flush();
                    sw.WriteLine("Write your name to log in");
                    sw.Flush();
                    username = sr.ReadLine();
                    while (isAdmin && bandera)
                    {

                        if (username != null)
                        {
                            // lock (l)
                            {
                                if (!users.Contains(username) && username != "admin")
                                {
                                    message = "Unknown user";
                                    sw.WriteLine(message);
                                    sw.Flush();
                                    bandera = false;

                                }

                            }

                            if (bandera)
                            {

                                switch (username)
                                {
                                    case "admin":
                                        isAdmin = true;
                                        if (adminPin == 0)//hacer tryparse
                                        {
                                            sw.WriteLine("Write the pin");
                                            sw.Flush();
                                            int.TryParse(sr.ReadLine(), out adminPin);



                                        }
                                        Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("userprofile"));

                                        lock (l) //Meto lock porque abre el archivo
                                        {
                                            pin = ReadPin("pin.bin");
                                            if (pin == -1)
                                            {
                                                pin = 1234;
                                                Console.WriteLine($"El archivo no se pudo abrir, el nuevo pin es {pin}");
                                            }
                                            if (adminPin == pin)
                                            {
                                                isAdmin = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Contraseña incorrecta");
                                                client.Close();
                                            }
                                        }
                                        goto default;

                                    default:
                                        //Comprobacion para ver si es admin,
                                        //si no es admin se pone a false y el bucle solo da una vuelta

                                        if (username != "admin")
                                        {
                                            isAdmin = false;
                                        }

                                        try
                                        {
                                            sw.WriteLine("Write the command");
                                            sw.Flush();
                                            command = sr.ReadLine();

                                            if (command != null)
                                            {
                                                realCommands = command.Split(' ');
                                            }
                                        }
                                        catch (IOException)
                                        {
                                            bandera = false;
                                        }
                                        if (realCommands.Length >= 1)
                                        {

                                            switch (realCommands[0])
                                            {
                                                case "list":
                                                    lock (l)
                                                    {
                                                        if (waitQueue.Count > 0)
                                                        {
                                                            foreach (string user in waitQueue)
                                                            {
                                                                sw.Write(user);
                                                                sw.Flush();
                                                            }
                                                        }
                                                    }
                                                    if (username != "admin")
                                                    {
                                                        sr.ReadLine();
                                                    }
                                                    break;
                                                case "add":
                                                    lock (l)
                                                    {
                                                        username = username + ";" + System.DateTime.Now + "\t";
                                                        if (waitQueue.Count <= 0)
                                                        {
                                                            waitQueue.Add(username);
                                                            sw.Write("Ok");
                                                            sw.Flush();
                                                        }
                                                        else
                                                        {
                                                            //  lock (l)
                                                            {

                                                                string[] nombre = username.Split(';');
                                                                if (!waitQueue.Contains(nombre[0]))
                                                                {
                                                                    waitQueue.Add(username);
                                                                    sw.Write("Ok");
                                                                    sw.Flush();
                                                                }
                                                                else
                                                                {
                                                                    sw.WriteLine("The user is already in the queue");
                                                                    sw.Flush();
                                                                }

                                                            }
                                                        }
                                                    }
                                                    sr.ReadLine();
                                                    break;
                                                case "del" when isAdmin:
                                                    lock (l)
                                                    {
                                                        if (realCommands.Length > 1)
                                                        {
                                                            if (waitQueue.Count <= Convert.ToInt32(realCommands[1]))
                                                            {
                                                                waitQueue.RemoveAt(Convert.ToInt32(realCommands[1]));

                                                            }
                                                            else
                                                            {
                                                                sw.WriteLine("delete error");
                                                                sw.Flush();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            sw.WriteLine("delete error");
                                                            sw.Flush();
                                                        }
                                                    }

                                                    break;
                                                case "chpin" when isAdmin:

                                                    try
                                                    {
                                                        lock (l)
                                                        {
                                                            using (BinaryWriter bw = new BinaryWriter(new FileStream("pin.bin", FileMode.Create)))
                                                            {
                                                                int nuevoPin;
                                                                int.TryParse(realCommands[1], out nuevoPin);
                                                                if (nuevoPin >= 1000 && nuevoPin <= 9999)
                                                                {
                                                                    bw.Write(nuevoPin);
                                                                    bw.Flush();
                                                                    sw.WriteLine("Pin guardado correctamente");
                                                                    sw.Flush();
                                                                }
                                                                else
                                                                {
                                                                    sw.WriteLine("No se ha podido guardar el pin");
                                                                    sw.Flush();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch (ArgumentException) { }

                                                    break;
                                                case "exit" when isAdmin:
                                                    isAdmin = false;

                                                    break;
                                                case "shutdown" when isAdmin:

                                                    lock (l)
                                                    {

                                                        using (StreamWriter stw = new StreamWriter("WaitQeue.txt"))
                                                        {
                                                            foreach (string user in waitQueue)
                                                            {

                                                                stw.Write(user);
                                                            }
                                                        }
                                                    }
                                                    isAdmin = false;
                                                    bandera = false;
                                                    s.Close();


                                                    break;
                                            }
                                        }
                                        break;
                                }

                            }
                        }
                    }

                }
            }
            catch (IOException ex) { }
            client.Close();
            //TODO shutdown

        }
    }
}
