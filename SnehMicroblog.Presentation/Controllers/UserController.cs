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
using System.Web;
using System.Web.Http;

namespace SnehMicroblog.Presentation.Controllers
{
    public class UserController : ApiController
    {
        private UserBusiness userBusiness;
        private ObjectConvertor objConvertor;

        public UserController()
        {
            userBusiness = new UserBusiness();
            objConvertor = new ObjectConvertor();
        }

        [Route("api/login")]
        public IHttpActionResult Post([FromBody] LoginModel user)
        {
            try
            {
                LoginDTO userDTO = objConvertor.LoginUser(user);
                LoggedInDTO loginDTO = userBusiness.Login(userDTO);
                return Ok(loginDTO);
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(e.Message)));
            }
        }

        public IHttpActionResult Post([FromBody] SignupModel user)
        {
            try
            {
                SignupDTO userDTO = objConvertor.NewUser(user);
                SignupDTO newUser = userBusiness.SignUp(userDTO);
                return Ok("User Registered Successfully");
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(e.Message)));
            }

        }

        [HttpPost]
        [Route("api/user/follow")]
        public bool Post(FollowModel user)
        {
            FollowDTO userFollow = new FollowDTO()
            {
                UserID = Guid.Parse(user.UserID),
                UserToFollowID = Guid.Parse(user.UserToFollowID)
            };
            bool result = userBusiness.Follow(userFollow);
            return result;
        }

        [HttpPost]
        [Route("api/user/unfollow")]
        public bool Unfollow(FollowModel user)
        {
            FollowDTO userFollow = new FollowDTO()
            {
                UserID = Guid.Parse(user.UserID),
                UserToFollowID = Guid.Parse(user.UserToFollowID)
            };
            userBusiness.UnFollow(userFollow);
            return true;
        }

        [HttpGet]
        [Route("api/user/followers/{userId}")]
        public IList<UserDTO> Get(string userId)
        {
            Guid loggedInUserId = Guid.Parse(userId);
            IList<UserDTO> users = userBusiness.GetAllFollowers(loggedInUserId);
            return users;
        }

        [HttpGet]
        [Route("api/user/following/{userId}")]
        public IList<UserDTO> Following(string userId)
        {
            Guid loggedInUserId = Guid.Parse(userId);
            IList<UserDTO> users = userBusiness.GetAllFollowing(loggedInUserId);
            return users;
        }

        [Route("api/user/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);

                postedFile.SaveAs(physicalPath);

                return filename;
            }
            catch (Exception)
            {
                return "anonymous.png";
            }
        }
    }
}
