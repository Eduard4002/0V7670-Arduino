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
using System.IO.Ports;

namespace CamReceiver
{
    public partial class Form1 : Form
    {
        StreamWriter sw;
        Bitmap bmp;
        byte[] startBuffer = new byte[1];
        byte[] endBuffer = new byte[1];
        private byte[] imageData;
        private int imageIndex = 0;

        public Form1()
        {
            InitializeComponent();
            sw = new StreamWriter("C:/TEST/CAM.txt");
            //bmp = new Bitmap(640, 480);
            //bmpData = bmp.LockBits(new Rectangle(0, 0, 640, 480), System.Drawing.Imaging.ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            startBuffer[0] = 1;
            endBuffer[0] = 0;

            imageData = new byte[640 * 480];
            CamPort.Open();
            CamTimer.Enabled = true;
            CamPort.DataReceived += SerialPort_DataReceived;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        unsafe private void CamTimer_Tick(object sender, EventArgs e)
        {                     
    
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
        
            Console.WriteLine(CamPort.BytesToRead);
            int bytesToRead = CamPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];
            CamPort.Read(buffer, 0, bytesToRead);

            for (int i = 0; i < bytesToRead; i++)
            {
            
                imageData[imageIndex] = buffer[i];
                imageIndex++;


                if (imageIndex == imageData.Length)
                {
                    CamPort.Write(endBuffer, 0, endBuffer.Length);

                    // We have received a complete image
                    Bitmap bitmap = new Bitmap(640, 480, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    System.Drawing.Imaging.ColorPalette palette = bitmap.Palette;
                    for (int j = 0; j < 256; j++)
                    {
                        palette.Entries[j] = Color.FromArgb(j, j, j);
                    }
                    bitmap.Palette = palette;

                    BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, 640, 480), ImageLockMode.WriteOnly, bitmap.PixelFormat);
                    System.Runtime.InteropServices.Marshal.Copy(imageData, 0, bitmapData.Scan0, imageData.Length);
                    bitmap.UnlockBits(bitmapData);
                    imageIndex = 0;
                    //CamPort.DiscardInBuffer();
                    MainImage.Invoke(new Action(() =>
                    {
                        MainImage.Image = bitmap;
                        MainImage.Refresh();
                    }));
                }
            }
        }
        void requestImage(int width, int height)
        {
            //Send "0" to camera
            bmp = new Bitmap(width, height);

            //CamPort.Write(buffer, 0, buffer.Length);

            int data;

            //Wait for a "0" from camera, means we are ready to take new image
            do
            {
                data = CamPort.ReadByte();
            } while (data != 0);

            //received a 0, we can start reading from camera
            CamData.Text = "Creating image...";
            for(int y = height-1;y >= 0; y--)
            {
                int _width = width;
                for(int x = _width-1;x >= 0; x--)
                {
                    data = CamPort.ReadByte();
                    if (data == -1)
                    {
                        CamData.Text = "DATA STOPPED";
                        break;
                    }

                    bmp.SetPixel(x, y, Color.FromArgb(data, data, data));

                }
                bytesRead += height;
                //CamData.Text = bytesRead.ToString();
            }

            CamData.Text = "Image done";
            bytesRead = 0;

            MainImage.Image = bmp;
            MainImage.Invalidate();

            sw.Flush();
        }
       


        private void MainImage_Click(object sender, EventArgs e)
        {

        }
    }
}
