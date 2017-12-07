using PassStorage2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassStorage2.Base
{
    public class MostUsageAnalyzer
    {
        protected IEnumerable<Password> passwords;

        public MostUsageAnalyzer (IEnumerable<Password> list)
        {
            passwords = list;
        }

        public IEnumerable<Password> Analyze()
        {
            var mostUsed = new List<Password>();

            foreach (var pass in passwords)
            {
                if (pass.ViewCount > 0)
                    mostUsed.Add(pass);
            }
            return mostUsed;
        }
    }
}
