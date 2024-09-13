using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static void WriteCSV(string path)
        {
            lock (lockWriteCSV)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, Path.GetFileName(path));

                string formattedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                bool fileExists = File.Exists(path);

                using (var writer = new StreamWriter(path, true, Encoding.UTF8))
                {
                    if (!fileExists)
                    {
                        string[] headers = { "Product Day", "Product Time" };
                        writer.WriteLine(string.Join(",", headers));
                    }

                    string[] data = {
                        formattedDateTime.Substring(0, 10),
                        formattedDateTime.Substring(11)
                    };

                    writer.WriteLine(string.Join(",", data));
                }
            }
        }

        //write excel
        public static void WriteExcel(string path)
        {
            lock (lockWriteExcel)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    path = Path.Combine(path, DateTime.Now.ToString("yyyyMMdd") + ".xlsx");

                    string formattedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    using (var package = new ExcelPackage(new FileInfo(path)))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet == null)
                        {
                            worksheet = package.Workbook.Worksheets.Add("Results");

                            worksheet.Cells["A1:X1"].Merge = true;
                            worksheet.Cells["A1"].Value = "We would like to request an update on items that will be added/deleted during actual writing.";
                            worksheet.Cells["T2:U2"].Merge = true;
                            worksheet.Cells["T2"].Value = "Air Leakage Test";

                            string[] headers = {
                                "Top Housing QRCode", "1st Glue Amount", "1st  Glue discharge volume Vision", "Insulator bar code",
                                "1st Glue overflow vision", "1st heated Air curing", "2nd Glue Amount", "2nd  Glue discharge volume Vision",
                                "FPCB bar code", "2nd Glue overflow vision", "2nd heated Air curing", "Distance", "3rd Glue Amount",
                                "3rd Glue discharge volume Vision", "3rd heated Air curing", "3rd Glue overflow vision",
                                "Tightness and location vision", "Height / Parallelism", "Resistance", "Air Leakage Test", "Air Leakage Test Result", "Product Day", "Product Time"
                            };

                            for (int i = 0; i < headers.Length; i++)
                            {
                                worksheet.Cells[2, i + 1].Value = headers[i];
                            }

                            package.Save();
                        }

                        int rowIndex = worksheet.Dimension?.Rows + 1 ?? 1;

                        worksheet.Cells[rowIndex, 1].Value = formattedDateTime.Substring(0, 10);
                        worksheet.Cells[rowIndex, 3].Value = formattedDateTime.Substring(11);

                        //if duplicate
                        //worksheet.Row(rowIndex).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //worksheet.Row(rowIndex).Style.Fill.BackgroundColor.SetColor(Color.Red);

                        package.Save();
                    }
                }
                catch (Exception ex)
                {
                    WriteLog($"Error can not save to file excel, error: {ex.Message}");
                }
            }
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

        public static void AutoDeleteFileLog()
        {
            Task.Run(async () =>
            {
                try
                {
                    int day = int.Parse(ConfigurationManager.AppSettings["DAY_AUTO_DELETE_FILE_LOG"]);
                    string AutoDelete = ConfigurationManager.AppSettings["AUTO_DELETE_FILE_LOG"];

                    while (true)
                    {
                        if (AutoDelete == "true")
                        {
                            DateTime now = DateTime.Now;
                            DeleteFileLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"), now, day);
                        }

                        await Task.Delay(TimeSpan.FromDays(1));
                    }
                }
                catch (Exception ex)
                {
                    Files.WriteLog($"Error can not delete file log, error: {ex.Message}");
                }
            });
        }

        private static void DeleteFileLog(string path, DateTime now, int dayDelete)
        {
            if (!Directory.Exists(path))
            {
                WriteLog($"Not found path to delete file!");
                return;
            }

            int batchSize = 1000;

            var fileBatch = Directory.EnumerateFiles(path).Take(batchSize);

            while (fileBatch.Any())
            {
                foreach (var file in fileBatch)
                {
                    DateTime creationTime = File.GetCreationTime(file);
                    TimeSpan fileAge = now - creationTime;

                    if (fileAge.TotalDays > dayDelete)
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch (Exception ex)
                        {
                            WriteLog($"Error can not delete file, error: {ex.Message}");
                        }
                    }
                }

                fileBatch = Directory.EnumerateFiles(path).Skip(batchSize).Take(batchSize);
            }

            var directories = Directory.GetDirectories(path);

            foreach (var directory in directories)
            {
                DeleteFileLog(directory, now, dayDelete);
            }
        }
    }
}
