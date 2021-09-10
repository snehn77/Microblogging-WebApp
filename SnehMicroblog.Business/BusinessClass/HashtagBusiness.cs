using SnehMicroblog.DAL;
using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Business
{
    public class HashtagBusiness
    {
        public bool CreateNewHashtags(TweetDTO tweet)
        {
            string[] tweets = tweet.Message.Split(' ');
            List<string> hashTagsElements = new List<string>();
            foreach(string tweetLocal in tweets)
            {
                if (tweetLocal.Contains("#"))
                {
                    hashTagsElements.Add(tweetLocal);
                }
            }

            using(HashtagDBContext hashTag = new HashtagDBContext())
            {
                bool response = hashTag.AddHashtages(hashTagsElements, tweet.TweetID);
                return true;
            }
        }
    }
}
