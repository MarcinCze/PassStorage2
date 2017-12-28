using System.Collections.Generic;

namespace PassStorage2.Models
{
    /// <summary>
    /// Header class
    /// </summary>
    public partial class Root
    {
        /// <summary>
        /// Computer name
        /// </summary>
        public string ComputerName { get; set; }
        /// <summary>
        /// Current Windows user name
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// File generation time
        /// </summary>
        public string SaveTime { get; set; }
        /// <summary>
        /// Version of the application
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// List of passwords
        /// </summary>
        public List<Password> Passwords { get; set; }
    }
}
