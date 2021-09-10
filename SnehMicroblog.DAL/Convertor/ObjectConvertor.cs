using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnehMicroblog.Shared;

namespace SnehMicroblog.DAL
{
    public class ObjectConvertor
    {
        public User NewUser(SignupDTO user)
        {
            User newUser = new User()
            {
                ID = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Image = user.Image,
                Country = user.Country,
                PasswordHash = user.Password
            };
            return newUser;
        }

        public LoggedInDTO LoginUser(User user)
        {
            LoggedInDTO userDTO = new LoggedInDTO()
            {
                ID = user.ID,
                Email = user.Email,
                FirstName = user.FirstName,
                Image = user.Image,
                Country = user.Country,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
                
            };
            return userDTO;
        }

        public Tweet NewTweet(TweetDTO tweet)
        {
            Tweet newTweet = new Tweet()
            {
                ID = Guid.NewGuid(),
                Message = tweet.Message,
                UserID = tweet.UserID,
                CreatedAt = DateTime.Now,
            };
            return newTweet;
        }

        public LikeTweet LikeTweet(LikeDTO tweet)
        {
            LikeTweet newLikedTweet = new LikeTweet()
            {
                ID = Guid.NewGuid(),
                TweetID = tweet.TweetID,
                UserID = tweet.LoggedInUserID
            };
            return newLikedTweet;
        }


    }
}
