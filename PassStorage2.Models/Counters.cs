using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassStorage2.Models
{
    public class Counters
    {
        public int All { get; set; }
        public int MostUsed { get; set; }
        public int Expired { get; set; }

        public Counters (int a, int m, int e)
        {
            All = a;
            MostUsed = m;
            Expired = e;
        }

        public bool NeedRefresh => All == 0 && MostUsed == 0 && Expired == 0;
    }
}
