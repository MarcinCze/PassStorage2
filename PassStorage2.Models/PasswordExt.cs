using System;

namespace PassStorage2.Models
{
    public partial class Password
    {
        [NonSerialized]
        public bool IsMostUsed;
    }
}
