using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MS_Seed.Common
{
    public static class Files
    {
        private static readonly object lockWriteLog = new object();
        private static readonly object lockWriteLogCustomPath = new object();
        private static object lockWriteCSV = new object();
        private static object lockWriteFileTxt = new object();
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

        public static void WriteFileToTxt(string filePath, Dictionary<string, string> values)
        {
            lock (lockWriteFileTxt)
            {
                try
                {
                    var lines = File.ReadAllLines(filePath).ToList();
                    var keysToUpdate = values.Keys.ToList();

                    var updatedKeys = new HashSet<string>();

                    for (int i = 0; i < lines.Count; i++)
                    {
                        var parts = lines[i].Split(new[] { '=' }, 2);
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim();
                            if (values.ContainsKey(key))
                            {
                                lines[i] = $"{key}= {values[key]}";
                                updatedKeys.Add(key);
                            }
                        }
                    }

                    foreach (var key in keysToUpdate)
                    {
                        if (!updatedKeys.Contains(key))
                        {
                            lines.Add($"{key}= {values[key]}");
                        }
                    }

                    File.WriteAllLines(filePath, lines);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error can not write value to file txt: {ex.Message}");
                }
            }
        }

        public static Dictionary<string, string> ReadValueFileTxt(string filePath, List<string> keys)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');

                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();

                        if (keys.Contains(key))
                        {
                            values[key] = parts[1].Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error can not read value from file txt: {ex.Message}");
            }

            return values;
        }

        public static string GetFilePathSetting()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setting.txt");
        }

        //public static void ThreadAutoDeleteOldFile()
        //{
        //    Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int day = int.Parse(ConfigurationManager.AppSettings["DAY_AUTO_DELETE_OLD_FILE"]);
        //            string autoDelete = ConfigurationManager.AppSettings["AUTO_DELETE_OLD_FILE"];

        //            while (true)
        //            {
        //                if (autoDelete == "true")
        //                {
        //                    //DeleteFileLog();
        //                }

        //                await Task.Delay(TimeSpan.FromDays(1));
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    });
        //}

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
