using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MS_Seed.Common
{
    public static class Files
    {
        private static readonly object lockWriteLog = new object();
        private static readonly object lockWriteLogCustomPath = new object();
        private static object lockWriteCSV = new object();
        private static object lockWriteExcel = new object();

        //write log
        public static void WriteLog(string message)
        {
            lock (lockWriteLog)
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));

                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                string logFormat = DateTimeOffset.Now.ToString("yyyy_MM_dd HH:mm:ss:ff") + " ==> ";

                try
                {
                    using (StreamWriter writer = File.AppendText($@"{logPath}\{DateTime.Now.ToString("dd")}.txt"))
                    {
                        writer.WriteLine($"{logFormat}{message}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error can not write log, error: {ex.Message}");
                }
            }
        }

        public static void WriteLog(string path, string message)
        {
            lock (lockWriteLogCustomPath)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string logFormat = DateTimeOffset.Now.ToString("yyyy_MM_dd HH:mm:ss:ff") + " ==> ";

                try
                {
                    using (StreamWriter writer = File.AppendText($@"{path}\{DateTime.Now.ToString("dd")}.txt"))
                    {
                        writer.WriteLine($"{logFormat}{message}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error can not write log, error: {ex.Message}");
                }
            }
        }

        //write csv
        public static void WriteCSV()
        {

        }

        //write excel
        public static void WriteExcel()
        {

        }

        public static void ThreadAutoDeleteOldFile()
        {
            Task.Run(async () =>
            {
                try
                {
                    int day = int.Parse(ConfigurationManager.AppSettings["DAY_AUTO_DELETE_OLD_FILE"]);
                    string autoDelete = ConfigurationManager.AppSettings["AUTO_DELETE_OLD_FILE"];

                    while (true)
                    {
                        if (autoDelete == "true")
                        {
                            //DeleteFileLog();
                        }

                        await Task.Delay(TimeSpan.FromDays(1));
                    }
                }
                catch (Exception ex)
                {

                }
            });
        }

        //public static void DeleteFileLog(string path, DateTime now, int dayDelete)
        //{
        //    Task.Run(async () =>
        //    {

        //    });

        //    if (!Directory.Exists(path))
        //    {
        //        WriteLog($"Not found path to delete file!");
        //        return;
        //    }

        //    int batchSize = 1000;

        //    var fileBatch = Directory.EnumerateFiles(path).Take(batchSize);

        //    while (fileBatch.Any())
        //    {
        //        foreach (var file in fileBatch)
        //        {
        //            DateTime creationTime = File.GetCreationTime(file);
        //            TimeSpan fileAge = now - creationTime;
        //            if (fileAge.TotalDays > dayDelete)
        //            {
        //                try
        //                {
        //                    File.Delete(file);
        //                }
        //                catch (Exception ex)
        //                {
        //                    WriteLog($"Error can not delete file, error: {ex.Message}");
        //                }
        //            }
        //        }

        //        fileBatch = Directory.EnumerateFiles(path).Skip(batchSize).Take(batchSize);
        //    }

        //    var directories = Directory.GetDirectories(path);

        //    foreach (var directory in directories)
        //    {
        //        DeleteFileLog(directory, now, dayDelete);
        //    }
        //}
    }
}
