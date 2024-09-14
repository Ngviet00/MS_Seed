using MS_Seed.Common;
using MS_Seed.Extensions.IndustrialCommunication.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MS_Seed.Forms
{
    public partial class FormTestPLC : Form
    {
        //define list plc and list port mx component in control plc mishubishi
        private List<ControlPLCMishubishi> _listPlc = new List<ControlPLCMishubishi>();
        readonly int[] ActStationNumbers = { 1, 2, 3, 4 };

        public FormTestPLC()
        {
            InitializeComponent();

            Task.Run(() =>
            {
                for (int i = 1; i <= 4; i++)
                {
                    var plc = ControlPLCMishubishi.GetInstance(i, ActStationNumbers[i - 1]);

                    if (!_listPlc.Contains(plc))
                    {
                        if (plc.ConnectPLC())
                        {
                            ConfigurePLCRegisters(plc, i);

                            foreach (var register in plc.Registers)
                            {
                                register.PropertyChanged += Register_PropertyChanged;
                            }

                            _listPlc.Add(plc);

                            plc.StartReading();

                            SetValueToTextBox($"Plc {i} connected!");
                        }
                        else
                        {
                            Invoke((MethodInvoker)(() =>
                            {
                                SetValueToTextBox($"Plc {i} connect failed!");
                                Global.ShowBoxError($"Error connect to PLC station number {ActStationNumbers[i - 1]} failed!");
                            }));
                        }
                    }
                }
            });
        }

        private void Register_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var obj = sender as Register;

            SetValueToTextBox($"PLC {obj.PlcIndex}: {obj.Title} changed to {obj.CurrentValue}");
        }

        private void ConfigurePLCRegisters(ControlPLCMishubishi plc, int plcIndex)
        {
            if (plcIndex == (int)Enums.PLC.PLC_1)
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

        private void FormTestPLC_FormClosing(object sender, FormClosingEventArgs e)
        {
            Task.Run(() =>
            {
                foreach (var plc in _listPlc.ToList())
                {
                    plc.DisconnectPLC();
                    plc.Registers.Clear();
                }

                _listPlc.ToList().Clear();
            });
        }

        private void BtnWriteBit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWriteRegister1.Text) || string.IsNullOrWhiteSpace(txtWriteValue1.Text) || string.IsNullOrWhiteSpace(txtWritePlc1.Text))
            {
                Global.ShowBoxError("Can not empty register, value or Plc");
                return;
            }

            ControlPLCMishubishi.WriteBit(int.Parse(txtWritePlc1.Text), txtWriteRegister1.Text, int.Parse(txtWriteValue1.Text) == 1 ? true : false);
        }

        private void BtnWriteWord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWriteRegister2.Text) || string.IsNullOrWhiteSpace(txtWriteValue2.Text) || string.IsNullOrWhiteSpace(txtWritePlc2.Text))
            {
                Global.ShowBoxError("Can not empty register, value or Plc");
                return;
            }

            short[] rs = new short[1] { short.Parse(txtWriteValue2.Text) };
            ControlPLCMishubishi.WriteWord(int.Parse(txtWritePlc2.Text), txtWriteRegister2.Text, 1, ref rs);
        }

        private void BtnWriteDWord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWriteRegister3.Text) || string.IsNullOrWhiteSpace(txtWriteValue3.Text) || string.IsNullOrWhiteSpace(txtWritePlc3.Text))
            {
                Global.ShowBoxError("Can not empty register, value or Plc");
                return;
            }

            int[] res = new int[1] { short.Parse(txtWriteValue3.Text) };
            ControlPLCMishubishi.WriteDWord(int.Parse(txtWritePlc3.Text), txtWriteRegister3.Text, 1, ref res);
        }

        private void BtnWriteString_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWriteRegister4.Text) || string.IsNullOrWhiteSpace(txtWriteValue4.Text) || string.IsNullOrWhiteSpace(txtWritePlc4.Text))
            {
                Global.ShowBoxError("Can not empty register, value or Plc");
                return;
            }

            ControlPLCMishubishi.WriteString(int.Parse(txtWritePlc4.Text), txtWriteRegister4.Text , txtWriteValue4.Text);
        }

        private void BtnWriteFloat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWriteRegister5.Text) || string.IsNullOrWhiteSpace(txtWriteValue5.Text) || string.IsNullOrWhiteSpace(txtWritePlc5.Text))
            {
                Global.ShowBoxError("Can not empty register, value or Plc");
                return;
            }

            float[] value = new float[1] { float.Parse(txtWriteValue5.Text) };
            ControlPLCMishubishi.WriteFloat(int.Parse(txtWritePlc5.Text), txtWriteRegister5.Text, 1, ref value);
        }

        private void BtnReadBit_Click(object sender, EventArgs e)
        {
            var rs = ControlPLCMishubishi.ReadBit(1, "READ_BIT_ALIVE");
            SetValueToTextBox($"Read BIT to {rs}");
        }

        private void BtnReadWord_Click(object sender, EventArgs e)
        {
            ControlPLCMishubishi.ReadWord(1, "READ_WORD", 1, out short[] rsWord);
            SetValueToTextBox($"Read WORD to {rsWord[0]}");
        }

        private void BtnReadDWord_Click(object sender, EventArgs e)
        {
            ControlPLCMishubishi.ReadDWord(1, "READ_DWORD", 1, out int[] rsDword);
            SetValueToTextBox($"Read DWORD to {rsDword[0]}");
        }

        private void BtnReadString_Click(object sender, EventArgs e)
        {
            var rs = ControlPLCMishubishi.ReadString(1, "READ_STRING", 25); //25 length
            SetValueToTextBox($"Read STRING to {rs}");
        }

        private void BtnReadFloat_Click(object sender, EventArgs e)
        {
            ControlPLCMishubishi.ReadFloat(1, "READ_FLOAT", 1, out float[] rsFloat);
            SetValueToTextBox($"Read FLOAT to {rsFloat[0]}");
        }

        public void SetValueToTextBox(string message)
        {
            if (txtResult.InvokeRequired)
            {
                txtResult.Invoke(new MethodInvoker(() =>
                {
                    txtResult.AppendText(message + Environment.NewLine);
                }));
            }
            else
            {
                txtResult.AppendText(message + Environment.NewLine);
            }
        }
    }
}
