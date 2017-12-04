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

        public string PasswordFirst { get; set; }
        public string PasswordSecond { get; set; }

        public MainController()
        {
            storage = new Base.DataAccessLayer.FileHandler();
        }

        public IEnumerable<Password> GetAll()
        {
            try
            {
                return storage.Read();
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
                return storage.Read().ToList().FirstOrDefault(x => x.Id == id);
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
                var passwords = storage.Read().ToList();
                passwords.Remove(passwords.First(x => x.Id == id));
                storage.Save(passwords);
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
                var passwords = storage.Read().ToList();
                passwords.Add(pass);
                storage.Save(passwords);
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
