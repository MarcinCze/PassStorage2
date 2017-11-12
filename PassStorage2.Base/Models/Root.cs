using System.Collections.Generic;

namespace PassStorage2.Base.Models
{
    public partial class Root
    {
        public string ComputerName { get; set; }
        public string User { get; set; }
        public string SaveTime { get; set; }
        public string Version { get; set; }
        public List<Password> Passwords { get; set; }
    }
}
