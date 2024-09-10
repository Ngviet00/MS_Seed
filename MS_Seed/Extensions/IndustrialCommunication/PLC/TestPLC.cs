using ActUtlTypeLib;
using MS_Seed.Common;
using MS_Seed.IndustrialCommunication.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MS_Seed.Enums;

namespace MS_Seed.Extensions.IndustrialCommunication.PLC
{
    public class Register : INotifyPropertyChanged
    {
        private int currentValue;
        public string Title { get; set; }
        public string Address { get; set; }
        public int PlcIndex { get; set; }

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

    public class ControlPLCMishu
    {
        private static readonly Dictionary<int, ControlPLCMishu> _instances = new Dictionary<int, ControlPLCMishu>();
        private static readonly object _lock = new object();

        private ActUtlType plc;
        private bool isReading;

        public List<Register> Registers { get; private set; }

        private ControlPLCMishu(int plcStation)
        {
            plc = new ActUtlType();
            plc.ActLogicalStationNumber = plcStation;
            isReading = false;
            Registers = new List<Register>();
        }

        public static ControlPLCMishu GetInstance(int plcStation)
        {
            lock (_lock)
            {
                if (!_instances.ContainsKey(plcStation))
                {
                    _instances[plcStation] = new ControlPLCMishu(plcStation);
                }
                return _instances[plcStation];
            }
        }

        public bool ConnectPLC()
        {
            int result = plc.Open();
            return result == 0;  // 0 là kết nối thành công
        }

        public void DisconnectPLC()
        {
            plc.Close();
            StopReading();
        }

        public void StartReading()
        {
            isReading = true;
            Task.Run(() => ReadRegistersLoop());
        }

        public void StopReading()
        {
            isReading = false;
        }

        public void ConfigureRegisters(params Register[] registers)
        {
            Registers.AddRange(registers);
        }

        public bool WriteDevice(string address, int value)
        {
            int result = plc.SetDevice(address, value);
            return result == 0; // 0 là ghi thành công
        }

        public int GetStationNumber()
        {
            return plc.ActLogicalStationNumber;
        }

        public static bool WriteToPLC(int plcIndex, string registerAddress, int value)
        {
            if (_instances.ContainsKey(plcIndex))
            {
                var plcController = _instances[plcIndex];
                return plcController.WriteDevice(registerAddress, value);
            }
            else
            {
                Console.WriteLine($"PLC with index {plcIndex} not found.");
                return false;
            }
        }

        private void ReadRegistersLoop()
        {
            while (isReading)
            {
                foreach (var register in Registers)
                {
                    int readValue = 0;
                    int result = plc.GetDevice(register.Address, out readValue);
                    if (result == 0) // Đọc thành công
                    {
                        register.CurrentValue = readValue;
                    }
                }
                Thread.Sleep(100); // Tạm dừng giữa các lần đọc
            }
        }
    }

    //public class ControlPLCMishu
    //{
    //    private ActUtlType plc;
    //    private int plcStation;
    //    private bool isReading;

    //    public List<Register> Registers { get; private set; }

    //    public ControlPLCMishu(int plcStation)
    //    {
    //        this.plcStation = plcStation;
    //        plc = new ActUtlType();
    //        plc.ActLogicalStationNumber = plcStation;
    //        isReading = false;
    //        Registers = new List<Register>();
    //    }

    //    public bool ConnectPLC()
    //    {
    //        int result = plc.Open();
    //        return result == 0;
    //    }

    //    public void DisconnectPLC()
    //    {
    //        plc.Close();
    //        StopReading();
    //    }

    //    public void StartReading()
    //    {
    //        isReading = true;
    //        Task.Run(() => ReadRegistersLoop());
    //    }

    //    public void StopReading()
    //    {
    //        isReading = false;
    //    }

    //    private void ReadRegistersLoop()
    //    {
    //        while (isReading)
    //        {
    //            foreach (var register in Registers)
    //            {
    //                //switch 5 case
    //                plc.GetDevice(register.Address, out int readValue);
    //                register.CurrentValue = readValue;
    //            }
    //            Thread.Sleep(100);
    //        }
    //    }
    //}
}
