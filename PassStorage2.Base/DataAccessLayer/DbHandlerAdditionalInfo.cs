using PassStorage2.ConfigurationProvider.Interfaces;
using PassStorage2.Logger.Interfaces;
using PassStorage2.Models;

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

namespace PassStorage2.Base.DataAccessLayer
{
    public class DbHandlerAdditionalInfo : DbHandlerExtended
    {
        public DbHandlerAdditionalInfo(ILogger logger, IConfigurationProvider configurationProvider) : base(logger, configurationProvider)
        {
            logger.FunctionStart();

            try
            {
                GenerateAdditionalInfoColumn();
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
            finally
            {
                logger.FunctionEnd();
            }
        }

        public override IEnumerable<Password> GetAll()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var list = new List<Password>();

            try
            {
                logger.FunctionStart();
                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("SELECT Id, Title, Login, Pass, SaveTime, PassChangeTime, ViewCount, Uid, AdditionalInfo FROM Password", connection);
                    logger.Debug($"Executing command in database - {command.CommandText}");
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
                            Uid = reader.GetString(7),
                            AdditionalInfo = reader.IsDBNull(8) ? null : reader.GetString(8),
                            ExpirationDays = configurationProvider.ExpirationDays
                        });
                    }

                    connection.Close();
                }

                return list;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
            finally
            {
                stopWatch.Stop();
                logger.Debug($"########### GetAll from DB ended in {stopWatch.ElapsedMilliseconds} ms ###########");
                logger.FunctionEnd();
            }
        }

        public override Password Get(int id)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                logger.FunctionStart();

                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand($"SELECT Id, Title, Login, Pass, SaveTime, PassChangeTime, ViewCount, Uid, AdditionalInfo FROM Password WHERE Id = {id}", connection);
                    logger.Debug($"Executing command in database - {command.CommandText}");
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
                        Uid = reader.GetString(7),
                        AdditionalInfo = reader.IsDBNull(8) ? null : reader.GetString(8),
                        ExpirationDays = configurationProvider.ExpirationDays
                    };
                    connection.Close();
                    return pass;
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
            finally
            {
                stopWatch.Stop();
                logger.Debug($"########### Get from DB ended in {stopWatch.ElapsedMilliseconds} ms ###########");
                logger.FunctionEnd();
            }
        }

        public override bool Save(Password pass, bool isPassUpdate)
        {
            try
            {
                logger.FunctionStart();

                string query;
                if (pass.Id == 0)
                {
                    query = $"INSERT INTO Password (Title, Login, Pass, SaveTime, PassChangeTime, ViewCount, Uid, AdditionalInfo) " +
                            $"VALUES ('{pass.Title}', '{pass.Login}', '{pass.Pass}', '{DateTime.Now:O}', '{DateTime.Now:O}', {pass.ViewCount}, '{pass.Uid}', '{pass.AdditionalInfo}')";
                }
                else
                {
                    string updTime = isPassUpdate ? $", PassChangeTime = '{DateTime.Now:O}'" : string.Empty;
                    query = $"UPDATE Password SET Title = '{pass.Title}', Login = '{pass.Login}', Pass = '{pass.Pass}', AdditionalInfo = '{pass.AdditionalInfo}', ViewCount = {pass.ViewCount} {updTime} WHERE Id = {pass.Id} AND Uid = '{pass.Uid}'";
                }

                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    logger.Debug($"Executing command in database - {query}");
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }
            finally
            {
                logger.FunctionEnd();
            }
        }

        protected void GenerateAdditionalInfoColumn()
        {
            if (!ColumnExists("Password", "AdditionalInfo"))
            {
                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("ALTER TABLE Password ADD COLUMN AdditionalInfo TEXT NULL;", connection);
                    logger.Debug("Executing command in database", command.CommandText);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
