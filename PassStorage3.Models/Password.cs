using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassStorage3.Models
{
    /// <summary>
    /// Password class
    /// </summary>
    public class Password
    {
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
        /// Additional comment
        /// </summary>
        public string AdditionalInfo { get; set; }
    }
}
