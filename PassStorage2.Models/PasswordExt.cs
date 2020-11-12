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

        /// <summary>
        /// Default expiration days
        /// </summary>
        public int ExpirationDays = 365;

        /// <summary>
        /// Is password expired?
        /// </summary>
        public bool IsExpired => (DateTime.Now - PassChangeTime).TotalDays >= ExpirationDays;
    }
}
