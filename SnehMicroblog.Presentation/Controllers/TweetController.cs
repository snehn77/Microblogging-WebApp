using Newtonsoft.Json;
using SnehMicroblog.Business;
using SnehMicroblog.Presentation.Models;
using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SnehMicroblog.Presentation.Controllers
{
    public class TweetController : ApiController
    {
        private TweetBusiness tweetBusiness;
        private ObjectConvertor objConvertor;

        public TweetController()
        {
            tweetBusiness = new TweetBusiness();
            objConvertor = new ObjectConvertor();
        }

        [Route("api/user/newTweet")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] TweetModel tweet)
        {
            try
            {
                TweetDTO newTweet = objConvertor.NewTweet(tweet);
                newTweet = tweetBusiness.CreateNewTweet(newTweet);
                return Ok(new { Tweet = newTweet });
            }
            catch(Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(e.Message)));
            }
        }

        [HttpGet]
        [Route("api/user/playground/{userId}")]
        public IList<TweetsDTO> Get(string userId)
        {
            Guid ID = Guid.Parse(userId);
            IList<TweetsDTO> tweets = tweetBusiness.GetAllTweets(ID);
            return tweets;
        }

        [HttpDelete]
        [Route("api/user/deleteTweet/{UserID}/{TweetID}")]
        public bool DeleteTweet(string UserId , string TweetId)
        {
            Guid userId = Guid.Parse(UserId);
            Guid tweetId = Guid.Parse(TweetId);
            return tweetBusiness.DeleteTweet(userId, tweetId);
        }

        [HttpPut]
        [Route("api/user/updatetweet")]
        public bool Put([FromBody] TweetModel tweet)
        {
            TweetDTO updatedTweet = objConvertor.EditTweet(tweet);
            return tweetBusiness.UpdateTweet(updatedTweet);
        }

        [HttpPost]
        [Route("api/user/like")]
        public bool Post(LikeModel tweet)
        {
            LikeDTO likedTweet = objConvertor.LikeTweet(tweet);
            tweetBusiness.LikeTweet(likedTweet);
            return true;
        }

        [HttpDelete]
        [Route("api/user/dislike/{UserID}/{TweetID}")]
        public bool DislikeTweet(string UserID , string TweetID)
        {
            Guid userId = Guid.Parse(UserID);
            Guid tweetId = Guid.Parse(TweetID);
            tweetBusiness.DislikeTweet(userId, tweetId);
            return true;
        }


    }
}
