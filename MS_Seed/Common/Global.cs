using System;
using System.Globalization;
using System.Windows.Forms;

namespace MS_Seed.Common
{
    public static class Global
    {
        public static void ShowBoxError(string msg)
        {
            MessageBox.Show($"{msg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowBoxInfo(string msg)
        {
            MessageBox.Show($"{msg}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowBoxWarning(string msg)
        {
            MessageBox.Show($"{msg}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static string FormatNumber(int number) //format 1000 => 1.000
        {
            NumberFormatInfo customNumberFormat = new NumberFormatInfo()
            {
                NumberDecimalSeparator = ",",
                NumberGroupSeparator = "."
            };

            return number.ToString("#,##0", customNumberFormat);
        }

        public static void SetViewAction(Action action, Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
