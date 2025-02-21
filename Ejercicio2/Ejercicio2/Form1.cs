using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio2
{
    public partial class Form1 : Form
    {
        private static readonly object l = new object();
        public Form1()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("userprofile"));
            if (File.Exists("Datos.txt"))
            {
                FileInfo f = new FileInfo("Datos.txt");

                if (f != null)
                {
                    lock (l)
                    {
                        using (StreamReader sr = new StreamReader("Datos.txt"))
                        {
                            string[] datos = sr.ReadToEnd().Split(';');
                            txbIp.Text = datos[0];
                            txbPuerto.Text = datos[1];
                            txbUser.Text = datos[2];
                        }
                    }

                }
            }


        }
        public string IP_SERVER = "127.0.0.1";
        public int puerto = 31416;
        private void btnClick(object sender, EventArgs e)
        {
            IP_SERVER = txbIp.Text;
            puerto = Convert.ToInt16(txbPuerto.Text);
            IPAddress iPAddress;

            if (IPAddress.TryParse(IP_SERVER, out iPAddress))
            {
                if ()
                {

                }
            }

        }
    }
}
