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
        protected readonly IEnumerable<Password> passwords;

        public MostUsageAnalyzer (IEnumerable<Password> list)
        {
            passwords = list;
        }

        public IEnumerable<Password> Analyze()
        {
            var mostUsed = new List<Password>();

            int max = passwords.Max(x => x.ViewCount);
            int min = passwords.Min(x => x.ViewCount);
            int border = (max - min) / 2 + min;

            foreach (var pass in passwords)
            {
                if (pass.ViewCount > border) mostUsed.Add(pass);
            }
            return mostUsed;
        }
    }
}
