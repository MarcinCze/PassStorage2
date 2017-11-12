using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PassStorage2.Base.DataAccessLayer.Interfaces;
using PassStorage2.Base.Models;

namespace PassStorage2.Base.DataAccessLayer
{
    public class FileHandler : IReadData, ISaveData
    {
        public FileHandler() { }

        public IEnumerable<Password> Read()
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<Password> passwords)
        {
            throw new NotImplementedException();
        }
    }
}
