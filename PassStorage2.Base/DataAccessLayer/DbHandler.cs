using PassStorage2.Base.DataAccessLayer.Interfaces;
using PassStorage2.Base.Models;
using System;
using System.Collections.Generic;

namespace PassStorage2.Base.DataAccessLayer
{
    public class DbHandler : IReadData, ISaveData
    {
        public IEnumerable<Password> Read() => throw new NotImplementedException();

        public void Save(IEnumerable<Password> passwords) => throw new NotImplementedException();
    }
}
