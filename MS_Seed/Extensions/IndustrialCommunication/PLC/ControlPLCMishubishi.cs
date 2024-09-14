using ActUtlTypeLib;
using MS_Seed.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS_Seed.Extensions.IndustrialCommunication.PLC
{
    public class ControlPLCMishubishi
    {
        private static readonly Dictionary<int, ControlPLCMishubishi> _instances = new Dictionary<int, ControlPLCMishubishi>();
        private static readonly object _lock = new object();

        public ActUtlType plc;
        private bool isReading;
        private int indexPLC;
        private int _stationNumber;

        public List<Register> Registers { get; set; }
        private static List<Register> TempRegisters { get; set; } = new List<Register>();

        private ControlPLCMishubishi(int plcIndex, int plcStation)
        {
            plc = new ActUtlType();
            plc.ActLogicalStationNumber = plcStation;
            indexPLC = plcIndex;
            isReading = false;
            Registers = new List<Register>();
            _stationNumber = plcStation;
        }

        public static ControlPLCMishubishi GetInstance(int indexPLC, int plcStation)
        {
            lock (_lock)
            {
                if (!_instances.ContainsKey(plcStation))
                {
                    _instances[indexPLC] = new ControlPLCMishubishi(indexPLC, plcStation);
                }

                return _instances[indexPLC];
            }
        }

        public bool ConnectPLC()
        {
            try
            {
                return plc.Open() == 0;
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Error can not connect to PLC {_stationNumber}, err: {ex.Message}!");
                return false;
            }
        }

        public void DisconnectPLC()
        {
            StopReading();
            plc.Disconnect();
            plc.Close();
        }

        public void StartReading()
        {
            isReading = true;
            Task.Run(() => ReadAlwayRegister());
        }

        public void StopReading()
        {
            isReading = false;
        }

        public void ReadAlwayRegister()
        {
            TempRegisters = Registers;

            Registers = Registers.Where(e => e.ReadAlway == true && e.ReadOrWrite == READ_OR_WRITE.READ).ToList();

            while (isReading)
            {
                foreach (var register in Registers.ToList())
                {
                    if (register.TypeDataPLC == TYPE_DATA_PLC.BIT)
                    {
                        plc.GetDevice2(register.Address, out short readValue);
                        register.CurrentValue = readValue;
                    }
                    else if (register.TypeDataPLC == TYPE_DATA_PLC.WORD)
                    {
                        short[] data = new short[1];
                        plc.ReadDeviceBlock2(register.Address, 1, out data[0]);
                        register.CurrentValue = data[0];
                    }
                    else if (register.TypeDataPLC == TYPE_DATA_PLC.DWORD)
                    {
                        int length = 1;
                        int[] data = new int[length];
                        var res = new short[length * 2];

                        if (plc.ReadDeviceBlock2(register.Address, length * 2, out res[0]) == 0)
                        {
                            for (int i = 0; i < length; i++)
                            {
                                var r = ConvertShortArr2Int(res[i * 2 + 1], res[i * 2]);
                                data[i] = r;
                            }

                            register.CurrentValue = data[0];
                        }
                    }
                    else if (register.TypeDataPLC == TYPE_DATA_PLC.STRING)
                    {
                        ReadWord(register.PlcIndex, register.Address, 25, out short[] rs);
                        register.CurrentValue = ConvertShortArrToString(rs);
                    }
                    else if (register.TypeDataPLC == TYPE_DATA_PLC.FLOAT)
                    {
                        float[] rs = new float[1];
                        var res = new short[2];
                        plc.ReadDeviceBlock2(register.Address, 2, out res[0]);

                        for (int i = 0; i < 1; i++)
                        {
                            var r = ConvertDWordIntToFloat(ConvertShortArr2Int(res[i * 2 + 1], res[i * 2]));
                            rs[i] = r;
                        }

                        register.CurrentValue = rs[0];
                    }
                }

                Task.Delay(100).Wait();
            }
        }

        public void ConfigureRegisters(params Register[] registers)
        {
            Registers.AddRange(registers);
        }

        public static bool WriteBit(int plcIndex, string registerOrTitle, bool value)
        {
            try
            {
                var item = TempRegisters.Find(e => (e.Title == registerOrTitle.Trim().ToUpper() || e.Address == registerOrTitle.Trim().ToUpper()) && e.ReadOrWrite == READ_OR_WRITE.WRITE);
                
                if (_instances[plcIndex].plc.SetDevice2(item?.Address ?? registerOrTitle, value ? (short)1 : (short)0) == 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not write bit, error: {ex.Message}");
                Files.WriteLog($"Error can not write bit, error: {ex.Message}");
                return false;
            }
        }

        public static bool ReadBit(int plcIndex, string registerOrTitle)
        {
            try
            {
                var item = TempRegisters.Find(e => (e.Title == registerOrTitle.Trim().ToUpper() || e.Address == registerOrTitle.Trim().ToUpper()) && e.ReadOrWrite == READ_OR_WRITE.READ);

                if (_instances[plcIndex].plc.GetDevice2(item?.Address ?? registerOrTitle, out short value) == 0)
                {
                    return value == 1;
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not read bit, error: {ex.Message}");
                Files.WriteLog($"Error can not read bit, error: {ex.Message}");
                return false;
            }
        }

        public static bool WriteWord(int plcIndex, string registerOrTitle, int leng, ref short[] data)
        {
            try
            {
                var item = TempRegisters.Find(e => (e.Title == registerOrTitle.Trim().ToUpper() || e.Address == registerOrTitle.Trim().ToUpper()) && e.ReadOrWrite == READ_OR_WRITE.WRITE);
                
                if (_instances[plcIndex].plc.WriteDeviceBlock2(item?.Address ?? registerOrTitle, leng, ref data[0]) == 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not write word, error: {ex.Message}");
                Files.WriteLog($"Error can not write word, error: {ex.Message}");
                return false;
            }
        }

        public static bool ReadWord(int plcIndex, string registerOrTitle, int leng, out short[] data)
        {
            try
            {
                var item = TempRegisters.Find(e => (e.Title == registerOrTitle.Trim().ToUpper() || e.Address == registerOrTitle.Trim().ToUpper()) && e.ReadOrWrite == READ_OR_WRITE.READ);

                data = new short[leng];

                if (_instances[plcIndex].plc.ReadDeviceBlock2(item?.Address ?? registerOrTitle, leng, out data[0]) == 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not read word, error: {ex.Message}");
                Files.WriteLog($"Error can not read word, error: {ex.Message}");
                data = null;
                return false;
            }
        }

        public static bool WriteDWord(int plcIndex, string registerOrTitle, int leng, ref int[] data)
        {
            try
            {
                var item = TempRegisters.Find(e => (e.Title == registerOrTitle.Trim().ToUpper() || e.Address == registerOrTitle.Trim().ToUpper()) && e.ReadOrWrite == READ_OR_WRITE.WRITE);

                short[] shorts = new short[leng * 2];

                for (int i = 0; i < leng; i++)
                {
                    var arr = ConvertInt2ShortArrForPLC(data[i]);
                    shorts[i * 2] = arr[1];
                    shorts[(i * 2) + 1] = arr[0];
                }

                if (_instances[plcIndex].plc.WriteDeviceBlock2(item?.Address ?? registerOrTitle, leng * 2, ref shorts[0]) == 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not write Dword, error: {ex.Message}");
                Files.WriteLog($"Error can not write Dword, error: {ex.Message}");
                return false;
            }
        }

        public static bool ReadDWord(int plcIndex, string registerOrTitle, int leng, out int[] data)
        {
            try
            {
                var item = TempRegisters.Find(e => (e.Title == registerOrTitle.Trim().ToUpper() || e.Address == registerOrTitle.Trim().ToUpper()) && e.ReadOrWrite == READ_OR_WRITE.READ);

                data = new int[leng];
                var res = new short[leng * 2];

                if (_instances[plcIndex].plc.ReadDeviceBlock2(item?.Address ?? registerOrTitle, leng * 2, out res[0]) == 0)
                {
                    for (int i = 0; i < leng; i++)
                    {
                        var r = ConvertShortArr2Int(res[i * 2 + 1], res[i * 2]);
                        data[i] = r;
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not read Dword, error: {ex.Message}");
                Files.WriteLog($"Error can not read Dword, error: {ex.Message}");
                data = null;
                return false;
            }
        }

        public static bool WriteString(int plcIndex, string registerOrTitle, string currentValue)
        {
            try
            {
                var item = TempRegisters.Find(e => (e.Title == registerOrTitle.Trim().ToUpper() || e.Address == registerOrTitle.Trim().ToUpper()) && e.ReadOrWrite == READ_OR_WRITE.WRITE);
                short[] res4 = ConvertStringToShortArr(currentValue);
                
                if (WriteWord(plcIndex, item?.Address ?? registerOrTitle, res4.Length, ref res4))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not write string, error: {ex.Message}");
                Files.WriteLog($"Error can not write string, error: {ex.Message}");
                return false;
            }
        }

        public static string ReadString(int plcIndex, string registerOrTitle, int length)
        {
            try
            {
                var item = TempRegisters.Find(e => (e.Title == registerOrTitle.Trim().ToUpper() || e.Address == registerOrTitle.Trim().ToUpper()) && e.ReadOrWrite == READ_OR_WRITE.READ);
                
                if (ReadWord(plcIndex, item?.Address ?? registerOrTitle, length, out short[] res4))
                {
                    return ConvertShortArrToString(res4);
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not read string, error: {ex.Message}");
                Files.WriteLog($"Error can not read string, error: {ex.Message}");
                return string.Empty;
            }
        }

        public static bool WriteFloat(int plcIndex, string registerOrTitle, int length, ref float[] data)
        {
            try
            {
                var item = TempRegisters.Find(e => (e.Title == registerOrTitle.Trim().ToUpper() || e.Address == registerOrTitle.Trim().ToUpper()) && e.ReadOrWrite == READ_OR_WRITE.WRITE);

                short[] shorts = new short[length * 2];

                for (int i = 0; i < length; i++)
                {
                    var arr = ConvertInt2ShortArrForPLC(ConvertFloatToDWordInt(data[i]));
                    shorts[i * 2] = arr[1];
                    shorts[(i * 2) + 1] = arr[0];
                }

                if (_instances[plcIndex].plc.WriteDeviceBlock2(item?.Address ?? registerOrTitle, length * 2, ref shorts[0]) == 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not write float, error: {ex.Message}");
                Files.WriteLog($"Error can not write float, error: {ex.Message}");
                return false;
            }
        }

        public static bool ReadFloat(int plcIndex, string registerOrTitle, int length, out float[] data)
        {
            try
            {
                var item = TempRegisters.Find(e => (e.Title == registerOrTitle.Trim().ToUpper() || e.Address == registerOrTitle.Trim().ToUpper()) && e.ReadOrWrite == READ_OR_WRITE.READ);

                data = new float[length];
                var res = new short[length * 2];

                if (_instances[plcIndex].plc.ReadDeviceBlock2(item?.Address ?? registerOrTitle, length * 2, out res[0]) == 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        var r = ConvertDWordIntToFloat(ConvertShortArr2Int(res[i * 2 + 1], res[i * 2]));
                        data[i] = r;
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not read float, error: {ex.Message}");
                Files.WriteLog($"Error can not read float, error: {ex.Message}");
                data = null;
                return false;
            }
        }

        public static short[] ConvertInt2ShortArrForPLC(int value)
        {
            short[] array = new short[2]
            {
                (short)(value / 65536),
                (short)(value % 65536)
            };

            if (value < 0)
            {
                array[0]--;
            }

            return array;
        }

        public static int ConvertShortArr2Int(short highValue, double lowValue)
        {
            return BitConverter.IsLittleEndian ? (((ushort)highValue << 16) | (ushort)lowValue) : (((ushort)lowValue << 16) | (ushort)highValue);
        }

        public static short[] ConvertStringToShortArr(string val)
        {
            if (val.Length % 2 != 0)
            {
                val += "\0";
            }

            short[] array = new short[val.Length / 2];

            for (int i = 0; i < val.Length; i += 2)
            {
                string s = val.Substring(i, 2);
                byte[] bytes = Encoding.ASCII.GetBytes(s);
                short num = BitConverter.ToInt16(bytes, 0);
                array[i / 2] = num;
            }

            return array;
        }

        public static int ConvertFloatToDWordInt(float floatNumber)
        {
            byte[] bytes = BitConverter.GetBytes(floatNumber);
            
            byte[] array = new byte[2]
            {
                bytes[2],
                bytes[3]
            };
            
            byte[] array2 = new byte[2]
            {
                bytes[0],
                bytes[1]
            };
            
            byte[] value = new byte[4]
            {
                array2[0],
                array2[1],
                array[0],
                array[1]
            };

            return BitConverter.ToInt32(value, 0);
        }

        public static float ConvertDWordIntToFloat(int register)
        {
            byte[] bytes = BitConverter.GetBytes(register);

            byte[] value = new byte[4]
            {
                bytes[0],
                bytes[1],
                bytes[2],
                bytes[3]
            };

            return BitConverter.ToSingle(value, 0);
        }

        public static string ConvertShortArrToString(short[] val)
        {
            string text = "";

            for (int i = 0; i < val.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(val[i]);
                text = ((bytes[0] == 0) ? (text + " ") : (text + Convert.ToChar(bytes[0])));
                text = ((bytes[1] == 0) ? (text + " ") : (text + Convert.ToChar(bytes[1])));
            }

            return text;
        }
    }

    public enum READ_OR_WRITE
    {
        READ = 1,
        WRITE = 2
    }

    public enum TYPE_DATA_PLC
    {
        BIT = 1,
        WORD = 2,
        DWORD = 3,
        STRING = 4,
        FLOAT = 5
    }

    public class Register : INotifyPropertyChanged
    {
        private object currentValue;
        public string Title { get; set; }
        public string Address { get; set; }
        public int PlcIndex { get; set; }
        public READ_OR_WRITE? ReadOrWrite { get; set; }
        public TYPE_DATA_PLC? TypeDataPLC { get; set; }
        public bool ReadAlway { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public object CurrentValue
        {
            get => currentValue;
            set
            {
                if (currentValue == null && value == null)
                    return;

                if (currentValue == null && IsDefaultValue(value))
                    return;

                if (!Equals(currentValue, value))
                {
                    currentValue = value;
                    OnPropertyChanged("CurrentValue");
                }
            }
        }

        private bool IsDefaultValue(object value)
        {
            if (value == null)
                return true;

            if (value is string strValue)
                return string.IsNullOrWhiteSpace(strValue);

            var type = value.GetType();

            if (type.IsValueType)
                return value.Equals(Activator.CreateInstance(type));

            return false;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
