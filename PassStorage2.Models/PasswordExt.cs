using System;
using System.Linq;

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

        public (string password, string positions) GetPositionedString()
        {
            string password = string.Empty;
            string positions = string.Empty;
            int index = 1;

            foreach (var c in Pass)
            {
                password += $" {c}";

                switch (index)
                {
                    case 1:
                        positions += " 1";
                        break;
                    case 5:
                        positions += $" 5";
                        break;
                    default:
                        positions += index % 5 == 0 ? index.ToString() : "  ";
                        break;
                }

                index++;
            }

            return (password, positions);
        }
    }
}
