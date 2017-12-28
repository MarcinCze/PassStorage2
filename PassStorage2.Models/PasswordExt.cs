using System;

namespace PassStorage2.Models
{
    public partial class Password
    {
        /// <summary>
        /// Is most used ? Value is not saved in memory
        /// </summary>
        [NonSerialized]
        public bool IsMostUsed;
    }
}
