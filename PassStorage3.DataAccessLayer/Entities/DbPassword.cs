using System;

namespace PassStorage3.DataAccessLayer.Entities
{
    public class DbPassword
    {
        public int Id { get; set; }

        public string Uid { get; set; }
  
        public string Title { get; set; }

        public string Login { get; set; }

        public string Pass { get; set; }

        public DateTime SaveTime { get; set; }

        public DateTime PassChangeTime { get; set; }

        public int ViewCount { get; set; }

        public string AdditionalInfo { get; set; }
    }
}