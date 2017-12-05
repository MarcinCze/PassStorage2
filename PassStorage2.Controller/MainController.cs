using System;
using System.Collections.Generic;
using System.Linq;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Models;
using PassStorage2.Base;
using System.Threading;

namespace PassStorage2.Controller
{
    public class MainController : IController
    {
        readonly Base.DataAccessLayer.Interfaces.IStorage storage;
        readonly Base.DataCryptoLayer.Interfaces.IDecodeData decoder;
        readonly Base.DataCryptoLayer.Interfaces.IEncodeData encoder;

        protected string PasswordFirst { get; set; }
        protected string PasswordSecond { get; set; }

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
            try
            {
                return decoder.Decode(storage.Read());
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

        public Password Get(int id)
        {
            Logger.Instance.FunctionStart();
            try
            {
                return decoder.Decode(storage.Read()).ToList().FirstOrDefault(x => x.Id == id);
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

        public void Delete(int id)
        {
            Logger.Instance.FunctionStart();
            try
            {
                var passwords = decoder.Decode(storage.Read()).ToList();
                passwords.Remove(passwords.First(x => x.Id == id));
                storage.Save(encoder.Encode(passwords));
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
            Logger.Instance.FunctionStart();
            try
            {
                var passwords = decoder.Decode(storage.Read()).ToList();
                passwords.Add(pass);
                storage.Save(encoder.Encode(passwords));
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
    }
}
