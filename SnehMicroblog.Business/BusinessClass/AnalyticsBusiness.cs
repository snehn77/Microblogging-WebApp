using SnehMicroblog.DAL;
using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Business
{
    public class AnalyticsBusiness
    {
        private TweetDBContext tweetDBContext;
        private UserDBContext userDBContext;

        public AnalyticsBusiness()
        {
            tweetDBContext = new TweetDBContext();
            userDBContext = new UserDBContext();
        }
        public AnalyticsDTO Analysis()
        {
            AnalyticsDTO analysis = new AnalyticsDTO()
            {
                MostTrending = tweetDBContext.MostTrending(),
                MostLiked = tweetDBContext.MostLiked(),
                TotalTweetsToday = tweetDBContext.TotalTweetsToday(),
                MostTweetsBy = userDBContext.MostTweetsBy()
            };
            return analysis;
        }
    }
}
