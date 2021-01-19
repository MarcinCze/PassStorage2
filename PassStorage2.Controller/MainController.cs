using PassStorage2.Base;
using PassStorage2.Base.DataAccessLayer;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PassStorage2.Controller
{
    [Obsolete("MainController is using file as a storage. It should not be used anymore.")]
    public class MainController : IController
    {
        private readonly FileHandler storage;
        private readonly Base.DataCryptoLayer.Interfaces.IDecodeData decoder;
        private readonly Base.DataCryptoLayer.Interfaces.IEncodeData encoder;

        protected string PasswordFirst { get; set; }
        protected string PasswordSecond { get; set; }
        protected IEnumerable<Password> Passwords { get; set; }

        public MainController()
        {
            Logger.Instance.Debug("Creating MainController");
            storage = new FileHandler();
            decoder = new Base.DataCryptoLayer.Decoder();
            encoder = new Base.DataCryptoLayer.Encoder();
        }

        public IEnumerable<Password> GetAll()
        {
            Logger.Instance.FunctionStart();
            Logger.Instance.Debug($"========= Get All start =========");
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                Passwords = decoder.Decode(storage.Read(), PasswordFirst, PasswordSecond).OrderBy(x => x.Title);
                Logger.Instance.Debug("Returning passwords");
                return Passwords;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            finally
            {
                stopWatch.Stop();
                Logger.Instance.Debug($"========= Get All finished in {stopWatch.ElapsedMilliseconds} ms =========");
                Logger.Instance.FunctionEnd();
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

        public Password Get(int id)
        {
            Logger.Instance.FunctionStart();
            try
            {
                return Get(id, GetAll());
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

        public Password Get(int id, IEnumerable<Password> passwords)
        {
            Logger.Instance.FunctionStart();
            try
            {
                return passwords.FirstOrDefault(x => x.Id == id);
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

        public void Delete(int id)
        {
            Logger.Instance.FunctionStart();
            try
            {
                var passwords = GetAll().ToList();
                passwords.Remove(passwords.First(x => x.Id == id));
                storage.Save(encoder.Encode(passwords, PasswordFirst, PasswordSecond));
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

        public void Save(Password pass)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Logger.Instance.FunctionStart();
            try
            {
                Logger.Instance.Debug("============ STARTING SAVING ============ ");
                var passwords = GetAll().ToList();

                if (pass.Id == 0)
                {
                    pass.SaveTime = DateTime.Now;
                    pass.PassChangeTime = DateTime.Now;
                    //pass.Id = Guid.NewGuid();
                    passwords.Add(pass);
                    storage.Save(encoder.Encode(passwords, PasswordFirst, PasswordSecond));
                    return;
                }

                var passInList = passwords.First(x => x.Id == pass.Id);
                passInList.Title = pass.Title;
                passInList.Login = pass.Login;
                passInList.ViewCount = pass.ViewCount;
                passInList.SaveTime = DateTime.Now;

                if (!pass.Pass.Equals(passInList.Pass))
                {
                    passInList.Pass = pass.Pass;
                    passInList.PassChangeTime = DateTime.Now;
                }

                storage.Save(encoder.Encode(passwords, PasswordFirst, PasswordSecond));
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
            }
            finally
            {
                stopWatch.Stop();
                Logger.Instance.Debug($"============ FINISHED SAVING ({stopWatch.ElapsedMilliseconds} ms) ============ ");
                Logger.Instance.FunctionEnd();
            }
        }

        public void Backup()
        {
            throw new NotImplementedException();
        }

        public void BackupDecoded()
        {
            throw new NotImplementedException();
        }

        public bool SetPasswords(string primary, string secondary) => throw new NotImplementedException();

        public void UpdateViewCount(int id, int counter)
        {
            Logger.Instance.FunctionStart();
            try
            {
                var password = Get(id);
                password.ViewCount = counter;
                Save(password);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                throw;
            }
        }

        public void IncrementViewCount(int id)
        {
            Logger.Instance.FunctionStart();
            try
            {
                IncrementViewCount(id, null);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                throw;
            }
        }

        public void IncrementViewCount(int id, IEnumerable<Password> passwords)
        {
            Logger.Instance.FunctionStart();
            Logger.Instance.Debug("=============== INCREMENT VIEW COUNT ===============");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            
            try
            {
                Password password = null;
                password = passwords == null ? Get(id) : Get(id, passwords);

                password.ViewCount++;
                Save(password);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                throw;
            }
            finally
            {
                stopWatch.Stop();
                Logger.Instance.Debug($"=============== INCREMENT VIEW COUNT FINISHED ({stopWatch.ElapsedMilliseconds} ms) ===============");
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
                return MostUsageAnalyzer.Get(passwords);
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

        public IEnumerable<Password> GetBySearchWord(string searchWord)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Password> GetBySearchWord(string searchWord, IEnumerable<Password> passwords)
        {
            throw new NotImplementedException();
        }

        public void Save(Password pass, bool updatePassTime)
        {
            throw new NotImplementedException();
        }

        public string Translate(string key)
        {
            throw new NotImplementedException();
        }
    }
}
