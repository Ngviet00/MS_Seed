using OfficeOpenXml;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MS_Seed
{
    internal static class Program
    {
        private static readonly Mutex mutex = new Mutex(true, "{b74f4e7a-7326-4b84-8d33-2f2d56bdf1f302052000}");

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("The program is running!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
