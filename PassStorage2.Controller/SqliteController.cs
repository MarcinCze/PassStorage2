using PassStorage2.Base;
using PassStorage2.Base.DataAccessLayer;
using PassStorage2.ConfigurationProvider.Interfaces;
using PassStorage2.Models;
using PassStorage2.Translations;
using PassStorage2.Translations.Interfaces;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PassStorage2.Controller
{
    public class SqliteController : Interfaces.IController
    {
        private readonly DbHandlerExtended storage;
        private readonly Base.DataCryptoLayer.Interfaces.IDecodeData decoder;
        private readonly Base.DataCryptoLayer.Interfaces.IEncodeData encoder;
        private readonly ITranslationProvider translationProvider;
        private readonly IConfigurationProvider configurationProvider;

        protected string PasswordFirst { get; set; }
        protected string PasswordSecond { get; set; }

        public SqliteController()
        {
            Logger.Instance.Debug("Creating SqliteController");
            configurationProvider = new PassStorage2.ConfigurationProvider.ConfigurationProvider();
            storage = new DbHandlerExtended(configurationProvider); 
            decoder = new Base.DataCryptoLayer.Decoder();
            encoder = new Base.DataCryptoLayer.Encoder();
            
            translationProvider = new TranslationProvider();
            translationProvider.SetLanguage(GetLangEnum());
        }

        public void Backup()
        {
            Logger.Instance.FunctionStart();
            try
            {
                BackupHandler.Backup();
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

        public void BackupDecoded()
        {
            Logger.Instance.FunctionStart();
            try
            {
                BackupHandler.BackupDecoded(GetAll());
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

        public void Delete(int id)
        {
            Logger.Instance.FunctionStart();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                Logger.Instance.FunctionStart();
                storage.Delete(id);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
            }
            finally
            {
                stopWatch.Stop();
                Logger.Instance.Debug($"########### DELETE {stopWatch.ElapsedMilliseconds} ms ###########");
                Logger.Instance.FunctionEnd();
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
                Logger.Instance.Error(e);
                return null;
            }
            finally
            {
                stopWatch.Stop();
                Logger.Instance.Debug($"########### GET {stopWatch.ElapsedMilliseconds} ms ###########");
            }
        }

        public Password Get(int id, IEnumerable<Password> passwords)
        {
            throw new NotImplementedException();
        }

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
                Logger.Instance.Error(e);
                return null;
            }
            finally
            {
                stopWatch.Stop();
                Logger.Instance.Debug($"########### GET ALL {stopWatch.ElapsedMilliseconds} ms ###########");
            }
        }

        public IEnumerable<Password> GetAllExpired()
        {
            Logger.Instance.FunctionStart();
            try
            {
                return GetAllExpired(GetAll());
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        public IEnumerable<Password> GetAllExpired(IEnumerable<Password> passwords)
        {
            Logger.Instance.FunctionStart();
            try
            {
                return passwords.Where(x => x.IsExpired).OrderBy(x => x.Title);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        public IEnumerable<Password> GetMostUsed()
        {
            Logger.Instance.FunctionStart();
            try
            {
                return GetMostUsed(GetAll());
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        public IEnumerable<Password> GetMostUsed(IEnumerable<Password> passwords)
        {
            Logger.Instance.FunctionStart();
            try
            {
                var analyzer = new MostUsageAnalyzer(passwords);
                return analyzer.Analyze().OrderBy(x => x.Title);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        public void IncrementViewCount(int id)
        {
            Logger.Instance.FunctionStart();
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                storage.IncrementViewCount(id);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
            }
            finally
            {
                stopWatch.Stop();
                Logger.Instance.Debug($"########### IncrementViewCount ({stopWatch.ElapsedMilliseconds} ms) ###########");
                Logger.Instance.FunctionEnd();
            }
        }

        public void IncrementViewCount(int id, IEnumerable<Password> passwords)
        {
            throw new NotImplementedException();
        }

        public void Save(Password pass, bool updatePassTime)
        {
            Logger.Instance.FunctionStart();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                Logger.Instance.FunctionStart();
                storage.Save(encoder.Encode(pass, PasswordFirst, PasswordSecond), updatePassTime);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
            }
            finally
            {
                stopWatch.Stop();
                Logger.Instance.Debug($"########### SAVE {stopWatch.ElapsedMilliseconds} ms ###########");
                Logger.Instance.FunctionEnd();
            }
        }

        public bool SetPasswords(string primary, string secondary)
        {
            Logger.Instance.FunctionStart();
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
                        Logger.Instance.Error(new Exception("Passwords are incorrect"));
                        return false;
                    }

                    PasswordFirst = primary;
                    PasswordSecond = secondary;
                    Logger.Instance.Debug("Passwords ok");
                    return true;
                }
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

        public void UpdateViewCount(int id, int counter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Password> GetBySearchWord(string searchWord)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Password> GetBySearchWord(string searchWord, IEnumerable<Password> passwords)
        {
            Logger.Instance.FunctionStart();
            try
            {
                return passwords.Where(x => x.Title.ToUpper().Contains(searchWord.ToUpper())).OrderBy(x => x.Title);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        public string Translate(string key) => translationProvider.Translate(key);

        protected Language GetLangEnum() => Enum.TryParse(configurationProvider.ApplicationLanguage, out Language lang) ? lang : Language.EN;
    }
}
