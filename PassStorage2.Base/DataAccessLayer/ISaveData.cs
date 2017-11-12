using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassStorage2.Base.DataAccessLayer
{
    interface ISaveData
    {
        void Save(IEnumerable<Models.Password> passwords);
    }
}
