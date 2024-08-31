using MS_Seed.IndustrialCommunication.Ethernet;
using MS_Seed.IndustrialCommunication.PLC;
using MS_Seed.SQL;
using System;
using System.Windows.Forms;

namespace MS_Seed
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            //ControlSerialPort controlSerialPort = new ControlSerialPort();
            //controlSerialPort.Open();

            //SQLite.Instance.Connect();

            ControlPLCMishubishi.Instance.ConnectPLC1();

            //if (ControlPLCMishubishi.Instance.ConnectPLC1())
            //{
            //    Console.WriteLine("111");
            //}

            if (ControlPLCMishubishi.Instance.ConnectPLC2())
            {
                Console.WriteLine("111");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ControlPLCMishubishi.Instance.WriteBit(2, "M34000", true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ControlPLCMishubishi.Instance.WriteBit(2, "M34000", false);
        }
    }
}
