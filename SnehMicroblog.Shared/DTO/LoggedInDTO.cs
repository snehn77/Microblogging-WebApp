using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Shared
{
    public class LoggedInDTO
    {
        public Guid ID { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }
    }
}
