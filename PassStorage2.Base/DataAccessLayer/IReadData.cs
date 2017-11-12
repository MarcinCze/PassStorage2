using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassStorage2.Base.DataAccessLayer
{
    interface IReadData
    {
        IEnumerable<Models.Password> Read();
    }
}
