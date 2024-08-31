using ActUtlTypeLib;
using MS_Seed.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MS_Seed.IndustrialCommunication.PLC
{
    public class DataTypePLC
    {
        public string Register { get; set; }
        public string Title { get; set; }
        public Enum TypePLC { get; set; }
        public int ReadOrWrite { get; set; }
    }

    public enum TYPE_PLC
    {
        BIT = 1,
        WORD = 2,
        DWORD = 3,
        FLOAT = 4,
        STRING = 5
    }

    public class ControlPLCMishubishi :INotifyPropertyChanged
    {
        public static ControlPLCMishubishi _instance;

        private static readonly object _lock = new object();

        public event PropertyChangedEventHandler PropertyChanged;

        public const int READ = 1;
        public const int WRITE = 2;

        private readonly ActUtlType _plc1 = new ActUtlType();
        private readonly ActUtlType _plc2 = new ActUtlType();
        private readonly ActUtlType _plc3 = new ActUtlType();
        private readonly ActUtlType _plc4 = new ActUtlType();
        private readonly ActUtlType _plc5 = new ActUtlType();

        private Thread thread1;
        private Thread thread2;
        private Thread thread3;
        private Thread thread4;
        private Thread thread5;

        private readonly int PortMx1 = 1;
        private readonly int PortMx2 = 2;
        private readonly int PortMx3 = 3;
        private readonly int PortMx4 = 4;
        private readonly int PortMx5 = 5;

        private readonly List<DataTypePLC> List1 = new List<DataTypePLC>();
        private readonly List<DataTypePLC> List2 = new List<DataTypePLC>();
        private readonly List<DataTypePLC> List3 = new List<DataTypePLC>();
        private readonly List<DataTypePLC> List4 = new List<DataTypePLC>();
        private readonly List<DataTypePLC> List5 = new List<DataTypePLC>();

        public ControlPLCMishubishi()
        {

        }

        public static ControlPLCMishubishi Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ControlPLCMishubishi();
                    }
                    return _instance;
                }
            }
        }

        public void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool ConnectPLC1()
        {
            try
            {
                ActUtlType plc = GetCurrentIndexPLC(1);

                plc.ActLogicalStationNumber = PortMx1;

                if (plc.Open() == 0)
                {
                    AddRegisterToPLC1();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Can not connect with PLC station {PortMx1}, error: {ex.Message}");
                Global.ShowBoxError($"Can not connect with PLC station {PortMx1}, error: {ex.Message}");
                return false;
            }

            //if (GetCurrentIndexPLC((int)Enums.PLC.PLC_1).Open() == 0)
            //{

            //}

            //if (_plc1.Open() == 0)
            //{
            //    Thread threadReadStatusPLC = new Thread(async () => await ReadStatusPLC());
            //    threadReadStatusPLC.Name = "THREAD_READ_STATUS_PLC";
            //    threadReadStatusPLC.IsBackground = true;
            //    threadReadStatusPLC.Start();
            //}
            //else
            //{
            //    Global.ShowBoxError($"Error can not connect to PLC");
            //}
        }

        public bool ConnectPLC2()
        {
            try
            {
                ActUtlType plc = GetCurrentIndexPLC(2);

                plc.ActLogicalStationNumber = PortMx2;

                return plc.Open() == 0;
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Can not connect with PLC station {PortMx2}, error: {ex.Message}");
                Global.ShowBoxError($"Can not connect with PLC station {PortMx2}, error: {ex.Message}");
                return false;
            }
        }

        public void AddRegisterToPLC1()
        {
            List1.AddRange(new List<DataTypePLC>
            {
                new DataTypePLC { Register = "M34000", Title = "ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ },
                new DataTypePLC { Register = "M34100", Title = "WRITE_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = WRITE},
            });

            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("111");
                    Thread.Sleep(50);
                }
            });

            //foreach (var item in List1)
            //{
            //    Console.WriteLine(item.Register);
            //}
        }

        private void ReadDataPLC1()
        {
            throw new NotImplementedException();
        }

        //list linq => where name => bit
        public void WriteBit(int indexPLC, string register, bool value)
        {
            GetCurrentIndexPLC(indexPLC).SetDevice2(register, value ? (short)1 : (short)0);
        }

        public void WriteWord(int indexPLC, string register, int value)
        {
            GetCurrentIndexPLC(indexPLC).WriteDeviceBlock2(register, 1, (short)value);
        }

        public void WriteDWord(int indexPLC, string register, int leng, ref int[] value)
        {
            short[] shorts = new short[leng * 2];

            for (int i = 0; i < leng; i++)
            {
                var arr = ConvertInt2ShortArrForPLC(value[i]);
                shorts[i * 2] = arr[1];
                shorts[(i * 2) + 1] = arr[0];
            }

            GetCurrentIndexPLC(indexPLC).WriteDeviceBlock2(register, leng * 2, ref shorts[0]);
        }

        public bool WriteString(int indexPLC, string regrister, string value)
        {
            short[] res4 = ConvertStringToShortArr(((string)value));

            return WriteWord(indexPLC, regrister, res4.Length, ref res4);
        }

        public bool WriteWord(int indexPLC, string register, int leng, ref short[] data)
        {
            var result = GetCurrentIndexPLC(indexPLC)?.WriteDeviceBlock2(register, leng, ref data[0]);

            if (result == 0 || result == 0)
            {
                return true;
            }

            return false;
        }

        public void WriteFloat(int indexPLC, string register, int leng, ref float[] data)
        {
            short[] shorts = new short[leng * 2];
            for (int i = 0; i < leng; i++)
            {
                //var arr = ConvertInt2ShortArrForPLC(ConvertFloatToDWordInt(data[i]));
                //shorts[i * 2] = arr[1];
                //shorts[(i * 2) + 1] = arr[0];
            }

            GetCurrentIndexPLC(indexPLC)?.WriteDeviceBlock2(register, leng * 2, ref shorts[0]);
        }

        public bool ReadBit(int indexPLC, string address)
        {
            short val = 0;
            GetCurrentIndexPLC(indexPLC)?.GetDevice2(address, out val);

            return val == 1;
        }

        public bool ReadWord(int indexPLC, string register, int leng, out short[] data)
        {
            data = new short[leng];

            if (GetCurrentIndexPLC(indexPLC)?.ReadDeviceBlock2(register, leng, out data[0]) == 0)
            {
                return true;
            }

            return false;
        }

        public bool ReadDWord(int indexPLC, string register, int leng, out int[] data)
        {
            data = new int[leng];
            var res = new short[leng * 2];

            if (GetCurrentIndexPLC(indexPLC)?.ReadDeviceBlock2(register, leng * 2, out res[0]) == 0)
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

        public bool ReadString(int indexPLC, string register, int leng, out short[] data)
        {
            data = new short[leng];

            if (GetCurrentIndexPLC(indexPLC)?.ReadDeviceBlock2(register, leng, out data[0]) == 0)
            {
                return true;
            }

            return false;
        }

        public bool ReadFloat(int indexPLC, string register, int leng, out float[] data)
        {
            data = new float[leng];
            var res = new short[leng * 2];


            if (GetCurrentIndexPLC(indexPLC)?.ReadDeviceBlock2(register, leng * 2, out res[0]) == 0)
            {
                //for (int i = 0; i < leng; i++)
                //{
                //    var r = ConvertShortArr2Int(res[i * 2 + 1], res[i * 2]).ConvertDWordIntToFloat();
                //    data[i] = r;
                //}

                return true;
            }

            return false;
        }

        public ActUtlType GetCurrentIndexPLC(int indexPLC)
        {
            ActUtlType plc = null;

            switch (indexPLC)
            {
                case (int)Enums.PLC.PLC_1:
                    plc = _plc1;
                    break;

                case (int)Enums.PLC.PLC_2:
                    plc = _plc2;
                    break;

                case (int)Enums.PLC.PLC_3:
                    plc = _plc3;
                    break;

                case (int)Enums.PLC.PLC_4:
                    plc = _plc4;
                    break;
            }

            return plc;
        }

        public short[] ConvertStringToShortArr(string val)
        {
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

        public int ConvertShortArr2Int(short highValue, double lowValue)
        {
            return BitConverter.IsLittleEndian ? (((ushort)highValue << 16) | (ushort)lowValue) : (((ushort)lowValue << 16) | (ushort)highValue);
        }

        public short[] ConvertInt2ShortArrForPLC(int value)
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

        public string ConvertShortArrToString(short[] val)
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

        //public int ConvertFloatToDWordInt(this float floatNumber)
        //{
        //    byte[] bytes = BitConverter.GetBytes(floatNumber);

        //    byte[] array = new byte[2]
        //    {
        //        bytes[2],
        //        bytes[3]
        //    };

        //    byte[] array2 = new byte[2]
        //    {
        //        bytes[0],
        //        bytes[1]
        //    };

        //    byte[] value = new byte[4]
        //    {
        //        array2[0],
        //        array2[1],
        //        array[0],
        //        array[1]
        //    };

        //    return BitConverter.ToInt32(value, 0);
        //}
    }
}
