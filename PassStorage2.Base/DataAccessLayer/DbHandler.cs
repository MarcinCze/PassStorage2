using PassStorage2.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using PassStorage2.Base.DataAccessLayer.Interfaces;

namespace PassStorage2.Base.DataAccessLayer
{
    public class DbHandler : IStorageHandler
    {
        public const string FileName = "PassStorage2.Storage.sqlite";
        protected string ConnString => $"Data Source={FileName};Version=3;";

        public DbHandler()
        {
            Logger.Instance.FunctionStart();
            try
            {
                if (!File.Exists(FileName))
                {
                    Logger.Instance.Debug("SQLite file doesn't exist. Creating...");
                    SQLiteConnection.CreateFile(FileName);
                    GenerateTables();
                }
                else
                {
                    Logger.Instance.Debug("SQLite file exist");
                }
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        public virtual bool Save(Password pass, bool isPassUpdate)
        {
            try
            {
                Logger.Instance.FunctionStart();

                string query;
                if (pass.Id == 0)
                {
                    query = $"INSERT INTO Password (Title, Login, Pass, SaveTime, PassChangeTime, ViewCount) VALUES ('{pass.Title}', '{pass.Login}', '{pass.Pass}', '{DateTime.Now:O}', '{DateTime.Now:O}', {pass.ViewCount})";
                }
                else
                {
                    string updTime = isPassUpdate ? $", PassChangeTime = '{DateTime.Now:O}'" : string.Empty;
                    query = $"UPDATE Password SET Title = '{pass.Title}', Login = '{pass.Login}', Pass = '{pass.Pass}', ViewCount = {pass.ViewCount} {updTime} WHERE Id = {pass.Id}";
                }

                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    Logger.Instance.Debug($"Executing command in database - {query}");
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

        public virtual Password Get(int id)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                Logger.Instance.FunctionStart();

                string query = $"SELECT Id, Title, Login, Pass, SaveTime, PassChangeTime, ViewCount FROM Password WHERE Id = {id}";

                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    Logger.Instance.Debug($"Executing command in database - {query}");
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
                Logger.Instance.Debug($"########### Get from DB ended in {stopWatch.ElapsedMilliseconds} ms ###########");
                Logger.Instance.FunctionEnd();
            }
        }

        public virtual IEnumerable<Password> GetAll()
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
                    Logger.Instance.Debug($"Executing command in database - {query}");
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
                Logger.Instance.Debug($"########### GetAll from DB ended in {stopWatch.ElapsedMilliseconds} ms ###########");
                Logger.Instance.FunctionEnd();
            }
        }

        public virtual bool IncrementViewCount(int id)
        {
            try
            {
                Logger.Instance.FunctionStart();
                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand($"UPDATE Password SET ViewCount = ViewCount + 1 WHERE Id = {id}", connection);
                    Logger.Instance.Debug($"Executing command in database - {command.CommandText}");
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

        protected void GenerateTables()
        {
            const string query = @"
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
                Logger.Instance.Debug("Executing command in database", command);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
