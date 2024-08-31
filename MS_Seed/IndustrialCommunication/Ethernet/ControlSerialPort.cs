using MS_Seed.Common;
using System;
using System.Configuration;
using System.IO.Ports;
using System.Linq;

namespace MS_Seed.IndustrialCommunication.Ethernet
{
    public class ControlSerialPort
    {
        private readonly SerialPort serialPort;

        private readonly string portName = ConfigurationManager.AppSettings["PORT_NAME"] ?? "COM1";

        private readonly int baudRate = int.Parse(ConfigurationManager.AppSettings["BAUD_RATE"] ?? "9600");

        private readonly Enum parity = (Parity)Enum.Parse(typeof(Parity), ConfigurationManager.AppSettings["PARITY"] ?? "0");

        private readonly int dataBits = int.Parse(ConfigurationManager.AppSettings["DATA_BITS"] ?? "8");

        private readonly Enum stopBits = (StopBits)Enum.Parse(typeof(StopBits), ConfigurationManager.AppSettings["STOP_BITS"] ?? "1");

        public ControlSerialPort()
        {
            if (!PortExists(portName))
            {
                Global.ShowBoxError("COM port not exists!");
                return;
            }

            serialPort = new SerialPort(portName, baudRate, (Parity)parity, dataBits, (StopBits)stopBits);
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        public void Open()
        {
            if (serialPort != null)
            {
                if (!serialPort.IsOpen)
                {
                    try
                    {
                        serialPort.Open();
                    }
                    catch (Exception ex)
                    {
                        Global.ShowBoxError($"Error can not connect to serial port, err: {ex.Message}");
                    }
                }
            }
        }

        public void Close()
        {
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
        }

        public void SendData(string data)
        {
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                {
                    serialPort.WriteLine(data);
                }
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                string data = sp.ReadExisting();

                Console.WriteLine($"Data Received: {data}");
            }
            catch (Exception ex)
            {
                Global.ShowBoxError($"Error when receive data from serial port, err: {ex.Message}");
            }
        }

        private static bool PortExists(string portName)
        {
            return SerialPort.GetPortNames().Any(port => port.Equals(portName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
