using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Shared
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Count { get; set; }
        public Guid ID { get; set; }
    }
}
