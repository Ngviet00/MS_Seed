using MS_Seed.Common;
using System.Configuration;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace MS_Seed.Extensions.Printer
{
    public class ZebraZT411Printer
    {
        private static ZebraZT411Printer _instance;
        private static readonly object _lock = new object();
        private Connection _connection;
        private ZebraPrinter _printer;
        private readonly string IpAddress = ConfigurationManager.AppSettings["IP_PRINTER_ZEBRAZT411"];

        public static ZebraZT411Printer Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ZebraZT411Printer();
                    }

                    return _instance;
                }
            }
        }

        private bool Connect(string ipAddress)
        {
            _connection = new TcpConnection(ipAddress, TcpConnection.DEFAULT_ZPL_TCP_PORT);

            try
            {
                _connection.Open();
                _printer = ZebraPrinterFactory.GetInstance(_connection);
                return true;
            }
            catch (ConnectionException ex)
            {
                Global.ShowBoxError($"Could not connect to printer: {ex.Message}");
                return false;
            }
        }

        private void Disconnect()
        {
            try
            {
                _connection.Close();
            }
            catch (ConnectionException ex)
            {
                Global.ShowBoxError($"Error disconnecting from printer: {ex.Message}");
            }
        }

        public void Print(string label)
        {
            try
            {
                if (Connect(IpAddress))
                {
                    _printer.SendCommand(label);
                    Disconnect();
                }
            }
            catch (ConnectionException ex)
            {
                Global.ShowBoxError($"Error print label, error: {ex.Message}");
            }
        }
    }
}
