using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RickMortyAlarm
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        public Timer timer;
        public Form1()
        {
            InitializeComponent();
            var timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000; //10 seconds
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000; //10 seconds
            timer.Start();
            HideCaret(txtSeconds.Handle);
        }


        void timer_Tick(object sender, EventArgs e)
        {

            int secondsLeft = Int32.Parse(txtSeconds.Text);
            if (secondsLeft > 0)
            {
                secondsLeft--;
                txtSeconds.Text = secondsLeft.ToString();
            }
            else if (secondsLeft == 0)
            {
                timer.Stop();

                //triggerArduino

            }
            if (secondsLeft == 1) {
                PlaySun();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PlaySun()
        {
            using (SerialPort serialPort1 = new SerialPort())
            {
                serialPort1.PortName = "COM4"; //set the port name you see in arduino IDE
                serialPort1.BaudRate = 9600;   //set the Baud you see in arduino IDE

                serialPort1.Open();

                System.Threading.Thread.Sleep(300);
                if (serialPort1.IsOpen)
                {
                    serialPort1.WriteLine("a");
                    serialPort1.Close();
                }
            }

        }
    }
}
