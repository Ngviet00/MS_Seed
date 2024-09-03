using ActUtlTypeLib;
using MS_Seed.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MS_Seed.Enums;

namespace MS_Seed.IndustrialCommunication.PLC
{
    public class DataTypePLC
    {
        public string Register { get; set; }
        public string Title { get; set; }
        public TYPE_PLC TypePLC { get; set; }
        public READ_OR_WRITE ReadOrWrite { get; set; }
        public bool ReadAlway { get; set; } = false;
    }

    public enum TYPE_PLC
    {
        BIT = 1,
        WORD = 2,
        DWORD = 3,
        FLOAT = 4,
        STRING = 5
    }

    public enum READ_OR_WRITE
    {
        READ = 1,
        WRITE = 2,
    }

    public class ListPLC
    {
        public int IndexPLC { get; set; }
        public ActUtlType ActUtlType { get; set; }
        public Thread Thread { get; set; }
        public int PortMx { get; set; }
        public List<DataTypePLC> ListDataTypePLC { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public Task Task { get; set; }
    }

    public class PLCData : INotifyPropertyChanged
    {
        private int _indexPLC;
        private string _title;
        private int _currentValue;

        public int IndexPLC
        {
            get => _indexPLC;
            set
            {
                if (_indexPLC != value)
                {
                    _indexPLC = value;
                    OnPropertyChanged(nameof(IndexPLC));
                }
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public int CurrentValue
        {
            get => _currentValue;
            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    OnPropertyChanged(nameof(CurrentValue));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ControlPLCMishubishi : INotifyPropertyChanged
    {
        public List<PLCData> _plcDataList = new List<PLCData>();

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void Notify([CallerMemberName] string name = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //}


        public static ControlPLCMishubishi _instance;

        private static readonly object _lock = new object();

        List<ListPLC> listPLCs = new List<ListPLC>();

        private readonly Dictionary<int, short> _currentValues = new Dictionary<int, short>();
        private readonly Dictionary<int, int> _indexes = new Dictionary<int, int>();
        private readonly Dictionary<int, string> _titles = new Dictionary<int, string>();

        public short GetCurrentValue(int plcId) => _currentValues.TryGetValue(plcId, out var value) ? value : default;
        public int GetIndexPLC(int plcId) => _indexes.TryGetValue(plcId, out var index) ? index : default;
        public string GetTitle(int plcId) => _titles.TryGetValue(plcId, out var title) ? title : default;

        #region Định nghĩa _plc, thread, PortMx, List thanh ghi theo số lượng PLC kết nối

        // Defined list threads
        private Thread[] _threads = new Thread[10];

        // Defined list AcyUtlType
        public List<ActUtlType> ListActUtlType = new List<ActUtlType>()
        {
            new ActUtlType(),
            new ActUtlType(),
            new ActUtlType(),
            new ActUtlType(),
            new ActUtlType(),
            new ActUtlType(),
            new ActUtlType(),
            new ActUtlType(),
            new ActUtlType(),
            new ActUtlType()
        };

        // Defined list registers
        public List<List<DataTypePLC>> ListDataTypePlcs = new List<List<DataTypePLC>>
        {
            new List<DataTypePLC>(),
            new List<DataTypePLC>(),
            new List<DataTypePLC>(),
            new List<DataTypePLC>(),
            new List<DataTypePLC>(),
            new List<DataTypePLC>(),
            new List<DataTypePLC>(),
            new List<DataTypePLC>(),
            new List<DataTypePLC>(),
            new List<DataTypePLC>(),
        };

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged1;
        protected virtual void Notify1([CallerMemberName] string name = "")
        {
            PropertyChanged1?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged2;
        protected virtual void Notify2([CallerMemberName] string name = "")
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged3;
        protected virtual void Notify3([CallerMemberName] string name = "")
        {
            PropertyChanged3?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Thuộc tính trả về của PLC khi thuộc tính thay đổi

        private int _indexPLC;
        public int IndexPLC
        {
            get { return _indexPLC; }

            set
            {
                if (_indexPLC != value)
                {
                    _indexPLC = value;
                    Notify1();
                }
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }

            set
            {
                if (_title != value)
                {
                    _title = value;
                    Notify1();
                }
            }
        }

        private object _currentValue;
        public object CurrentValue
        {
            get { return _currentValue; }

            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    Notify1();
                }
            }
        }

        private int _indexPLC1;
        public int IndexPLC1
        {
            get { return _indexPLC1; }

            set
            {
                if (_indexPLC1 != value)
                {
                    _indexPLC1 = value;
                    Notify2();
                }
            }
        }

        private string _title1;
        public string Title1
        {
            get { return _title1; }

            set
            {
                if (_title1 != value)
                {
                    _title1 = value;
                    Notify2();
                }
            }
        }

        private object _currentValue1;
        public object CurrentValue1
        {
            get { return _currentValue1; }

            set
            {
                if (_currentValue1 != value)
                {
                    _currentValue1 = value;
                    Notify2();
                }
            }
        }

        #endregion

        public ControlPLCMishubishi()
        {
            // Define PLC_1
            //ListPLC plc1 = new ListPLC
            //{
            //    IndexPLC = 1,
            //    //ActUtlType = new ActUtlType(),
            //    PortMx = 1,
            //};

            //// Define register
            //List<DataTypePLC> list1 = new List<DataTypePLC>()
            //{
            //    new DataTypePLC { Register = "M34000", Title = "ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ, ReadAlway = true },
            //    new DataTypePLC { Register = "M34300", Title = "TEST_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ, ReadAlway = true },
            //    new DataTypePLC { Register = "M34100", Title = "WRITE_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = WRITE },
            //};

            //plc1.ListDataTypePLC = list1;
            //listPLCs.Add(plc1);

            //// Define PLC_2
            //ListPLC plc2 = new ListPLC
            //{
            //    IndexPLC = 2,
            //    ActUtlType = new ActUtlType(),
            //    PortMx = 2,
            //};

            //List<DataTypePLC> list2 = new List<DataTypePLC>()
            //{
            //    new DataTypePLC { Register = "M34000", Title = "ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ, ReadAlway = true },
            //    new DataTypePLC { Register = "M34300", Title = "TEST_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ, ReadAlway = true },
            //    new DataTypePLC { Register = "M34100", Title = "WRITE_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = WRITE },
            //};

            //plc2.ListDataTypePLC = list2;
            //listPLCs.Add(plc2);
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

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void Notify([CallerMemberName] string name = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //}

        //public bool ConnectToPLC(int indexPLC)
        //{
        //    try
        //    {
        //        var plc = listPLCs.Find(e => e.IndexPLC == indexPLC);

        //        if (plc == null)
        //        {
        //            Files.WriteLog($"Error can not connect PLC {indexPLC}");
        //            Global.ShowBoxError($"Error can not connect PLC {indexPLC}");
        //        }

        //        plc.ActUtlType = new ActUtlType();

        //        plc.ActUtlType.ActLogicalStationNumber = plc.PortMx;

        //        if (plc.ActUtlType.Open() == 0)
        //        {
        //            Thread a = new Thread(() => {
        //                while (true)
        //                {
        //                    ReadDataFromPLC(plc.ListDataTypePLC, indexPLC, plc.ActUtlType);
        //                    Thread.Sleep(3000);
        //                    //await Task.Delay(1);
        //                }
        //            });
        //            //plc.Thread = new Thread(() =>
        //            //{
        //            //    while (true)
        //            //    {
        //            //        ReadDataFromPLC(plc.ListDataTypePLC, indexPLC, plc.ActUtlType);
        //            //        Thread.Sleep(20);
        //            //        //await Task.Delay(1);
        //            //    }
        //            //});

        //            a.IsBackground = true;
        //            a.Start();

        //            //plc.Thread.Name = $"THREAD_CONNECT_PLC_{indexPLC}";
        //            //plc.Thread.IsBackground = true;
        //            //plc.Thread.Start();
        //            //Task.Run(async () =>
        //            //{
        //            //    while (true)
        //            //    {
        //            //        ReadDataFromPLC(plc.ListDataTypePLC, indexPLC, _plc);
        //            //        await Task.Delay(1);
        //            //    }
        //            //});

        //            return true;
        //        }
        //        else
        //        {
        //            Global.ShowBoxWarning($"Error can not connect to PLC number {indexPLC}");
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Files.WriteLog($"Error can not connect to PLC number {indexPLC}, error: {ex.Message}");
        //        return false;
        //    }
        //}

        public bool ConnectPLC(int indexPLC, List<DataTypePLC> listRegister, int port = 1)
        {
            var plcData = new PLCData { IndexPLC = indexPLC };
            _plcDataList.Add(plcData);

            listRegister = listRegister.Where(i => i.ReadOrWrite == READ_OR_WRITE.READ).ToList();

            ListActUtlType[indexPLC - 1].ActLogicalStationNumber = port;

            if (ListActUtlType[indexPLC - 1].Open() == 0)
            {
                // DEFINED REGISTER PLC
                _threads[indexPLC - 1] = new Thread(() => ReadDataFromPLC(listRegister, plcData, indexPLC, ListActUtlType[indexPLC - 1]));
                _threads[indexPLC - 1].IsBackground = true;
                _threads[indexPLC - 1].Name = $"THREAD_READ_PLC_INDEX_{indexPLC}";
                _threads[indexPLC - 1].Start();
                //Task.Delay(4).Wait();

                return true;
            }
            else
            {
                Global.ShowBoxError($"Error can not connect to PLC station number: {port}");
                return false;
            }
        }

        //public bool ConnectPLC1(int indexPLC)
        //{
        //    try
        //    {
        //        ActUtlType plc = GetCurrentIndexPLC(indexPLC);

        //        plc.ActLogicalStationNumber = PortMx1;

        //        if (plc.Open() == 0)
        //        {
        //            Task.Run(() => {

        //                //define list register
        //                //defind task run thread 
        //                //function read plc

        //                List1.AddRange(new List<DataTypePLC>
        //                {
        //                    new DataTypePLC { Register = "M34000", Title = "ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ, ReadAlway = true },
        //                    new DataTypePLC { Register = "M34300", Title = "TEST_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ, ReadAlway = true },
        //                    new DataTypePLC { Register = "M34100", Title = "WRITE_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = WRITE },
        //                });

        //                List<DataTypePLC> listRegisters = List1.Where(i => i.ReadOrWrite == READ_OR_WRITE.READ && i.ReadAlway == true).ToList();

        //                Task.Run(async () =>
        //                {
        //                    while (true)
        //                    {
        //                        ReadDataFromPLC(listRegisters, indexPLC);
        //                        await Task.Delay(100);
        //                    }
        //                });

        //                //AddRegisterToPLC1(indexPLC);
        //            });

        //            return true;

        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Files.WriteLog($"Can not connect with PLC station {PortMx1}, error: {ex.Message}");
        //        Global.ShowBoxError($"Can not connect with PLC station {PortMx1}, error: {ex.Message}");
        //        return false;
        //    }
        //}

        //public bool ConnectPLC2(int idxPLC)
        //{
        //    try
        //    {
        //        int indexPLC = (int)Enums.PLC.PLC_2;

        //        ActUtlType plc = GetCurrentIndexPLC(indexPLC);

        //        plc.ActLogicalStationNumber = PortMx2;

        //        if (plc.Open() == 0)
        //        {
        //            AddRegisterToPLC2(indexPLC);
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Files.WriteLog($"Can not connect with PLC station {PortMx1}, error: {ex.Message}");
        //        Global.ShowBoxError($"Can not connect with PLC station {PortMx1}, error: {ex.Message}");
        //        return false;
        //    }
        //}

        //public void AddRegisterToPLC1(int indexPLC)
        //{
        //    //địa chỉ thanh ghi; đặt tên tiêu đề cho thanh ghi đó; loại BIT, DWord,...; trạng thái BIT đó đọc hay ghi; có đọc liên tục hay không

        //    List1.AddRange(new List<DataTypePLC>
        //    {
        //        new DataTypePLC { Register = "M34000", Title = "ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ, ReadAlway = true },
        //        new DataTypePLC { Register = "M34300", Title = "TEST_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ, ReadAlway = true },
        //        new DataTypePLC { Register = "M34100", Title = "WRITE_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = WRITE },
        //    });

        //    List<DataTypePLC> listRegisters = List1.Where(i => i.ReadOrWrite == READ && i.ReadAlway == true).ToList();

        //    Task.Run(async () =>
        //    {
        //        while (true)
        //        {
        //            ReadDataFromPLC(listRegisters, indexPLC);
        //            await Task.Delay(1000);
        //        }
        //    });
        //}

        public void AddRegisterToPLC2(int indexPLC)
        {
            //địa chỉ thanh ghi; đặt tên tiêu đề cho thanh ghi đó; loại BIT, DWord,...; trạng thái BIT đó đọc hay ghi; có đọc liên tục hay không

            //List2.AddRange(new List<DataTypePLC>
            //{
            //    new DataTypePLC { Register = "M34000", Title = "ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ, ReadAlway = true },
            //    new DataTypePLC { Register = "M34300", Title = "TEST_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ, ReadAlway = true },
            //    new DataTypePLC { Register = "M34100", Title = "WRITE_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = WRITE },
            //});

            //List<DataTypePLC> listRegisters = List2.Where(i => i.ReadOrWrite == READ && i.ReadAlway == true).ToList();

            //Task.Run(async () =>
            //{
            //    while (true)
            //    {
            //        ReadDataFromPLC(listRegisters, indexPLC);
            //        await Task.Delay(1);
            //    }
            //});
        }

        private void ReadDataFromPLC(List<DataTypePLC> listRegisterPLC, PLCData plcData, int idxPLC, ActUtlType _plc = null)
        {
            while (true)
            {
                _plc.GetDevice2("M34000", out short result);

                if (idxPLC == 1)
                {
                    Title = "ALIVE";
                    IndexPLC = idxPLC;
                    CurrentValue = result;
                }

                if (idxPLC == 2)
                {
                    Title1 = "ALIVE";
                    IndexPLC1 = idxPLC;
                    CurrentValue1 = result;
                }
                //foreach (var item in listRegisterPLC)
                //{
                //    if (item.TypePLC == TYPE_PLC.BIT)
                //    {
                //        _plc.GetDevice2(item.Register, out short result);

                //        if (idxPLC == 1)
                //        {
                //            Title = item.Title;
                //            IndexPLC =idxPLC;
                //            CurrentValue = result;
                //        }
                //        else if (idxPLC == 2)
                //        {
                //            Title1 = item.Title;
                //            IndexPLC1 = idxPLC;
                //            CurrentValue1 = result;
                //        }
                //    }
                //}

                Thread.Sleep(2000);
            }
            //foreach (var item in listRegisterPLC)
            //{
            //    switch (item.TypePLC)
            //    {
            //        case TYPE_PLC.BIT:
            //            IndexPLC = idxPLC;
            //            Title = item.Title;
            //            short result = 0;
            //            GetCurrentIndexPLC(idxPLC)?.GetDevice2(item.Register, out result);
            //            CurrentValue = result;
            //            break;

            //        case TYPE_PLC.WORD:
            //            break;

            //        case TYPE_PLC.DWORD:

            //            break;

            //        case TYPE_PLC.STRING:
            //            break;

            //        case TYPE_PLC.FLOAT:
            //            break;
            //    }
            //}
            
        }

        //public void Test1(List<DataTypePLC> listRegisterPLC, int idxPLC, ActUtlType _plc = null)
        //{
        //    foreach (var item in listRegisterPLC)
        //    {
        //        if (item.TypePLC == TYPE_PLC.BIT)
        //        {
        //            //lock (_lockObject11)
        //            //{
        //                //IndexPLC = idxPLC;
        //                //Title = item.Title;
        //                //short result = 0;
        //                count++;
        //                _plc.GetDevice2(item.Register, out short result1);
                        
        //                Console.WriteLine($"ham read data from plc: {idxPLC}, {_plc.ActLogicalStationNumber}, {count}, {result1}");
        //                //Thread.Sleep(100); // Adjust as needed
        //                                   //CurrentValue = result;
        //                                   //Thread.Sleep(50);
        //            //}

        //        }
        //    }
        //}

        public void WriteBit(int indexPLC, string title, bool value)
        {
            var item = ListDataTypePlcs[indexPLC - 1].Find(x => (x.Title.ToLower() == title.ToLower() || x.Register.ToLower() == title.ToLower()) && x.ReadOrWrite == READ_OR_WRITE.WRITE);
            ListActUtlType[indexPLC - 1].SetDevice2(item.Register, value ? (short)1 : (short)0);
        }

        public void WriteWord(int indexPLC, string title, int value)
        {
            //var item = List1.Find(x => x.Title.ToLower() == title.ToLower());
            GetCurrentIndexPLC(indexPLC).WriteDeviceBlock2(title, 1, (short)value);
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
                //case (int)Enums.PLC.PLC_1:
                //    plc = _plc1;
                //    break;

                //case (int)Enums.PLC.PLC_2:
                //    plc = _plc2;
                //    break;

                //case (int)Enums.PLC.PLC_3:
                //    plc = _plc3;
                //    break;

                //case (int)Enums.PLC.PLC_4:
                //    plc = _plc4;
                //    break;
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
