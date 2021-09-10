using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnehMicroblog.Shared
{
    public class SearchDTO
    {
        public string SearchString { get; set;  }

        public string Message { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid TweetID { get; set; }

        public Guid UserId { get; set; }

        public bool IsLiked { get; set; }

        public bool IsAuthor { get; set; }

        public bool IsFollowed { get; set; }

        public Guid UserID { get; set; }
    }
}