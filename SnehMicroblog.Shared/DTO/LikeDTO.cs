using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Shared
{
    public class LikeDTO
    {
        public Guid TweetID { get; set; }

        public Guid LoggedInUserID { get; set; }
    }
}
