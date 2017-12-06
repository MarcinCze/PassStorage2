using System;

namespace PassStorage2.Models
{
    public partial class Password
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public DateTime SaveTime { get; set; }
        public DateTime PassChangeTime { get; set; }
        public int ViewCount { get; set; }
    }
}
