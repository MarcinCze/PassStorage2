using PassStorage2.Base;
using PassStorage2.Base.DataAccessLayer.Interfaces;
using PassStorage2.Base.DataCryptoLayer.Interfaces;
using PassStorage2.ConfigurationProvider.Interfaces;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Logger.Interfaces;
using PassStorage2.Models;
using PassStorage2.Translations;
using PassStorage2.Translations.Interfaces;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PassStorage2.Controller
{
    public class SqliteController : IController
    {
        private readonly IStorageHandler storage;
        private readonly IDecodeData decoder;
        private readonly IEncodeData encoder;
        private readonly ITranslationProvider translationProvider;
        private readonly IConfigurationProvider configurationProvider;
        private readonly ILogger logger;

        protected string PasswordFirst { get; set; }
        protected string PasswordSecond { get; set; }

        public SqliteController(
            IStorageHandler storage,
            IDecodeData decoder,
            IEncodeData encoder,
            ITranslationProvider translationProvider,
            IConfigurationProvider configurationProvider,
            ILogger logger)
        {
            logger.Debug("Creating SqliteController");
            
            this.storage = storage;
            this.decoder = decoder;
            this.encoder = encoder;
            this.logger = logger;
            this.configurationProvider = configurationProvider;
            this.translationProvider = translationProvider;
            this.translationProvider.SetLanguage(GetLangEnum());
        }

        public void Backup()
        {
            logger.FunctionStart();
            try
            {
                BackupHandler.Backup();
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

        public void BackupDecoded()
        {
            logger.FunctionStart();
            try
            {
                BackupHandler.BackupDecoded(GetAll());
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

        public void Delete(int id)
        {
            logger.FunctionStart();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                logger.FunctionStart();
                storage.Delete(id);
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
            finally
            {
                stopWatch.Stop();
                logger.Debug($"########### DELETE {stopWatch.ElapsedMilliseconds} ms ###########");
                logger.FunctionEnd();
            }
        }

        public Password Get(int id)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                return decoder.Decode(storage.Get(id), PasswordFirst, PasswordSecond);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
            finally
            {
                stopWatch.Stop();
                logger.Debug($"########### GET {stopWatch.ElapsedMilliseconds} ms ###########");
            }
        }

        public Password Get(int id, IEnumerable<Password> passwords) => throw new NotImplementedException();

        public IEnumerable<Password> GetAll()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                return decoder.Decode(storage.GetAll(), PasswordFirst, PasswordSecond).OrderBy(x => x.Title);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
            finally
            {
                stopWatch.Stop();
                logger.Debug($"########### GET ALL {stopWatch.ElapsedMilliseconds} ms ###########");
            }
        }

        public IEnumerable<Password> GetAllExpired()
        {
            logger.FunctionStart();
            try
            {
                return GetAllExpired(GetAll());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
            finally
            {
                logger.FunctionEnd();
            }
        }

        public IEnumerable<Password> GetAllExpired(IEnumerable<Password> passwords)
        {
            logger.FunctionStart();
            try
            {
                return passwords.Where(x => x.IsExpired).OrderBy(x => x.Title);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
            finally
            {
                logger.FunctionEnd();
            }
        }

        public IEnumerable<Password> GetMostUsed()
        {
            logger.FunctionStart();
            try
            {
                return GetMostUsed(GetAll());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
            finally
            {
                logger.FunctionEnd();
            }
        }

        public IEnumerable<Password> GetMostUsed(IEnumerable<Password> passwords)
        {
            logger.FunctionStart();
            try
            {
                var results = MostUsageAnalyzer.Get(passwords);
                logger.Debug($"MOST USED VALS :: Min [{results.min}] :: Max [{results.max}] :: Border [{results.border}]");
                return results.passwords.OrderBy(x => x.Title);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
            finally
            {
                logger.FunctionEnd();
            }
        }

        public void IncrementViewCount(int id)
        {
            logger.FunctionStart();
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                storage.IncrementViewCount(id);
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
            finally
            {
                stopWatch.Stop();
                logger.Debug($"########### IncrementViewCount ({stopWatch.ElapsedMilliseconds} ms) ###########");
                logger.FunctionEnd();
            }
        }

        public void IncrementViewCount(int id, IEnumerable<Password> passwords)
        {
            throw new NotImplementedException();
        }

        public void Save(Password pass, bool updatePassTime)
        {
            logger.FunctionStart();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                logger.FunctionStart();
                storage.Save(encoder.Encode(pass, PasswordFirst, PasswordSecond), updatePassTime);
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
            finally
            {
                stopWatch.Stop();
                logger.Debug($"########### SAVE {stopWatch.ElapsedMilliseconds} ms ###########");
                logger.FunctionEnd();
            }
        }

        public bool SetPasswords(string primary, string secondary)
        {
            logger.FunctionStart();
            try
            {
                using (var protection = new Base.DataCryptoLayer.EntryProtection(
                    primary, 
                    secondary, 
                    configurationProvider.PrimaryHash, 
                    configurationProvider.SecondaryHash))
                {
                    if (!protection.IsAllowed)
                    {
                        logger.Error(new Exception("Passwords are incorrect"));
                        return false;
                    }

                    PasswordFirst = primary;
                    PasswordSecond = secondary;
                    logger.Debug("Passwords ok");
                    return true;
                }
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

        public void UpdateViewCount(int id, int counter) => throw new NotImplementedException();

        public IEnumerable<Password> GetBySearchWord(string searchWord) => throw new NotImplementedException();

        public IEnumerable<Password> GetBySearchWord(string searchWord, IEnumerable<Password> passwords)
        {
            logger.FunctionStart();
            try
            {
                return passwords.Where(x => x.Title.ToUpper().Contains(searchWord.ToUpper())).OrderBy(x => x.Title);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
            finally
            {
                logger.FunctionEnd();
            }
        }

        public string Translate(string key) => translationProvider.Translate(key);

        protected Language GetLangEnum() => Enum.TryParse(configurationProvider.ApplicationLanguage, out Language lang) ? lang : Language.EN;
    }
}
