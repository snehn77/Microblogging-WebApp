using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnehMicroblog.Presentation.Models
{
    public class TweetModel
    {
        public string UserID { get; set; }

        public string Message { get; set; }

        public Guid TweetID { get; set; }
    }
}