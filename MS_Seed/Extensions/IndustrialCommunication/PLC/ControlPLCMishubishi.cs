using ActUtlTypeLib;
using MS_Seed.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
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

        public List<Register> Registers { get; private set; }

        private ControlPLCMishubishi(int plcIndex, int plcStation)
        {
            plc = new ActUtlType();
            plc.ActLogicalStationNumber = plcStation;
            indexPLC = plcIndex;
            isReading = false;
            Registers = new List<Register>();
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
            return plc.Open() == 0;
        }

        public void DisconnectPLC()
        {
            plc.Close();
            StopReading();
        }

        public void StartReading()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    foreach (var register in Registers)
                    {
                        plc.GetDevice(register.Address, out int readValue);
                        register.CurrentValue = readValue;
                    }
                    Thread.Sleep(10);
                }
            });
        }

        public void StopReading()
        {
            isReading = false;
        }

        public void ConfigureRegisters(params Register[] registers)
        {
            Registers.AddRange(registers);
        }

        public static bool WriteBit(int plcIndex, string register, bool value)
        {
            try
            {
                var result = _instances[plcIndex].plc.SetDevice2(register, value ? (short)1 : (short)0);
                return result == 1 || result == 0;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not write bit, error: {ex.Message}");
                Files.WriteLog($"Error can not write bit, error: {ex.Message}");
                return false;
            }
        }

        public static bool ReadBit(int plcIndex, string register)
        {
            try
            {
                _instances[plcIndex].plc.GetDevice2(register, out short value);
                return value == 1;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not read bit, error: {ex.Message}");
                Files.WriteLog($"Error can not read bit, error: {ex.Message}");
                return false;
            }
        }

        public static bool WriteWord(int plcIndex, string register, int leng, ref short[] data)
        {
            try
            {
                return _instances[plcIndex].plc.WriteDeviceBlock2(register, leng, ref data[0]) == 0;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not write word, error: {ex.Message}");
                Files.WriteLog($"Error can not write word, error: {ex.Message}");
                return false;
            }
        }

        public static bool ReadWord(int plcIndex, string register, int leng, out short[] data)
        {
            try
            {
                data = new short[leng];
                return _instances[plcIndex].plc.ReadDeviceBlock2(register, leng, out data[0]) == 0;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not read word, error: {ex.Message}");
                Files.WriteLog($"Error can not read word, error: {ex.Message}");
                data = null;
                return false;
            }
        }

        public static bool WriteDWord(int plcIndex, string register, int leng, ref int[] data)
        {
            try
            {
                short[] shorts = new short[leng * 2];

                for (int i = 0; i < leng; i++)
                {
                    var arr = ConvertInt2ShortArrForPLC(data[i]);
                    shorts[i * 2] = arr[1];
                    shorts[(i * 2) + 1] = arr[0];
                }

                return _instances[plcIndex].plc.WriteDeviceBlock2(register, leng * 2, ref shorts[0]) == 0;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not write Dword, error: {ex.Message}");
                Files.WriteLog($"Error can not write Dword, error: {ex.Message}");
                return false;
            }
        }

        public static bool ReadDWord(int plcIndex, string register, int leng, out int[] data)
        {
            try
            {
                data = new int[leng];
                var res = new short[leng * 2];

                if (_instances[plcIndex].plc.ReadDeviceBlock2(register, leng * 2, out res[0]) == 0)
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

        public static bool WriteString(int plcIndex, string register, string currentValue)
        {
            try
            {
                short[] res4 = ConvertStringToShortArr(currentValue);
                return WriteWord(plcIndex, register, res4.Length, ref res4);
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not write string, error: {ex.Message}");
                Files.WriteLog($"Error can not write string, error: {ex.Message}");
                return false;
            }
        }

        public static string ReadString(int plcIndex, string register, int length)
        {
            try
            {
                ReadWord(plcIndex, register, length, out short[] res4);
                return ConvertShortArrToString(res4);
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not read string, error: {ex.Message}");
                Files.WriteLog($"Error can not read string, error: {ex.Message}");
                return string.Empty;
            }
        }

        public static bool WriteFloat(int plcIndex, string register, int length, ref float[] data)
        {
            try
            {
                short[] shorts = new short[length * 2];

                for (int i = 0; i < length; i++)
                {
                    var arr = ConvertInt2ShortArrForPLC(ConvertFloatToDWordInt(data[i]));
                    shorts[i * 2] = arr[1];
                    shorts[(i * 2) + 1] = arr[0];
                }

                return _instances[plcIndex].plc.WriteDeviceBlock2(register, length * 2, ref shorts[0]) == 0;
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error can not write float, error: {ex.Message}");
                Files.WriteLog($"Error can not write float, error: {ex.Message}");
                return false;
            }
        }

        public static bool ReadFloat(int plcIndex, string register, int length, out float[] data)
        {
            try
            {
                data = new float[length];
                var res = new short[length * 2];

                if (_instances[plcIndex].plc.ReadDeviceBlock2(register, length * 2, out res[0]) == 0)
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
                val = val + "\0";
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
        private int currentValue;
        public string Title { get; set; }
        public string Address { get; set; }
        public int PlcIndex { get; set; }
        public READ_OR_WRITE? ReadOrWrite { get; set; }
        public TYPE_DATA_PLC? TypeDataPLC { get; set; }
        public bool ReadAlway { get; set; } = true;

        public event PropertyChangedEventHandler PropertyChanged;

        public int CurrentValue
        {
            get => currentValue;
            set
            {
                if (currentValue != value)
                {
                    currentValue = value;
                    OnPropertyChanged("CurrentValue");
                }
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
