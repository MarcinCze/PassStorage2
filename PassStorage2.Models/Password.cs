using System;

namespace PassStorage2.Models
{
    public partial class Password
    {
        protected const int expirationDays = 180;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public DateTime SaveTime { get; set; }
        public DateTime PassChangeTime { get; set; }
        public int ViewCount { get; set; }
        public bool IsExpired => (DateTime.Now - PassChangeTime).TotalDays >= expirationDays;
    }
}
