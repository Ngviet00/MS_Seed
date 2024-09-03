using MS_Seed.IndustrialCommunication.Ethernet;
using MS_Seed.IndustrialCommunication.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MS_Seed
{
    public partial class FormMain : Form
    {
        private List<ControlPLC> controlPLCs;

        public FormMain()
        {
            InitializeComponent();

            //ControlSerialPort controlSerialPort = new ControlSerialPort();
            //controlSerialPort.Open();

            //SQLite.Instance.Connect();

            //ControlPLCMishubishi.Instance.ConnectPLC1(1);

            //ControlPLCMishubishi.Instance.ConnectPLC2(2);

            //ControlPLCMishubishi.Instance.ConnectToPLC(1);
            //ControlPLCMishubishi.Instance.ConnectToPLC(2);


            // Defined register of each plc then connect to PLC, ex: PLC_1: 1, PLC_2: 2
            ControlPLCMishubishi.Instance.ListDataTypePlcs[1].AddRange(new List<DataTypePLC>
            {
                new DataTypePLC { Register = "M34000", Title = "ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ_OR_WRITE.READ, ReadAlway = true },
                new DataTypePLC { Register = "M34300", Title = "TEST_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ_OR_WRITE.READ, ReadAlway = true },
                new DataTypePLC { Register = "M34100", Title = "WRITE_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ_OR_WRITE.WRITE },
            });

            ControlPLCMishubishi.Instance.ListDataTypePlcs[2].AddRange(new List<DataTypePLC>
            {
                new DataTypePLC { Register = "M34000", Title = "ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ_OR_WRITE.READ, ReadAlway = true },
                new DataTypePLC { Register = "M34300", Title = "TEST_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ_OR_WRITE.READ, ReadAlway = true },
                new DataTypePLC { Register = "M34100", Title = "WRITE_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ_OR_WRITE.WRITE },
            });

            ControlPLCMishubishi.Instance.ConnectPLC(1, ControlPLCMishubishi.Instance.ListDataTypePlcs[1], 1);
            ControlPLCMishubishi.Instance.ConnectPLC(2, ControlPLCMishubishi.Instance.ListDataTypePlcs[2], 2);
            ControlPLCMishubishi.Instance.PropertyChanged1 += Plc_PropertyChanged;
            ControlPLCMishubishi.Instance.PropertyChanged2 += Plc_PropertyChanged1;


            
            

            //foreach (var plc in ControlPLCMishubishi.Instance._plcDataList)
            //{
            //    plc.PropertyChanged += Plc_PropertyChanged;
            //}


            //ControlPLCMishubishi.Instance.PropertyChanged1 += Plc_PropertyChanged;

            //foreach (var plcData in ControlPLCMishubishi.Instance._plcDataList)
            //{
            //    plcData.PropertyChanged += (sender, e) =>
            //    {
            //        var obj = sender as PLCData;
            //        if (obj != null)
            //        {
            //            if (obj.Title == "ALIVE")
            //            {
            //                Console.WriteLine($"plc-{obj.IndexPLC}-title-{obj.Title}-current_value-{obj.CurrentValue}");
            //            }

            //            if (obj.Title == "TEST_ALIVE")
            //            {
            //                Console.WriteLine($"plc-{obj.IndexPLC}-title-{obj.Title}-current_value-{obj.CurrentValue}");
            //            }

            //            //Console.WriteLine($"PLC {data.IndexPLC} - {e.PropertyName} changed to: {data.CurrentValue} - {data.Title}");
            //        }
            //    };
            //}

            //ControlPLCMishubishi.Instance.WriteBit(1, "ALIVE", true);



            //ControlPLCMishubishi.Instance.ConnectPLC(2);

            //ControlPLCMishubishi.Instance.PropertyChanged += Plc_PropertyChanged;

        }

        private void Plc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var obj = ((ControlPLCMishubishi)sender);

            if (obj != null)
            {
                if (obj.Title == "ALIVE")
                {
                    Console.WriteLine($"plc-{obj.IndexPLC}-title-{obj.Title}-current_value-{obj.CurrentValue}");
                }
                else if (obj.Title == "TEST_ALIVE")
                {
                    Console.WriteLine($"plc-{obj.IndexPLC}-title-{obj.Title}-current_value-{obj.CurrentValue}");
                }
                //if (obj.Title == "TEST_ALIVE")
                //{
                //    Console.WriteLine($"plc-{obj.IndexPLC}-title-{obj.Title}-current_value-{obj.CurrentValue}");
                //}

            }

            //var plc = sender as ControlPLC;

            //if (e.PropertyName == nameof(ControlPLC.CurrentValue))
            //{
            //    Console.WriteLine($"PLC {plc.IndexPLC} ({plc.Title}) has new value: {plc.CurrentValue}");
            //    // Handle the new value
            //    //essageBox.Show($"PLC {plc.IndexPLC} ({plc.Title}) has new value: {plc.CurrentValue}");
            //}
        }

        private void Plc_PropertyChanged1(object sender, PropertyChangedEventArgs e)
        {
            var obj = ((ControlPLCMishubishi)sender);

            if (obj != null)
            {
                if (obj.Title == "ALIVE")
                {
                    Console.WriteLine($"plc-{obj.IndexPLC}-title-{obj.Title}-current_value-{obj.CurrentValue}");
                }
                else if (obj.Title == "TEST_ALIVE")
                {
                    Console.WriteLine($"plc-{obj.IndexPLC}-title-{obj.Title}-current_value-{obj.CurrentValue}");
                }
                //if (obj.Title == "TEST_ALIVE")
                //{
                //    Console.WriteLine($"plc-{obj.IndexPLC}-title-{obj.Title}-current_value-{obj.CurrentValue}");
                //}

            }

            //var plc = sender as ControlPLC;

            //if (e.PropertyName == nameof(ControlPLC.CurrentValue))
            //{
            //    Console.WriteLine($"PLC {plc.IndexPLC} ({plc.Title}) has new value: {plc.CurrentValue}");
            //    // Handle the new value
            //    //essageBox.Show($"PLC {plc.IndexPLC} ({plc.Title}) has new value: {plc.CurrentValue}");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ControlPLCMishubishi.Instance.WriteBit(1, "M34000", true);
            ControlPLCMishubishi.Instance.WriteBit(2, "M34000", true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ControlPLCMishubishi.Instance.WriteBit(2, "M34000", false);
            ControlPLCMishubishi.Instance.WriteBit(2, "M34000", false);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var item in ControlPLCMishubishi.Instance.ListActUtlType)
            {
                item?.Close();
                item?.Disconnect();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //ControlPLCMishubishi.Instance.ListDataTypePlcs[1].AddRange(new List<DataTypePLC>
            //{
            //    new DataTypePLC { Register = "M34000", Title = "ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ_OR_WRITE.READ, ReadAlway = true },
            //    new DataTypePLC { Register = "M34300", Title = "TEST_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ_OR_WRITE.READ, ReadAlway = true },
            //    new DataTypePLC { Register = "M34100", Title = "WRITE_ALIVE", TypePLC = TYPE_PLC.BIT, ReadOrWrite = READ_OR_WRITE.WRITE },
            //});

            //ControlPLCMishubishi.Instance.ConnectPLC(1, ControlPLCMishubishi.Instance.ListDataTypePlcs[1], 1);

            //foreach (var plc in ControlPLCMishubishi.Instance._plcDataList)
            //{
            //    plc.PropertyChanged += Plc_PropertyChanged;
            //}
        }
    }
}
