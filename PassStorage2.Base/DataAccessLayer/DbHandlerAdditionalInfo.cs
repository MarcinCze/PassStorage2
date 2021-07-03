using PassStorage2.ConfigurationProvider.Interfaces;
using PassStorage2.Logger.Interfaces;

using System;
using System.Data.SQLite;

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
