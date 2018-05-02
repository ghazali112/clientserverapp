using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
namespace oslab2
{
    public partial class Form2 : Form
    {
        public int port;
        private TcpClient client;
        private TcpListener Server;
        private NetworkStream mainStream;
        private readonly Thread Listening;
        private readonly Thread Getimage;


        public Form2( int Port )
        {
            port = Port;
            client = new TcpClient();
            Listening = new Thread(StartListening);
            Getimage = new Thread(recieveimage);


            InitializeComponent();
        }

        private void StartListening()
        {
           // while (true) {
                
                client = Server.AcceptTcpClient();

               // MessageBox.Show("Start");
                Getimage.Start();
            //}
            
        
        
        }

        private void StopListening() {

            Server.Stop();
            client = null;
            if (Listening.IsAlive) Listening.Abort();
            if (Getimage.IsAlive) Getimage.Abort();
        
        }

        private void recieveimage() {

            BinaryFormatter binformatter = new BinaryFormatter();
            while (client.Connected) {
                mainStream = client.GetStream();
                pictureBox1.Image = (Image) binformatter.Deserialize(mainStream);
            
            }
        }

        protected override void OnLoad(EventArgs e ) {
            base.OnLoad(e);
            Server = new TcpListener(IPAddress.Any,port);
            Server.Start();           
		   Listening.Start();
        
        }

        protected override void OnFormClosing(FormClosingEventArgs e ){
        base.OnFormClosing(e);
        StopListening();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
