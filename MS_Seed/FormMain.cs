using Microsoft.Win32;
using MS_Seed.Extensions.IndustrialCommunication.PLC;
using MS_Seed.IndustrialCommunication.Ethernet;
using MS_Seed.IndustrialCommunication.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using static MS_Seed.Enums;

namespace MS_Seed
{
    public partial class FormMain : Form
    {
        private ControlPLCMishu plc1Controller;
        private ControlPLCMishu plc2Controller;

        private List<ControlPLCMishu> plcControllers;

        public FormMain()
        {
            InitializeComponent();

            plcControllers = new List<ControlPLCMishu>();

            for (int i = 1; i <= 2; i++) 
            {
                var plcController = ControlPLCMishu.GetInstance(i);

                if (!plcControllers.Contains(plcController))
                {
                    plcController.ConnectPLC();

                    ConfigurePLCRegisters(plcController, i);

                    foreach (var register in plcController.Registers)
                    {
                        register.PropertyChanged += Register_PropertyChanged;
                    }

                    plcControllers.Add(plcController);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Register_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var register = sender as Register;

            if (e.PropertyName == "CurrentValue")
            {
                Console.WriteLine($"PLC {register.PlcIndex}: {register.Title} changed to {register.CurrentValue}");
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //plc1Controller.StartReading();
            //plc2Controller.StartReading();

            foreach (var plcController in plcControllers)
            {
                plcController.StartReading();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var plcController in plcControllers)
            {
                plcController.DisconnectPLC();
            }
        }

        private void ConfigurePLCRegisters(ControlPLCMishu plcController, int plcIndex)
        {
            plcController.ConfigureRegisters(
                new Register { Title = $"PLC{plcIndex} Register 1", Address = "M34000", PlcIndex = plcIndex },
                new Register { Title = $"PLC{plcIndex} Register 2", Address = "M34001", PlcIndex = plcIndex }
            // Thêm nhiều thanh ghi nếu cần
            );
        }
    }
}
