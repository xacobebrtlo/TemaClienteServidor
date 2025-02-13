using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente //Tituylo, icono, comprob   
{
    public partial class Form1 : Form
    {
        public string IP_SERVER = "127.0.0.1";
        public int puerto = 31416;
        public Form1()
        {
            InitializeComponent();
        }


        private void buttonClick(object sender, EventArgs e)
        {

            string msg;
            string userMsg = "";
            IPAddress iPAddress;

            if (IPAddress.TryParse(IP_SERVER, out iPAddress))
            {
                if (puerto < 0 || puerto > 65535)
                {
                    //comprueba rango del puerto y si esta fuera de rango pone un por defecto
                    lblResultado.Text = "Invalid port, the port has been changed to 31416";
                    puerto = 31416;
                }
                IPEndPoint ie = new IPEndPoint(iPAddress, puerto);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ie);
                }

                catch (SocketException ex)
                {
                    lblResultado.Text = $"Error connection: {ex.Message}";

                    return;
                }
                IPEndPoint ieServer = (IPEndPoint)server.RemoteEndPoint;
                lblResultado.Text = $"Server on Ip: {ieServer.Address} at port{ieServer.Port}";
                using (NetworkStream ns = new NetworkStream(server))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    try
                    {

                        msg = sr.ReadLine();
                        lblResultado.Text = msg;



                        if (sender == btnClose)
                        {
                            //Acabar btn close y el poder indicar la ip y el puerto con un formulario de dialogo
                            userMsg = btnClose.Text + " " + textBox1.Text;
                        }


                        Debug.WriteLine(lblResultado.Text);
                        sw.WriteLine(((Button)sender).Text);
                        sw.Flush();
                        msg = sr.ReadLine();
                        lblResultado.Text = msg;
                    }
                    catch (IOException exc)
                    {
                        Debug.Write("IOException");
                    }
                }
                server.Close();
            }
            else
            {
                lblResultado.Text = "Invalid IP";
            }
        }

        private void btnCambiarClick(object sender, EventArgs e)
        {

            Form formCambiar = new Form();
            formCambiar.Icon = new Icon("..\\..\\..\\rayquaza.ico");
            formCambiar.Text = "Actualizar puerto e ip";
            TextBox txtboxIP = new TextBox();
            TextBox txtboxPuerto = new TextBox();
            Button btnAceptar = new Button();
            Label lblIP = new Label();
            Label lblPuerto = new Label();

            lblIP.Text = "IP: ";
            lblIP.Location = new Point(0, 0);
            txtboxIP.Text = IP_SERVER;
            txtboxIP.Location = new Point(20, 0);


            lblPuerto.Text = "Puerto: ";
            lblPuerto.Location = new Point(0, 30);
            txtboxPuerto.Text = puerto.ToString();
            txtboxPuerto.Location = new Point(40, 30);

            formCambiar.Controls.Add(txtboxIP);
            formCambiar.Controls.Add(txtboxPuerto);
            formCambiar.Controls.Add(btnAceptar);
            formCambiar.Controls.Add(lblIP);
            formCambiar.Controls.Add(lblPuerto);
            btnAceptar.Location = new Point(0, 60);
            btnAceptar.DialogResult = DialogResult.OK;
            btnAceptar.Text = "Aceptar";
            if (formCambiar.ShowDialog() == DialogResult.OK)
            {
                IP_SERVER = txtboxIP.Text;
                UInt16.TryParse(txtboxPuerto.Text, out ushort puerto);

            }

        }
    }
}
