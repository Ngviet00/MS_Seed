using MS_Seed.Extensions.IndustrialCommunication.PLC;
using MS_Seed.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MS_Seed
{
    public partial class FormMain : Form
    {
        //define list plc and list port mx component in control plc mishubishi
        private List<ControlPLCMishubishi> _listPlc = new List<ControlPLCMishubishi>();
        readonly int[] ActStationNumbers = { 1, 2, 3, 4 };

        public FormMain()
        {
            InitializeComponent();

            for (int i = 1; i <= 1; i++)
            {
                var plc = ControlPLCMishubishi.GetInstance(i, ActStationNumbers[i - 1]);

                if (!_listPlc.Contains(plc))
                {
                    plc.ConnectPLC();

                    ConfigurePLCRegisters(plc, i);

                    foreach (var register in plc.Registers)
                    {
                        register.PropertyChanged += Register_PropertyChanged;
                    }

                    _listPlc.Add(plc);

                    plc.StartReading();
                }
            }
        }

        //WRITE
        private void button1_Click(object sender, EventArgs e)
        {
            //STRING
            //ControlPLCMishubishi.WriteString(1, "D45000", "ok la1");

            //FLOAT
            //float[] value = new float[1] { 0.4354f };
            //ControlPLCMishubishi.WriteFloat(1, "D45000", 1, ref value);

            //DWORD
            //int[] res = new int[1] { 20 };
            //ControlPLCMishubishi.WriteDWord(1, "D45000", 1, ref res);

            //WORD
            //short[] rs = new short[1] { 1 };
            //ControlPLCMishubishi.WriteWord(1, "D45000", 1, ref rs);

            //BIT
            //ControlPLCMishubishi.WriteBit(1, "M34000", true);
        }

        //READ
        private void button1_Click_1(object sender, EventArgs e)
        {
            //STRING
            //var a = ControlPLCMishubishi.ReadString(1, "D45000", 10); //10 bit
            //Console.WriteLine(a);

            //FLOAT
            //float[] res = new float[1];
            //ControlPLCMishubishi.ReadFloat(1, "D45000", 1, out res);
            //Console.WriteLine(res[0]);

            //DWORD
            //int[] res = new int[1];
            //ControlPLCMishubishi.ReadDWord(1, "D45000", 1, out res);
            //Console.WriteLine(res[0]);

            //WORD
            //ControlPLCMishubishi.ReadWord(1, "D45000", 1, out short[] rs);
            //Console.WriteLine(rs[0]);

            //BIT
            //ControlPLCMishubishi.WriteBit(1, "M34000", false);
        }

        private void Register_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var obj = sender as Register;

            if (obj.Title == "ALIVE")
            {
                Console.WriteLine($"PLC {obj.PlcIndex}: {obj.Title} changed to {obj.CurrentValue}");
            }
            else if (obj.Title == "TEST_ALIVE")
            {
                Console.WriteLine($"PLC {obj.PlcIndex}: {obj.Title} changed to {obj.CurrentValue}");
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var plc in _listPlc)
            {
                plc.DisconnectPLC();
            }
        }

        private void ConfigurePLCRegisters(ControlPLCMishubishi plc, int plcIndex)
        {
            if (plcIndex == 1)
            {
                plc.ConfigureRegisters(
                    new Register { Title = "ALIVE", Address = "M34000", PlcIndex = plcIndex },
                    new Register { Title = "TEST_ALIVE", Address = "M34010", PlcIndex = plcIndex }
                );
            }
            else if (plcIndex == 2)
            {
                plc.ConfigureRegisters(
                    new Register { Title = "ALIVE", Address = "M34000", PlcIndex = plcIndex },
                    new Register { Title = "TEST_ALIVE", Address = "M34010", PlcIndex = plcIndex }
                );
            }
            else if (plcIndex == 3)
            {
                plc.ConfigureRegisters(
                    new Register { Title = "ALIVE", Address = "M34000", PlcIndex = plcIndex },
                    new Register { Title = "TEST_ALIVE", Address = "M34010", PlcIndex = plcIndex }
                );
            }
        }

        private void pictureBtnSetting_Click(object sender, EventArgs e)
        {
            FormSetting fs = new FormSetting();
            fs.ShowDialog();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dddd, MMM dd, yyyy | HH:mm:ss");
        }

        private void picturePLC_Click(object sender, EventArgs e)
        {
            FormTestPLC formPLC = new FormTestPLC();
            formPLC.ShowDialog();
        }
    }
}
