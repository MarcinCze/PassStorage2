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

        /// <summary>
        /// Get main password where one char sits on two positions plus index string
        /// </summary>
        /// <returns></returns>
        public (string password, string positions) GetPositionedString()
        {
            int index = 1;
            string password = string.Empty;
            string positions = string.Empty;

            foreach (var c in Pass)
            {
                password += $" {c}";

                switch (index)
                {
                    case 1:
                        positions += " 1";
                        break;
                    case 5:
                        positions += " 5";
                        break;
                    default:
                        positions += index % 5 == 0 ? $"{index}" : "  ";
                        break;
                }

                index++;
            }

            return (password, positions);
        }
    }
}
