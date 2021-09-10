using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.DAL
{
    public class SearchDBContext : IDisposable
    {
        snehEntities DBContext;
        TweetDBContext tweetDBContext;
        public SearchDBContext()
        {
            DBContext = new snehEntities();
            tweetDBContext = new TweetDBContext();
        }

        public IList<SearchDTO> GetAllUsers(string searchString , Guid UserId)
        {
            if(searchString !=null && searchString != "")
            {
                IList<SearchDTO> userList = new List<SearchDTO>();
                SearchDTO getAllUsers;
                IList<User> user = new List<User>();
                user = DBContext.Users.Where(x => (x.FirstName.Contains(searchString) || x.LastName.Contains(searchString)) && (x.ID != UserId)).ToList();
                if (user.Count > 0)
                {
                    foreach(var item in user)
                    {
                        getAllUsers = new SearchDTO()
                        {
                            Image = item.Image,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Email = item.Email,
                            UserId = item.ID
                        };
                        Follow follow = DBContext.Follows.Where(x => (x.Follower_UserID == UserId) && (x.Followed_UserID == getAllUsers.UserId)).FirstOrDefault();
                        if(follow != null)
                        {
                            getAllUsers.IsFollowed = true;
                        }
                        else
                        {
                            getAllUsers.IsFollowed = false;
                        }
                        userList.Add(getAllUsers);
                    }
                    return userList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public IList<SearchDTO> GetAllHashtag(string searchString, Guid UserId)
        {
            if(searchString !=null && searchString != "")
            {
                IList<Tag> hashtag = DBContext.Tags.Where(x => x.TagName.Contains(searchString)).ToList();
                IList<SearchDTO> hashtagList = new List<SearchDTO>();
                SearchDTO getAllTags;
                if(hashtag.Count > 0)
                {
                    foreach(var tag in hashtag)
                    {
                        getAllTags = new SearchDTO();
                        tweetDBContext.UpdateSearchCount(tag);
                        IList<Tweet> tweet = DBContext.Tweets.Where(x => x.ID == tag.TweetID).ToList();
                        foreach(var item in tweet)
                        {
                            User user = DBContext.Users.Where(x => x.ID == item.UserID).FirstOrDefault();
                            getAllTags.Message = item.Message;
                            getAllTags.CreatedAt = item.CreatedAt;
                            getAllTags.Username = user.FirstName + ' '+user.LastName;
                            getAllTags.Image = user.Image;
                        }
                        hashtagList.Add(getAllTags);
                    }
                    return hashtagList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
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

        ~SearchDBContext()
        {
            Dispose(false);
        }
    }
}
