using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Runtime.Serialization.Formatters.Binary;
namespace oslab
{
    public partial class Form1 : Form
    {
        private readonly TcpClient client =new TcpClient();
        private NetworkStream mainstream;
        private int portnum;
      
        private static Image GrabDesktop() {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width,bounds.Height,PixelFormat.Format32bppArgb);
            Graphics graphic =Graphics.FromImage(screenshot);
            graphic.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenshot;
        
        
        }

        private void SendImage() {
            BinaryFormatter binformatter = new BinaryFormatter();
            mainstream = client.GetStream();
            binformatter.Serialize(mainstream,GrabDesktop());

        
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            portnum = int.Parse(textBox2.Text);
            try
            {
                client.Connect(textBox1.Text,portnum);
                MessageBox.Show("connected");


            }
            catch (Exception es)
            {

                MessageBox.Show(es.Message.ToString());

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Text.StartsWith("connect")|| button2.Text.StartsWith("share")) 
            {
                timer1.Start();
                button2.Text = "stop sharing";

            
            }
            else {
            
            timer1.Stop();
            button2.Text="share my screen";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendImage();
        }
    }
}
