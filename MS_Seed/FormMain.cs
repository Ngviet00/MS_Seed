using MS_Seed.Common;
using MS_Seed.Extensions.IndustrialCommunication.PLC;
using MS_Seed.Extensions.Printer;
using MS_Seed.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MS_Seed
{
    public partial class FormMain : Form
    {
        //define list plc and list port mx component in control plc mishubishi
        //private List<ControlPLCMishubishi> _listPlc = new List<ControlPLCMishubishi>();
        //readonly int[] ActStationNumbers = { 1, 2, 3, 4 };

        public FormMain()
        {
            InitializeComponent();

            //PRINT
            //Task.Run(() =>
            //{
            //    ZebraZT411Printer.Instance.Print("test_printer");
            //});
            
            //INIT CONNECT TO PLC
            //Task.Run(() =>
            //{
            //    for (int i = 1; i <= 4; i++)
            //    {
            //        var plc = ControlPLCMishubishi.GetInstance(i, ActStationNumbers[i - 1]);

            //        if (!_listPlc.Contains(plc))
            //        {
            //            if (plc.ConnectPLC())
            //            {
            //                ConfigurePLCRegisters(plc, i);

            //                foreach (var register in plc.Registers)
            //                {
            //                    register.PropertyChanged += Register_PropertyChanged;
            //                }

            //                _listPlc.Add(plc);

            //                plc.StartReading();
            //            }
            //            else
            //            {
            //                Invoke((MethodInvoker)(() =>
            //                {
            //                    Global.ShowBoxError($"Error connect to PLC station number {ActStationNumbers[i - 1]} failed!");
            //                }));
            //            }
            //        }
            //    }
            //});
        }

        //WRITE
        private void button1_Click(object sender, EventArgs e)
        {
            ////STRING
            //ControlPLCMishubishi.WriteString(1, "D45550", "VIET_NV_1");

            ////FLOAT
            //float[] value = new float[1] { 4.1f };
            //ControlPLCMishubishi.WriteFloat(1, "WRITE_FLOAT", 1, ref value);

            ////DWORD
            //int[] res = new int[1] { 41 };
            //ControlPLCMishubishi.WriteDWord(1, "WRITE_DWORD", 1, ref res);

            ////WORD
            //short[] rs = new short[1] { 41 };
            //ControlPLCMishubishi.WriteWord(1, "WRITE_WORD", 1, ref rs);

            ////BIT
            //ControlPLCMishubishi.WriteBit(1, "WRITE_BIT_ALIVE", true);
        }

        //READ
        private void button2_Click(object sender, EventArgs e)
        {
            ////STRING
            //var a = ControlPLCMishubishi.ReadString(1, "READ_STRING", 25); //25 length
            //Console.WriteLine(a);

            ////FLOAT
            //float[] rsFloat = new float[1];
            //ControlPLCMishubishi.ReadFloat(1, "READ_FLOAT", 1, out rsFloat);
            //Console.WriteLine(rsFloat[0]);

            ////DWORD
            //int[] rsDword = new int[1];
            //ControlPLCMishubishi.ReadDWord(1, "READ_DWORD", 1, out rsDword);
            //Console.WriteLine(rsDword[0]);

            ////WORD
            //ControlPLCMishubishi.ReadWord(1, "READ_WORD", 1, out short[] rsWord);
            //Console.WriteLine(rsWord[0]);

            ////BIT
            //var b = ControlPLCMishubishi.ReadBit(1, "READ_BIT_ALIVE");
            //Console.WriteLine(b);
        }

        private void Register_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var obj = sender as Register;

            Console.WriteLine($"PLC {obj.PlcIndex}: {obj.Title} changed to {obj.CurrentValue}");

            //if (obj.Title == "READ_BIT_ALIVE")
            //{
            //    Console.WriteLine($"PLC {obj.PlcIndex}: {obj.Title} changed to {obj.CurrentValue}");
            //}
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //foreach (var plc in _listPlc.ToList())
            //{
            //    plc.DisconnectPLC();
            //}
        }

        private void ConfigurePLCRegisters(ControlPLCMishubishi plc, int plcIndex)
        {
            if (plcIndex == (int)Enums.PLC.PLC_1)
            {
                plc.ConfigureRegisters(
                    new Register { Title = "READ_BIT_ALIVE", Address = "M34000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.BIT, ReadAlway = true  }, //READ
                    new Register { Title = "WRITE_BIT_ALIVE", Address = "M34000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.BIT, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_WORD", Address = "D45000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.WORD, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_WORD", Address = "D45000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.WORD, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_DWORD", Address = "D45100", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.DWORD, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_DWORD", Address = "D45100", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.DWORD, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_STRING", Address = "D45200", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.STRING, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_STRING", Address = "D45200", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.STRING, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_FLOAT", Address = "D45300", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.FLOAT, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_FLOAT", Address = "D45300", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.FLOAT, ReadAlway = false } //WRITE
                );
            }
            else if (plcIndex == (int)Enums.PLC.PLC_2)
            {
                plc.ConfigureRegisters(
                    new Register { Title = "READ_BIT_ALIVE", Address = "M34000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.BIT, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_BIT_ALIVE", Address = "M34000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.BIT, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_WORD", Address = "D45000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.WORD, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_WORD", Address = "D45000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.WORD, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_DWORD", Address = "D45100", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.DWORD, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_DWORD", Address = "D45100", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.DWORD, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_STRING", Address = "D45200", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.STRING, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_STRING", Address = "D45200", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.STRING, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_FLOAT", Address = "D45300", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.FLOAT, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_FLOAT", Address = "D45300", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.FLOAT, ReadAlway = false } //WRITE
                );
            }
            else if (plcIndex == (int)Enums.PLC.PLC_3)
            {
                plc.ConfigureRegisters(
                    new Register { Title = "READ_BIT_ALIVE", Address = "M34000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.BIT, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_BIT_ALIVE", Address = "M34000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.BIT, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_WORD", Address = "D45000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.WORD, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_WORD", Address = "D45000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.WORD, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_DWORD", Address = "D45100", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.DWORD, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_DWORD", Address = "D45100", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.DWORD, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_STRING", Address = "D45200", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.STRING, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_STRING", Address = "D45200", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.STRING, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_FLOAT", Address = "D45300", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.FLOAT, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_FLOAT", Address = "D45300", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.FLOAT, ReadAlway = false } //WRITE
                );
            }
            else if (plcIndex == (int)Enums.PLC.PLC_4)
            {
                plc.ConfigureRegisters(
                    new Register { Title = "READ_BIT_ALIVE", Address = "M34000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.BIT, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_BIT_ALIVE", Address = "M34000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.BIT, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_WORD", Address = "D45000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.WORD, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_WORD", Address = "D45000", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.WORD, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_DWORD", Address = "D45100", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.DWORD, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_DWORD", Address = "D45100", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.DWORD, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_STRING", Address = "D45200", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.STRING, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_STRING", Address = "D45200", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.STRING, ReadAlway = false }, //WRITE

                    new Register { Title = "READ_FLOAT", Address = "D45300", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.READ, TypeDataPLC = TYPE_DATA_PLC.FLOAT, ReadAlway = true }, //READ
                    new Register { Title = "WRITE_FLOAT", Address = "D45300", PlcIndex = plcIndex, ReadOrWrite = READ_OR_WRITE.WRITE, TypeDataPLC = TYPE_DATA_PLC.FLOAT, ReadAlway = false } //WRITE
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
