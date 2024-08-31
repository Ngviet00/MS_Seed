using MS_Seed.IndustrialCommunication.Ethernet;
using MS_Seed.IndustrialCommunication.PLC;
using MS_Seed.SQL;
using System;
using System.ComponentModel;
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

            //ControlPLCMishubishi.Instance.ConnectPLC1();

            //ControlPLCMishubishi.Instance.ConnectPLC2();

            ControlPLCMishubishi.Instance.ConnectToPLC(1);
            ControlPLCMishubishi.Instance.ConnectToPLC(2);

            ControlPLCMishubishi.Instance.PropertyChanged += Plc_PropertyChanged;
        }

        private void Plc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var obj = ((ControlPLCMishubishi)sender);

            if (obj != null)
            {
                if (obj.Title == "ALIVE")
                {
                    Console.WriteLine($"plc-{obj.IndexPLC}-title-{obj.Title}-current_value-{obj.CurrentValue}");
                }
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
