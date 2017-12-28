using PassStorage2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PassStorage2.Base
{
    public class MostUsageAnalyzer
    {
        protected readonly IEnumerable<Password> Passwords;

        public MostUsageAnalyzer (IEnumerable<Password> list)
        {
            Passwords = list;
        }

        public IEnumerable<Password> Analyze()
        {
            try
            {
                var mostUsed = new List<Password>();

                if (!Passwords.Any())
                {
                    return mostUsed;
                }

                int max = Passwords.Max(x => x.ViewCount);
                int min = Passwords.Min(x => x.ViewCount);
                int border = (max - min) / 2 + min;
                Logger.Instance.Debug($"MOST USED VALS :: Min [{min}] :: Max [{max}] :: Border [{border}]");

                mostUsed.AddRange(Passwords.Where(pass => pass.ViewCount > border));
                return mostUsed;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return null;
            }
        }
    }
}
