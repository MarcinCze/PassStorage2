using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PassStorage2.Base;
using PassStorage2.Base.DataAccessLayer;
using PassStorage2.Models;

namespace PassStorage2.Controller
{
    public class SqliteController : Interfaces.IController
    {
        readonly DbHandler storage;
        readonly Base.DataCryptoLayer.Interfaces.IDecodeData decoder;
        readonly Base.DataCryptoLayer.Interfaces.IEncodeData encoder;

        protected string PasswordFirst { get; set; }
        protected string PasswordSecond { get; set; }
        protected IEnumerable<Password> Passwords { get; set; }

        public SqliteController()
        {
            Logger.Instance.Debug("Creating SqliteController");
            storage = new DbHandler();
            decoder = new Base.DataCryptoLayer.Decoder();
            encoder = new Base.DataCryptoLayer.Encoder();
        }

        public void Backup()
        {
            throw new NotImplementedException();
        }

        public void BackupDecoded()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Password Get(int id)
        {
            throw new NotImplementedException();
        }

        public Password Get(int id, IEnumerable<Password> passwords)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Password> GetAll()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Logger.Instance.Debug($"########### GET ALL ###########");
            try
            {
                return decoder.Decode(storage.GetAll(), PasswordFirst, PasswordSecond);
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
            throw new NotImplementedException();
        }

        public IEnumerable<Password> GetAllExpired(IEnumerable<Password> passwords)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Password> GetMostUsed()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Password> GetMostUsed(IEnumerable<Password> passwords)
        {
            throw new NotImplementedException();
        }

        public void IncrementViewCount(int id)
        {
            throw new NotImplementedException();
        }

        public void IncrementViewCount(int id, IEnumerable<Password> passwords)
        {
            throw new NotImplementedException();
        }

        public void Save(Password pass)
        {
            try
            {
                Logger.Instance.FunctionStart();
                storage.Save(encoder.Encode(pass, PasswordFirst, PasswordSecond));
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

        public void UpdateViewCount(int id, int counter)
        {
            throw new NotImplementedException();
        }
    }
}
