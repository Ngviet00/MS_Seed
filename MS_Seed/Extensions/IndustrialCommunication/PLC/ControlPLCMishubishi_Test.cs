using ActUtlTypeLib;
using MS_Seed.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MS_Seed.Enums;

namespace MS_Seed.IndustrialCommunication.PLC
{
    public class ControlPLC : INotifyPropertyChanged
    {
        private ActUtlType actUtlType;
        private Thread readThread;
        private bool isReading;

        public int IndexPLC { get; private set; }
        public string Title { get; private set; }
        private object currentValue;

        public object CurrentValue
        {
            get => currentValue;
            private set
            {
                if (currentValue != value)
                {
                    currentValue = value;
                    OnPropertyChanged(nameof(CurrentValue));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ControlPLC(int indexPLC, string title)
        {
            actUtlType = new ActUtlType();
            IndexPLC = indexPLC;
            Title = title;
        }

        public bool ConnectPLC()
        {
            try
            {
                actUtlType.ActLogicalStationNumber = IndexPLC;

                if (actUtlType.Open() == 0)
                {
                    // Connection successful
                    isReading = true;
                    readThread = new Thread(ReadPLCData);
                    readThread.IsBackground = true;
                    readThread.Start();

                    Console.WriteLine($"Connected to PLC {IndexPLC}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to connect to PLC {IndexPLC}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to PLC {IndexPLC}: {ex.Message}");
                return false;
            }
        }

        public void DisconnectPLC()
        {
            try
            {
                isReading = false;
                if (readThread != null && readThread.IsAlive)
                {
                    readThread.Join();
                }

                if (actUtlType.Close() == 0)
                {
                    Console.WriteLine($"Disconnected from PLC {IndexPLC} successfully");
                }
                else
                {
                    Console.WriteLine($"Failed to disconnect from PLC {IndexPLC}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disconnecting from PLC {IndexPLC}: {ex.Message}");
            }
        }

        private void ReadPLCData()
        {
            while (true)
            {
                try
                {
                    int result;
                    int ret = actUtlType.GetDevice("M34300", out result);
                    CurrentValue = ret;

                    Thread.Sleep(20); // Thời gian delay giữa các lần đọc, có thể điều chỉnh phù hợp
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading from PLC {IndexPLC}: {ex.Message}");
                }
            }
        }
    }

}
