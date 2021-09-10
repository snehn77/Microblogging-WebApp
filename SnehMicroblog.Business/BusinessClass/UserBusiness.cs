using SnehMicroblog.DAL;
using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Business
{
    public class UserBusiness
    {
        private UserDBContext DBContext;

        public UserBusiness()
        {
            DBContext = new UserDBContext();
        }

        // Signup Business Class
        public SignupDTO SignUp(SignupDTO user)
        {
            if (!DBContext.EmailExists(user.Email))
            {
                user.Password = PasswordHasher.Encrypt(user.Password , PasswordHasher.EncryptionKey);
                if(user.Image == null)
                {
                    user.Image = Defaults.defaultUserImage;
                }
                SignupDTO userDTO = DBContext.CreateNewUser(user);
                return userDTO;
            }
            else
            {
                throw new Exceptions.AlreadyExistsException("This Email id is already registered");
            }
        } 

        // Login Buisness Model
        public LoggedInDTO Login(LoginDTO user)
        {
            AuthDTO authUser = DBContext.CheckUserExsists(user);
            if(authUser == null)
            {
                throw new Exceptions.InvalidCredentialsException("No User registered with this email");
            }

            string encryptedPassword = PasswordHasher.Encrypt(user.Password, PasswordHasher.EncryptionKey);
            if(authUser.Password == encryptedPassword)
            {
                LoggedInDTO userDTO = DBContext.GetUserInfo(authUser);
                return userDTO;
            }
            else
            {
                throw new Exceptions.InvalidCredentialsException("Incorrect Password");
            }
        }

        // To follow business class
        public bool Follow(FollowDTO user)
        {
            bool result = DBContext.Follow(user);
            return result;
        }

        // To unfollow business class
        public bool UnFollow(FollowDTO user)
        {
            bool result = DBContext.UnFollow(user);
            return true;
        }

        public IList<UserDTO> GetAllFollowers(Guid userID)
        {
            IList<UserDTO> user = DBContext.GetAllFollowers(userID);
            return user;
        }

        public IList<UserDTO> GetAllFollowing(Guid userID)
        {
            IList<UserDTO> user = DBContext.GetAllFollowing(userID);
            return user;
        }
    }
}
