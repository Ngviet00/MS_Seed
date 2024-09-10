using ActUtlTypeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MS_Seed.Extensions.IndustrialCommunication.PLC
{
    public class PLC : INotifyPropertyChanged
    {
        private static readonly Lazy<PLC> _instance = new Lazy<PLC>(() => new PLC());

        private string _title;
        private object _currentValue;
        private int _indexPLC;

        private ActUtlType actUtl;

        public Dictionary<string, int> RegisterValues { get; set; }

        public Dictionary<string, string> RegisterAddresses { get; set; }

        public int PlcPort { get; set; }

        public static PLC Instance => _instance.Value;

        public PLC()
        {
            RegisterAddresses = new Dictionary<string, string>(); 
            RegisterValues = new Dictionary<string, int>();  
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    //OnPropertyChanged(nameof(Title));
                }
            }
        }

        public object CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    OnPropertyChanged(nameof(CurrentValue));
                }
            }
        }

        public int IndexPLC
        {
            get { return _indexPLC; }
            set
            {
                if (_indexPLC != value)
                {
                    _indexPLC = value;
                    OnPropertyChanged(nameof(IndexPLC));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void InitializePLCConnection(int port)
        {
            actUtl = new ActUtlType();
            PlcPort = port;
            actUtl.ActLogicalStationNumber = port;

            int ret = actUtl.Open();
            if (ret != 0)
            {
                throw new Exception($"Không thể mở kết nối đến PLC với cổng {port}. Lỗi: {ret}");
            }
        }

        public void ReadValuePLC()
        {
            // Kiểm tra nếu kết nối chưa mở
            if (actUtl == null)
            {
                throw new Exception("PLC chưa được khởi tạo kết nối.");
            }

            foreach (var entry in RegisterAddresses)
            {
                string registerName = entry.Key;
                string registerAddress = entry.Value;

                actUtl.GetDevice2(registerAddress, out short value);

                if (RegisterValues.ContainsKey(registerName))
                {
                    if (RegisterValues[registerName] != value)
                    {
                        RegisterValues[registerName] = value;
                        CurrentValue = value;
                        Title = registerName;
                    }
                }
                else
                {
                    RegisterValues.Add(registerName, value);
                    Title = registerName;

                    CurrentValue = value;
                }
            }
        }

        // Ghi dữ liệu vào PLC
        public void WriteValuePLC(string deviceAddress, int value)
        {
            if (actUtl == null)
            {
                throw new Exception("PLC chưa được khởi tạo kết nối.");
            }

            // Ghi giá trị vào thiết bị PLC tại địa chỉ tương ứng
            int ret = actUtl.WriteDeviceBlock(deviceAddress, 1, ref value);
            if (ret != 0)
            {
                throw new Exception($"Ghi giá trị vào PLC không thành công. Lỗi: {ret}");
            }
        }

        // Đóng kết nối
        public void CloseConnection()
        {
            //if (actUtl != null)
            //{
            //    actUtl.Close();
            //}
        }
    }

}
