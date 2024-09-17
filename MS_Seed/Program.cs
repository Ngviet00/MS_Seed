using MS_Seed.Common;
using OfficeOpenXml;
using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;

namespace MS_Seed
{
    internal static class Program
    {
        public static Mutex mutex = null;

        [STAThread]
        static void Main()
        {
            string appName = ConfigurationManager.AppSettings["PROJECT_NAME"];

            mutex = new Mutex(true, appName, out bool createdNew);

            if (!createdNew)
            {
                MessageBox.Show("The program is running!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
                return;
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Files.AutoDeleteFileLog();
                Application.Run(new FormMain());
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            }
        }
    }
}
