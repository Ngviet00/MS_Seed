using OfficeOpenXml;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MS_Seed
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Process[] NetClassClient = Process.GetProcessesByName(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

            if (NetClassClient.Length > 1)
            {
                MessageBox.Show("The program is running!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            }
        }
    }
}
