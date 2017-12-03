using System;
using System.Collections.Generic;
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
                if (string.IsNullOrEmpty(PasswordFirst) || string.IsNullOrEmpty(PasswordSecond))
                    throw new Exception("Passwords are incorrect");

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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveAll()
        {
            throw new NotImplementedException();
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
