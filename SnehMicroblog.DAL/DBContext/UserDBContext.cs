using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.DAL
{
    public class UserDBContext : IDisposable
    {
        snehEntities DBContext;
        ObjectConvertor objConvertor;

        public UserDBContext()
        {
            DBContext = new snehEntities();
            objConvertor = new ObjectConvertor();
        }

        public SignupDTO CreateNewUser(SignupDTO user)
        {
            User newUser = objConvertor.NewUser(user);
            DBContext.Users.Add(newUser);
            DBContext.SaveChanges();
            return user;
        }


        public bool EmailExists(string email)
        { 
            if (DBContext.Users.Where(x => x.Email == email).Any())
            {
                return true;
            }
            return false;
        }

        public LoggedInDTO GetUserInfo(AuthDTO user)
        {
            User userLocal = DBContext.Users.FirstOrDefault(x=>x.ID == user.ID);
            LoggedInDTO userDTO = objConvertor.LoginUser(userLocal);
            return userDTO;
        }

        public AuthDTO CheckUserExsists(LoginDTO user)
        {
           User userDTO = DBContext.Users.FirstOrDefault(x => x.Email == user.Email);
           if (userDTO != null)
           {
              AuthDTO authInfo = new AuthDTO()
              {
                  ID = userDTO.ID,
                  Email = userDTO.Email,
                  Password = userDTO.PasswordHash
              };
              return authInfo;
           }
           return null;
        }

        public bool Follow(FollowDTO user)
        {
            Follow newUser = DBContext.Follows.Where(x => x.Followed_UserID == user.UserToFollowID).FirstOrDefault();
            if(newUser != null && newUser.Follower_UserID == user.UserID)
            {
                return false;
            }
            try
            {
                Follow followUser = new Follow()
                {
                    ID = Guid.NewGuid(),
                    Follower_UserID = user.UserID,
                    Followed_UserID = user.UserToFollowID
                };
                DBContext.Follows.Add(followUser);
                DBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UnFollow(FollowDTO user)
        {
            Follow newUser = DBContext.Follows.Where(x => x.Followed_UserID == user.UserToFollowID).FirstOrDefault();
            DBContext.Follows.Remove(newUser);
            DBContext.SaveChanges();
            return true;
        }

        public IList<UserDTO> GetAllFollowers(Guid loginUserId)
        {
            IList<UserDTO> followersList = new List<UserDTO>();
            UserDTO follower;
            User user;

            IEnumerable<Follow> followedUser = DBContext.Follows.Where(x => x.Followed_UserID == loginUserId);
            var i = 0;
            foreach (var userLocal in followedUser)
            {

                user = new User();
                Follow Followers = DBContext.Follows.Where(x => x.Follower_UserID == userLocal.Follower_UserID).FirstOrDefault();
                user = DBContext.Users.Where(x => x.ID == Followers.Follower_UserID).FirstOrDefault();
                follower = new UserDTO()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Image = user.Image,
                    Count = i + 1
                };
                followersList.Add(follower);
                i++;
            }
            return followersList;
        }

        public IList<UserDTO> GetAllFollowing(Guid loginUserId)
        {
            IList<UserDTO> followersList = new List<UserDTO>();
            UserDTO follower;
            User user;

            IEnumerable<Follow> followedUser = DBContext.Follows.Where(x => x.Follower_UserID == loginUserId);
            var i = 0;
            foreach (var userLocal in followedUser)
            {

                user = new User();
                Follow Followers = DBContext.Follows.Where(x => x.Followed_UserID == userLocal.Followed_UserID).FirstOrDefault();
                user = DBContext.Users.Where(x => x.ID == Followers.Followed_UserID).FirstOrDefault();
                follower = new UserDTO()
                {
                    Email = user.Email,
                    ID = user.ID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Image = user.Image,
                    Count = i + 1
                };
                followersList.Add(follower);
                i++;
            }
            return followersList;
        }

        public string MostTweetsBy()
        {
            Guid ID = DBContext.Tweets.GroupBy(x => x.UserID).OrderByDescending(x => x.Count()).First().Key;
            User user = DBContext.Users.Where(x => x.ID == ID).FirstOrDefault();
            string Username = user.FirstName + " " +user.LastName;
            return Username; 
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

        ~UserDBContext()
        {
            Dispose(false);
        }
    }
}
