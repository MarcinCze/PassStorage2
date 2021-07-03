using PassStorage2.ConfigurationProvider.Interfaces;
using PassStorage2.Logger.Interfaces;
using PassStorage2.Models;

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PassStorage2.Base.DataAccessLayer
{
    public class DbHandlerExtended : DbHandler
    {
        protected readonly IConfigurationProvider configurationProvider;

        public DbHandlerExtended(ILogger logger, IConfigurationProvider configurationProvider) : base(logger)
        {
            logger.FunctionStart();
            this.configurationProvider = configurationProvider;
            try
            {
                GenerateAdditionalStructure();
                FillMissingUid();
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
                    var command = new SQLiteCommand("SELECT Id, Title, Login, Pass, SaveTime, PassChangeTime, ViewCount, Uid FROM Password", connection);
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
                    var command = new SQLiteCommand($"SELECT Id, Title, Login, Pass, SaveTime, PassChangeTime, ViewCount, Uid FROM Password WHERE Id = {id}", connection);
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
                    query = $"INSERT INTO Password (Title, Login, Pass, SaveTime, PassChangeTime, ViewCount, Uid) " +
                            $"VALUES ('{pass.Title}', '{pass.Login}', '{pass.Pass}', '{DateTime.Now:O}', '{DateTime.Now:O}', {pass.ViewCount}, '{pass.Uid}')";
                }
                else
                {
                    string updTime = isPassUpdate ? $", PassChangeTime = '{DateTime.Now:O}'" : string.Empty;
                    query = $"UPDATE Password SET Title = '{pass.Title}', Login = '{pass.Login}', Pass = '{pass.Pass}', ViewCount = {pass.ViewCount} {updTime} WHERE Id = {pass.Id} AND Uid = '{pass.Uid}'";
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

        public override void Delete(int id)
        {
            try
            {
                logger.FunctionStart();

                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand($"DELETE FROM Password WHERE Id = {id}", connection);
                    logger.Debug($"Executing command in database - {command.CommandText}");
                    command.ExecuteNonQuery();
                    connection.Close();
                }
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

        protected void GenerateAdditionalStructure()
        {
            if (!ColumnExists("Password", "Uid"))
            {
                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("ALTER TABLE Password ADD COLUMN Uid TEXT NULL;", connection);
                    logger.Debug("Executing command in database", command.CommandText);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            if (!TableExists("Tag"))
            {
                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(@"
                        CREATE TABLE Tag (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,
                                Name TEXT NOT NULL)
                        ", connection);
                    logger.Debug("Executing command in database", command.CommandText);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            if (!TableExists("PasswordTag"))
            {
                using (var connection = new SQLiteConnection(ConnString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(@"
                        CREATE TABLE PasswordTag (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,
                                TagId INTEGER NOT NULL, 
                                PasswordId INTEGER NOT NULL,
                                FOREIGN KEY(TagId) REFERENCES Tag(Id),
                                FOREIGN KEY(PasswordId) REFERENCES Password(Id)
                        )", connection);
                    logger.Debug("Executing command in database", command.CommandText);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        protected bool TableExists(string tableName)
        {
            using (var connection = new SQLiteConnection(ConnString))
            {
                connection.Open();
                var command = new SQLiteCommand($"SELECT count(*) FROM sqlite_master WHERE type='table' and name = '{tableName}'", connection);
                logger.Debug("Executing command in database", command.CommandText);
                var response = int.Parse(command.ExecuteScalar().ToString());
                connection.Close();
                return response == 1;
            }
        }

        protected bool ColumnExists(string tableName, string columnName)
        {
            using (var connection = new SQLiteConnection(ConnString))
            {
                connection.Open();
                var command = new SQLiteCommand($"SELECT COUNT(*) FROM pragma_table_info('{tableName}') WHERE name='{columnName}'", connection);
                logger.Debug("Executing command in database", command.CommandText);
                var response = int.Parse(command.ExecuteScalar().ToString());
                connection.Close();
                return response == 1;
            }
        }

        protected void FillMissingUid()
        {
            using (var connection = new SQLiteConnection(ConnString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Id FROM Password WHERE Uid IS NULL", connection);
                logger.Debug($"Executing command in database - {command.CommandText}");
                var reader = command.ExecuteReader();

                var list = new List<int>();
                while (reader.Read())
                    list.Add(reader.GetInt32(0));

                if (!list.Any())
                {
                    connection.Close();
                    return;
                }

                var updateQuery = new StringBuilder();
                foreach (var id in list)
                    updateQuery.AppendLine($"UPDATE Password SET Uid = '{Guid.NewGuid()}' WHERE Id = {id};");
                var updateCommand = new SQLiteCommand(updateQuery.ToString(), connection);
                logger.Debug($"Executing command in database - {updateCommand.CommandText}");
                updateCommand.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
