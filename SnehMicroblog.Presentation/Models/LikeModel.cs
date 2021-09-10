using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnehMicroblog.Presentation.Models
{
    public class LikeModel
    {
        public string TweetID { get; set; }

        public string LoggedInUserID { get; set; }
    }
}