using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.DAL
{
    public class TweetDBContext : IDisposable
    {
        snehEntities DBContext;
        ObjectConvertor objConvertor;
        HashtagDBContext hashTagDBContext;
        public TweetDBContext()
        {
            DBContext = new snehEntities();
            objConvertor = new ObjectConvertor();
            hashTagDBContext = new HashtagDBContext();
        }

        public TweetDTO CreateNewTweet(TweetDTO tweet)
        {
            Tweet newTweet = objConvertor.NewTweet(tweet);
            DBContext.Tweets.Add(newTweet);
            DBContext.SaveChangesAsync();
            tweet.TweetID = newTweet.ID;
            return tweet;
        }

        public IList<TweetsDTO> GetAllTweets(Guid ID)
        {
            IList<TweetsDTO> tweets = new List<TweetsDTO>();
            IList<TweetsDTO> tweets1 = new List<TweetsDTO>();

            tweets = (from x in DBContext.Follows.Where(x=>x.Follower_UserID == ID) join 
                      y in DBContext.Follows on x.Followed_UserID equals y.Followed_UserID join
                      z in DBContext.Tweets on y.Followed_UserID equals z.UserID join 
                      user in DBContext.Users on z.UserID equals user.ID
                      orderby z.CreatedAt descending
                      select new TweetsDTO()
                      {
                          MessageID = z.ID,
                          Message = z.Message,
                          CreatedAt = z.CreatedAt,
                          Username = user.FirstName + " " +user.LastName,
                          IsAuthor = false,
                          TweetID = z.ID,
                          Image = user.Image
                      }).Distinct().ToList();

            foreach(var item in tweets)
            {
                LikeTweet likeTweetLocal = DBContext.LikeTweets.Where(x => (x.UserID == ID) && (x.TweetID == item.TweetID)).FirstOrDefault();
                if (likeTweetLocal != null)
                {
                    item.IsLiked = true;
                }
                else item.IsLiked = false;
            }

            tweets1 = (from x in DBContext.Users.Where(x=>x.ID == ID) join
                       y in DBContext.Tweets on x.ID equals y.UserID
                       orderby y.CreatedAt descending
                       select new TweetsDTO()
                       {
                           MessageID = y.ID,
                           Message = y.Message,
                           CreatedAt = y.CreatedAt,
                           Username = x.FirstName + " " +x.LastName,
                           IsAuthor = true ,
                           IsLiked = false,
                           TweetID = y.ID,
                           Image = x.Image
                       }).ToList();
            tweets = tweets.Concat(tweets1).ToList();
            IList<TweetsDTO> tweetsList = tweets.OrderByDescending(x => x.CreatedAt).ToList();
            return tweetsList;
        }

        public bool DeleteTweet(Guid userId , Guid tweetId)
        {
            Tweet tweet = DBContext.Tweets.Where(x => x.ID == tweetId && x.UserID == userId).FirstOrDefault();
            if(tweet != null)
            {
                hashTagDBContext.DeleteTag(tweet.ID);
                DBContext.Entry(tweet).State = EntityState.Deleted;
                LikeTweet likeTweet = new LikeTweet();
                likeTweet = DBContext.LikeTweets.Where(x => x.TweetID == tweetId).FirstOrDefault();
                if (likeTweet != null)
                {
                    foreach(var id in DBContext.LikeTweets)
                    {
                        if(id.TweetID == likeTweet.TweetID)
                        {
                            DBContext.LikeTweets.Remove(id);
                        }
                    }                    
                }
                DBContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Guid UpdateTweet(TweetDTO editedTweet)
        {
            Tweet tweet = DBContext.Tweets.Where(x => x.ID == editedTweet.TweetID).FirstOrDefault();
            tweet.Message = editedTweet.Message;
            tweet.CreatedAt = DateTime.Now;
            DBContext.SaveChanges();
            return editedTweet.TweetID; 
        }

        public bool LikeTweets(LikeDTO likedTweet)
        {
            LikeTweet likeTweet = DBContext.LikeTweets.Where(x => x.UserID == likedTweet.LoggedInUserID && x.TweetID == likedTweet.TweetID).FirstOrDefault();
            if(likeTweet != null)
            {
                return false;
            }
            else
            {
                LikeTweet newLikeTweet = objConvertor.LikeTweet(likedTweet);
                DBContext.LikeTweets.Add(newLikeTweet);
                DBContext.SaveChanges();
                return true;
            }
        }

        public bool DislikeTweets(Guid userId, Guid tweetId)
        {
            LikeTweet tweet = DBContext.LikeTweets.Where(x => x.UserID == userId && x.TweetID == tweetId).FirstOrDefault();
            if(tweet != null)
            {
                DBContext.LikeTweets.Remove(tweet);
                DBContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateSearchCount(Tag hashtag)
        {
            Tag newHashtag = DBContext.Tags.Where(x => x.ID == hashtag.ID).FirstOrDefault();
            if(newHashtag.SearchCount == null)
            {
                newHashtag.SearchCount = 1;
            }
            else
            {
                newHashtag.SearchCount += 1;
            }
            DBContext.SaveChanges();
            return true;
        }

        public string MostTrending()
        {
            var hashtag = DBContext.Tags.GroupBy(x => x.TagName).OrderByDescending(gp => gp.Count()).Take(5)
                                    .Select(g => g.Key).ToList();
            //IEnumerable<Tag> hashtag = DBContext.Tags.OrderByDescending(x => x.SearchCount).ThenByDescending(x => x.TagName);
            return hashtag.ElementAt(0);
        }

        public int TotalTweetsToday()
        {
            DateTime date = DateTime.Today;
            int count = DBContext.Tweets.Count(x => DbFunctions.TruncateTime(x.CreatedAt) == DateTime.Today);
            return count;
        }

        public string MostLiked()
        {
            Guid ID = DBContext.LikeTweets.GroupBy(x => x.TweetID).OrderByDescending(x => x.Count()).First().Key;
            Tweet tweet = DBContext.Tweets.Where(x => x.ID == ID).FirstOrDefault();
            return tweet.Message;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DBContext != null)
                {
                    DBContext.Dispose();
                }
            }
        }

        ~TweetDBContext()
        {
            Dispose(false);
        }
    }
}
