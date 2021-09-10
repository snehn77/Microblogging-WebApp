using SnehMicroblog.DAL;
using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Business
{
    public class TweetBusiness
    {
        private TweetDBContext tweetDBContext;
        private HashtagBusiness hashtagBusiness;
        private HashtagDBContext hashtagDBContext;

        public TweetBusiness()
        {
            tweetDBContext = new TweetDBContext();
            hashtagBusiness  = new HashtagBusiness();
            hashtagDBContext = new HashtagDBContext();
        }
        public TweetDTO CreateNewTweet(TweetDTO tweet)
        {
            TweetDTO newTweet = tweetDBContext.CreateNewTweet(tweet);
            if(newTweet != null)
            {
                bool result = hashtagBusiness.CreateNewHashtags(tweet);               
            }
            return newTweet;
        }

        public IList<TweetsDTO> GetAllTweets(Guid id)
        {
            IList<TweetsDTO> tweets = tweetDBContext.GetAllTweets(id);
            return tweets;
        }

        public bool DeleteTweet(Guid userId, Guid tweetId)
        {
            return tweetDBContext.DeleteTweet(userId, tweetId);
        }

        public bool UpdateTweet(TweetDTO tweet)
        {
            Guid result = tweetDBContext.UpdateTweet(tweet);
            hashtagDBContext.DeleteTag(result);
            hashtagBusiness.CreateNewHashtags(tweet);
            return true;            
        }

        public bool LikeTweet(LikeDTO likeTweet)
        {
            tweetDBContext.LikeTweets(likeTweet);
            return true;
        }

        public bool DislikeTweet(Guid userId, Guid tweetId)
        {
            tweetDBContext.DislikeTweets(userId , tweetId);
            return true;
        }
    }
}
