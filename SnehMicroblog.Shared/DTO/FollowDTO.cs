using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Shared
{
    public class FollowDTO
    {
        public Guid UserID { get; set; }

        public Guid UserToFollowID { get; set; }
    }
}
