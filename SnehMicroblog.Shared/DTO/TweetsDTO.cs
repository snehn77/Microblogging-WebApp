using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Shared
{
    public class TweetsDTO
    {
        public Guid MessageID { get; set; }

        public string Message { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid TweetID { get; set;  }

        public bool IsAuthor { get; set;  }

        public bool IsLiked { get; set;  }

        public string Image { get; set;  }
    }
}
