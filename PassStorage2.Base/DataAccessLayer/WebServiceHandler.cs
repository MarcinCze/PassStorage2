using PassStorage2.Models;
using System;
using System.Collections.Generic;

namespace PassStorage2.Base.DataAccessLayer
{
    public class WebServiceHandler
    {
        public IEnumerable<Password> Read() => throw new NotImplementedException();

        public void Save(IEnumerable<Password> passwords) => throw new NotImplementedException();
    }
}
