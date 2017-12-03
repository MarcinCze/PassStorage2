using System;
using System.Collections.Generic;
using PassStorage2.Controller.Interfaces;
using PassStorage2.Models;

namespace PassStorage2.Controller
{
    public class MainController : IController
    {
        Base.DataAccessLayer.Interfaces.IStorage storage;

        public MainController()
        {
            storage = new Base.DataAccessLayer.FileHandler();
        }

        public IEnumerable<Password> GetList()
        {
            throw new NotImplementedException();
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
