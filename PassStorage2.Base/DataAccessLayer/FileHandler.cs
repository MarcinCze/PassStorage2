using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PassStorage2.Base.DataAccessLayer.Interfaces;
using PassStorage2.Models;

namespace PassStorage2.Base.DataAccessLayer
{
    public class FileHandler : IStorage
    {
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
