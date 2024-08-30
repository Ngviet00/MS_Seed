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
    }
}
