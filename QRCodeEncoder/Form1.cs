using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace QRCodeEncoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitSerialPort();
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            serialPort1.Open();

        }


        private void InitSerialPort()
        {
            var ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
        }

       
        private void SendData(SerialPort port,string message)
        {
            try
            {
                port.Open();
                port.WriteLine(message);
            }
            finally
            {
                if (port != null)
                {
                    port.Dispose();
                    port.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var port = new SerialPort(comboBox1.SelectedItem.ToString());
                SendData(port, "Hello World");
               
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Invoke(new Action(() => {
                    var txt = serialPort1.ReadExisting();
                    label1.Text = txt;
                }));
               
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                serialPort1.Dispose();
                serialPort1.Close();
            }
           
        }
    }
}
