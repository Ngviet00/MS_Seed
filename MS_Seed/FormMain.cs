using MS_Seed.Forms;
using MS_Seed.IndustrialCommunication.Ethernet;
using System;
using System.Windows.Forms;

namespace MS_Seed
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void pictureBtnSetting_Click(object sender, EventArgs e)
        {
            FormSetting fs = new FormSetting();
            fs.ShowDialog();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dddd, MMM dd, yyyy | HH:mm:ss");
        }

        private void picturePLC_Click(object sender, EventArgs e)
        {
            FormTestPLC formPLC = new FormTestPLC();
            formPLC.ShowDialog();
        } 
    }
}
