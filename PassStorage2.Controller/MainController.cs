using System;
using System.Collections.Generic;
using System.Linq;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Models;
using PassStorage2.Base;

namespace PassStorage2.Controller
{
    public class MainController : IController
    {
        Base.DataAccessLayer.Interfaces.IStorage storage;
        Base.DataCryptoLayer.Interfaces.IDecodeData decoder;
        Base.DataCryptoLayer.Interfaces.IEncodeData encoder;

        public string PasswordFirst { get; set; }
        public string PasswordSecond { get; set; }

        public MainController()
        {
            storage = new Base.DataAccessLayer.FileHandler();
            decoder = new Base.DataCryptoLayer.Decoder();
            encoder = new Base.DataCryptoLayer.Encoder();
        }

        public IEnumerable<Password> GetAll()
        {
            try
            {
                return decoder.Decode(storage.Read());
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e.Message);
                return null;
            }
        }

        public Password Get(int id)
        {
            try
            {
                return decoder.Decode(storage.Read()).ToList().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e.Message);
                return null;
            }
        }

        public void Delete(int id)
        {
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
        }

        public void Save(Password pass)
        {
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
        }

        public void Backup()
        {
            throw new NotImplementedException();
        }

        public void BackupDecoded()
        {
            throw new NotImplementedException();
        }
    }
}
