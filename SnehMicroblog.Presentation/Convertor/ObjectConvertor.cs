using SnehMicroblog.Presentation.Models;
using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnehMicroblog.Presentation
{
    public class ObjectConvertor
    {
        public SignupDTO NewUser(SignupModel user)
        {
            SignupDTO userDTO = new SignupDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Image = user.Image,
                Country = user.Country,
                Password = user.Password
            };
            return userDTO;
        }

        public LoginDTO LoginUser(LoginModel user)
        {
            LoginDTO userDTO = new LoginDTO()
            {
                Email = user.Email,
                Password = user.Password
            };
            return userDTO;
        }

        public TweetDTO NewTweet (TweetModel tweet)
        {
            TweetDTO newTweet = new TweetDTO()
            {
                UserID = Guid.Parse(tweet.UserID),
                Message = tweet.Message
            };
            return newTweet;
        }

        public TweetDTO EditTweet(TweetModel tweet)
        {
            TweetDTO newTweet = new TweetDTO()
            {
                UserID = Guid.Parse(tweet.UserID),
                TweetID = tweet.TweetID,
                Message = tweet.Message
            };
            return newTweet;
        }

        public LikeDTO LikeTweet(LikeModel like)
        {
            LikeDTO likedTweet = new LikeDTO()
            {
                TweetID = Guid.Parse(like.TweetID),
                LoggedInUserID = Guid.Parse(like.LoggedInUserID)
            };
            return likedTweet;
        }
    }
}