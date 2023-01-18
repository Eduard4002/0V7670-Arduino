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
    
            if (CamPort.IsOpen)
            {

                int data = CamPort.ReadByte();

                //Waiting for a zero since we are sending "0" before starting to send rest of data
                while(data != 0)
                {
                    for (int y = 0; y < 480; y++)
                    {
                        for (int x = 0; x < 640; x++)
                        {
                            data = CamPort.ReadByte();

                            bmp.SetPixel(x, y, Color.FromArgb(data, data, data));

                            sw.Write(data + " ");


                        }
                    }
                    /*int stride = bmpData.Stride < 0 ? -bmpData.Stride : bmpData.Stride;
                    unsafe
                    {
                        byte* row = (byte*)bmpData.Scan0;
                        for (int f = 0; f < 480; f++)
                        {
                            for (int w = 0; w < 640; w++)
                            {
                                data = CamPort.ReadByte();
                                row[1] = (byte)Math.Min(255, Math.Max(0, data));
                                //dataCount++;
                            }
                            row += stride;
                        }
                    }
                    bmp.UnlockBits(bmpData);*/

                    sw.WriteLine("");
                    sw.WriteLine("");
                    CamData.Text = "Image done";

                    MainImage.Image = bmp;
                    MainImage.Invalidate();

                    sw.Flush();
                }

                

            }
            else
            {
                //CamData.AppendText(CamPort.BytesToRead.ToString());
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {


            if (CamPort.IsOpen)
            {

                byte data = 0;
             

                for (int y = 0; y < 480; y++)
                {
                    for (int x = 0; x < 640; x++)
                    {
                        data = (byte)CamPort.ReadByte();

                        bmp.SetPixel(x, y, Color.FromArgb(data, data, data));

                        sw.Write(data + " ");

                        //CamData.AppendText(data.ToString());

                    }
                }
                MainImage.Image = bmp;
                MainImage.Invalidate();

                sw.Flush();

            }
            else
            {
                //CamData.AppendText(CamPort.BytesToRead.ToString());
            }
            /*
            int data = CamPort.ReadByte();

            byte[] dataBuffer = new byte[1024];
            int bytesRead = CamPort.Read(dataBuffer, 0, dataBuffer.Length);
            //CamData.AppendText(bytesRead.ToString());

            if (bytesRead > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    try
                    {
                        bmp.SetPixel(xCount, YCount, Color.FromArgb(dataBuffer[i], dataBuffer[i], dataBuffer[i]));
                        xCount = (i / 480) * count;

                        YCount = (i % 480);
                    }
                    catch (Exception exc)
                    {
                        //CamData.AppendText(exc.Message);
                    }

                }
                CamData.AppendText(count.ToString());
                count++;


            }

            MainImage.Image = bmp;
            MainImage.Invalidate();*/
        }

        private void MainImage_Click(object sender, EventArgs e)
        {

        }
    }
}
