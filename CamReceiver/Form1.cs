using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
namespace CamReceiver
{
    public partial class Form1 : Form
    {
        StreamWriter sw;
        Bitmap bmp;
        BitmapData bmpData;
        object syncRoot = new object();
        byte[] buffer = new byte[1];

        

        public Form1()
        {
            InitializeComponent();
            sw = new StreamWriter("C:/TEST/CAM.txt");
            bmp = new Bitmap(640, 480);
            //bmpData = bmp.LockBits(new Rectangle(0, 0, 640, 480), System.Drawing.Imaging.ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            buffer[0] = 0;


            CamPort.Open();
            CamTimer.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        int bytesRead = 0;

        unsafe private void CamTimer_Tick(object sender, EventArgs e)
        {                     
    
        }
        void requestImage(int width, int height)
        {
            //Send "0" to camera
            CamPort.Write(buffer, 0, buffer.Length);
            int data;

            //Wait for a "0" from camera, means we are ready to take new image
            do
            {
                data = CamPort.ReadByte();
            } while (data != 0);

            //received a 0, we can start reading from camera
            CamData.Text = "Creating image...";

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    data = CamPort.ReadByte();
                    if (data == -1)
                    {
                        CamData.Text = "DATA STOPPED";
                        break;
                    }

                    bmp.SetPixel(x, y, Color.FromArgb(data, data, data));


                    //sw.Write(data + " ");


                }
                bytesRead += height;
                CamData.Text = bytesRead.ToString();

            }
            CamData.Text = "Image done";
            bytesRead = 0;

            MainImage.Image = bmp;
            MainImage.Invalidate();

            sw.Flush();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            requestImage(640 / 2, 480);

        }

        private void MainImage_Click(object sender, EventArgs e)
        {

        }
    }
}
