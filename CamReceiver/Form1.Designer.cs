
namespace CamReceiver
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CamPort = new System.IO.Ports.SerialPort(this.components);
            this.CamData = new System.Windows.Forms.TextBox();
            this.CamTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.MainImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainImage)).BeginInit();
            this.SuspendLayout();
            // 
            // CamPort
            // 
            this.CamPort.BaudRate = 1000000;
            this.CamPort.PortName = "COM4";
            // 
            // CamData
            // 
            this.CamData.Location = new System.Drawing.Point(124, -2);
            this.CamData.Multiline = true;
            this.CamData.Name = "CamData";
            this.CamData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CamData.Size = new System.Drawing.Size(673, 129);
            this.CamData.TabIndex = 0;
            // 
            // CamTimer
            // 
            this.CamTimer.Interval = 10;
            this.CamTimer.Tick += new System.EventHandler(this.CamTimer_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 49);
            this.button1.TabIndex = 1;
            this.button1.Text = "Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainImage
            // 
            this.MainImage.Location = new System.Drawing.Point(252, 135);
            this.MainImage.Name = "MainImage";
            this.MainImage.Size = new System.Drawing.Size(960, 738);
            this.MainImage.TabIndex = 2;
            this.MainImage.TabStop = false;
            this.MainImage.Click += new System.EventHandler(this.MainImage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 882);
            this.Controls.Add(this.MainImage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CamData);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort CamPort;
        private System.Windows.Forms.TextBox CamData;
        private System.Windows.Forms.Timer CamTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox MainImage;
    }
}

