using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassStorage3.Models
{
    public class Password
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

        public Dictionary<string, bool> EncodedPropertyStatuses { get; set; }

        public Password()
        {
            EncodedPropertyStatuses = new Dictionary<string, bool>
            {
                { nameof(Title), false },
                { nameof(Login), false },
                { nameof(Pass), false },
                { nameof(AdditionalInfo), false },
            };
        }
    }
}
