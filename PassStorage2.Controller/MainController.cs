using PassStorage2.Base;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PassStorage2.Controller
{
    public class MainController : IController
    {
        readonly Base.DataAccessLayer.Interfaces.IStorage storage;
        readonly Base.DataCryptoLayer.Interfaces.IDecodeData decoder;
        readonly Base.DataCryptoLayer.Interfaces.IEncodeData encoder;

        protected string PasswordFirst { get; set; }
        protected string PasswordSecond { get; set; }
        protected IEnumerable<Password> Passwords { get; set; }

        public MainController()
        {
            Logger.Instance.Debug("Creating MainController");
            storage = new Base.DataAccessLayer.FileHandler();
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
                Logger.Instance.Error(e.Message);
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
                Logger.Instance.Error(e.Message);
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
                Logger.Instance.Error(e.Message);
                return null;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        public Password Get(Guid id)
        {
            Logger.Instance.FunctionStart();
            try
            {
                return Get(id, GetAll());
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e.Message);
                return null;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        public Password Get(Guid id, IEnumerable<Password> passwords)
        {
            Logger.Instance.FunctionStart();
            try
            {
                return passwords.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e.Message);
                return null;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }

        public void Delete(Guid id)
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

                if (pass.Id == null)
                {
                    pass.SaveTime = DateTime.Now;
                    pass.PassChangeTime = DateTime.Now;
                    pass.Id = Guid.NewGuid();
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

        public bool SetPasswords(string primary, string secondary)
        {
            Logger.Instance.FunctionStart();
            try
            {
                using (var protection = new Base.DataCryptoLayer.EntryProtection(primary, secondary))
                {
                    protection.Validate();

                    if (!protection.IsAllowed)
                    {
                        Logger.Instance.Error("Passwords are incorrect");
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

        public void UpdateViewCount(Guid id, int counter)
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

        public void IncrementViewCount(Guid id)
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

        public void IncrementViewCount(Guid id, IEnumerable<Password> passwords)
        {
            Logger.Instance.FunctionStart();
            Logger.Instance.Debug("=============== INCREMENT VIEW COUNT ===============");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            
            try
            {
                Password password = null;
                if (passwords == null)
                {
                    password = Get(id);
                }
                else
                {
                    password = Get(id, passwords);
                }

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
                Logger.Instance.Error(e.Message);
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
                return analyzer.Analyze();
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e.Message);
                return null;
            }
            finally
            {
                Logger.Instance.FunctionEnd();
            }
        }
    }
}
