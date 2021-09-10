using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Shared
{
    public class TweetDTO
    {
        public Guid UserID { get; set; }

        public string Message { get; set; }

        public Guid TweetID { get; set; }
    }
}
