using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows;
using Dapper;
using MS_Seed.Classes;

namespace UtilCommon.SQL
{
    public class SQLite
    {
        private static SQLite _instance;
        private static readonly object _lock = new object();
        private SQLiteConnection _connection;
        private string ConnectionString = ConfigurationManager.AppSettings["CONNECTION_STRING_SQLITE"];
        private string projectName = ConfigurationManager.AppSettings["PROJECT_NAME"];

        public static SQLite Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SQLite();
                    }
                    return _instance;
                }
            }
        }

        public void Connect()
        {
            try
            {
                if (!string.IsNullOrEmpty(projectName) && !string.IsNullOrEmpty(ConnectionString))
                {
                    ConnectionString = ConnectionString.Replace("MS_Seed", projectName);
                }

                string dbFilePath = ConnectionString.Split('=')[1].Trim();

                string directoryPath = Path.GetDirectoryName(dbFilePath);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dbFilePath));
                }

                if (!File.Exists(dbFilePath))
                {
                    SQLiteConnection.CreateFile(dbFilePath);
                }

                _connection = new SQLiteConnection(ConnectionString);
                _connection.Open();

                ExecuteNonQuery("PRAGMA cache_size = -20000;");
                ExecuteNonQuery("PRAGMA journal_mode = WAL;");
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Error can not create file database, error: {ex.Message}");
                Global.ShowBoxError($"Error can not create file database, error: {ex.Message}");
            }
        }

        private void ExecuteNonQuery(string query)
        {
            using (var command = new SQLiteCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private void CreateTable(string query)
        {
            ExecuteNonQuery(query);
        }

        public bool Update(string table, string column, object value)
        {
            try
            {
                string query = $"UPDATE {table} SET {column} = @Value WHERE 1 = 1";
                _connection.Execute(query, new { Value = value });

                return true;
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Error can not update to database, error: {ex.Message}");
                return false;
            }
        }

        public bool Delete(string table, string column, object value)
        {
            try
            {
                string query = $"DELETE FROM {table} WHERE {column} = {value}";
                ExecuteNonQuery(query);

                return true;
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Error can not delete to database, error: {ex.Message}");
                return false;
            }
        }

        public bool ResetDatabase()
        {
            try
            {
                ExecuteNonQuery(@"DELETE FROM qrcodes");
                ExecuteNonQuery(@"vacuum");

                return true;
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Error can not reset database, error: {ex.Message}");
                return false;
            }
        }

        public bool Insert(string qrCode, string content, string createdAt)
        {
            try
            {
                string insertQuery = "INSERT INTO qrcodes (qr_code, content, created_at) VALUES (@qrCode, @content, @createdAt);";

                using (var transaction = _connection.BeginTransaction())
                {
                    _connection.Execute(insertQuery, new { qrCode, content, createdAt }, transaction);
                    transaction.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Error can not insert into database, error: {ex.Message}");
                return false;
            }
        }

        public List<ResultSearch> GetResultSearch(string qrCode)
        {
            List<ResultSearch> results = new List<ResultSearch>();

            try
            {
                string cmd = @"SELECT * FROM qrcodes WHERE qr_code = @qrCode";
                results = _connection.Query<ResultSearch>(cmd, new { qrCode }).ToList();

                return results;
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Error can not search result, error: {ex.Message}");
            }

            return null;
        }

        public Dictionary<string, string> GetDataLineChart()
        {
            try
            {
                string query = @"
                WITH RECURSIVE date_series AS (
                    SELECT DATE('now', '-6 days') AS day
                    UNION ALL
                    SELECT DATE(day, '+1 day')
                    FROM date_series
                    WHERE day < DATE('now')
                )
                SELECT 
                    ds.day,
                    IFNULL(COUNT(q.QR_Code), 0) AS total_count
                FROM date_series ds
                LEFT JOIN qrcodes q ON DATE(q.created_at) = ds.day
                GROUP BY ds.day
                ORDER BY ds.day ASC;";

                Dictionary<string, string> resultLineChart = new Dictionary<string, string>();

                using (var command = new SQLiteCommand(query, _connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultLineChart.Add(reader["day"].ToString(), reader["total_count"].ToString());
                        }
                    }
                }

                return resultLineChart;
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Error can not get data line chart, error: {ex.Message}");
                return null;
            }
        }

        public bool CheckDuplicateQrCode(string qrCode)
        {
            try
            {
                string cmd = @"
                    WITH RecentRecords AS (
                    SELECT qr_code
                    FROM qrcodes
                    ORDER BY created_at DESC
                    LIMIT 1000000
                )
                SELECT qr_code
                FROM RecentRecords
                WHERE qr_code = @qrCode limit 1";

                using (var command = new SQLiteCommand(cmd, _connection))
                {
                    command.Parameters.AddWithValue("@qrCode", qrCode);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Files.WriteLog($"Error check duplicate result, error: {ex.Message}");
                return false;
            }
        }
    }
}
