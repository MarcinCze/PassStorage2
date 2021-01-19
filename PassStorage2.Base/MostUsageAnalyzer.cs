using PassStorage2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PassStorage2.Base
{
    public static class MostUsageAnalyzer
    {
        public static (IEnumerable<Password> passwords, int? min, int? max, int? border) Get(IEnumerable<Password> list)
        {
            try
            {
                var mostUsed = new List<Password>();

                if (!list.Any())
                {
                    return (mostUsed, null, null, null);
                }

                int max = list.Max(x => x.ViewCount);
                int min = list.Min(x => x.ViewCount);
                int border = (max - min) / 2 + min;

                mostUsed.AddRange(list.Where(pass => pass.ViewCount > border));
                return (mostUsed, min, max, border);
            }
            catch (Exception)
            {
                return (null, null, null, null);
            }
        }
    }
}
