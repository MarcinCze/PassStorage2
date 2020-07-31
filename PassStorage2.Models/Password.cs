using System;

namespace PassStorage2.Models
{
    /// <summary>
    /// Password class
    /// </summary>
    public partial class Password
    {
        /// <summary>
        /// Default expiration days
        /// </summary>
        protected int ExpirationDays = Constants.ExpirationDays;
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Additional ID as Guid
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// Title of password
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Pass { get; set; }
        /// <summary>
        /// Save time in storage
        /// </summary>
        public DateTime SaveTime { get; set; }
        /// <summary>
        /// Last password change
        /// </summary>
        public DateTime PassChangeTime { get; set; }
        /// <summary>
        /// View count - how many times it was checked
        /// </summary>
        public int ViewCount { get; set; }
        /// <summary>
        /// Is password expired?
        /// </summary>
        public bool IsExpired => (DateTime.Now - PassChangeTime).TotalDays >= ExpirationDays;
    }
}
