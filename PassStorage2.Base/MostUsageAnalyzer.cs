using PassStorage2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PassStorage2.Base
{
    public static class MostUsageAnalyzer
    {
        public static IEnumerable<Password> Get(IEnumerable<Password> list)
        {
            try
            {
                var mostUsed = new List<Password>();

                if (!list.Any())
                {
                    return mostUsed;
                }

                int max = list.Max(x => x.ViewCount);
                int min = list.Min(x => x.ViewCount);
                int border = (max - min) / 2 + min;
                Logger.Instance.Debug($"MOST USED VALS :: Min [{min}] :: Max [{max}] :: Border [{border}]");

                mostUsed.AddRange(list.Where(pass => pass.ViewCount > border));
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
