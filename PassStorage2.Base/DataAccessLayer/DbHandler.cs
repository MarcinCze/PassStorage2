using PassStorage2.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace PassStorage2.Base.DataAccessLayer
{
    public class DbHandler
    {
        protected const string FileName = "PassStorage2.Storage.sqlite";
        protected string ConnString => $"Data Source={FileName};Version=3;";

        public DbHandler()
        {
            if (!File.Exists(FileName))
            {
                SQLiteConnection.CreateFile(FileName);
                GenerateTables();
            }
        }

        protected void GenerateTables()
        {
            string query = @"
                             CREATE TABLE Password (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,
                                Title TEXT NOT NULL,
                                Login TEXT NOT NULL,
                                Pass TEXT NOT NULL,
                                SaveTime TEXT,
                                PassChangeTime TEXT,
                                ViewCount INTEGER )
                            ";

            using (var connection = new SQLiteConnection(ConnString))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool Save(Password pass, bool isPassUpdate)
        {
            try
            {
                Logger.Instance.FunctionStart();

                string query;
                if (pass.Id == 0)
                {
                    query = $"INSERT INTO Password (Title, Login, Pass, SaveTime, PassChangeTime, ViewCount) VALUES ('{pass.Title}', '{pass.Login}', '{pass.Pass}', '{DateTime.Now.ToString("O")}', '{DateTime.Now.ToString("O")}', {pass.ViewCount})";
                }
                else
                {
                    string updTime = isPassUpdate ? $", PassChangeTime = '{DateTime.Now.ToString("O")}'" : string.Empty;
                    query = $"UPDATE Password SET Title = '{pass.Title}', Login = '{pass.Login}', Pass = '{pass.Pass}', ViewCount = {pass.ViewCount} {updTime} WHERE Id = {pass.Id}";
                }

                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return false;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        public Password Get(int id)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                Logger.Instance.FunctionStart();

                var list = new List<Password>();
                string query = $"SELECT Id, Title, Login, Pass, SaveTime, PassChangeTime, ViewCount FROM Password WHERE Id = {id}";

                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    var pass = new Password
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Login = reader.GetString(2),
                        Pass = reader.GetString(3),
                        SaveTime = reader.GetDateTime(4),
                        PassChangeTime = reader.GetDateTime(5),
                        ViewCount = reader.GetInt32(6),
                    };
                    connection.Close();
                    return pass;
                }
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            finally
            {
                stopWatch.Stop();
                Logger.Instance.Debug($"**** Get from DB ended in {stopWatch.ElapsedMilliseconds} ms");
                Logger.Instance.FunctionEnd();
            }
        }

        public IEnumerable<Password> GetAll()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                Logger.Instance.FunctionStart();
                
                var list = new List<Password>();
                string query = "SELECT Id, Title, Login, Pass, SaveTime, PassChangeTime, ViewCount FROM Password";

                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    var reader = command.ExecuteReader();
                
                    while (reader.Read())
                    {
                        list.Add(new Password
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Login = reader.GetString(2),
                            Pass = reader.GetString(3),
                            SaveTime = reader.GetDateTime(4),
                            PassChangeTime = reader.GetDateTime(5),
                            ViewCount = reader.GetInt32(6),
                        });
                    }

                    connection.Close();
                }

                return list;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            finally
            {
                stopWatch.Stop();
                Logger.Instance.Debug($"**** GetAll from DB ended in {stopWatch.ElapsedMilliseconds} ms");
                Logger.Instance.FunctionEnd();
            }
        }

        public bool IncrementViewCount(int id)
        {
            try
            {
                Logger.Instance.FunctionStart();

                string query = $"UPDATE Password SET ViewCount = ViewCount + 1 WHERE Id = {id}";

                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return false;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }
    }
}
