using SnehMicroblog.Business;
using SnehMicroblog.Presentation.Models;
using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SnehMicroblog.Presentation.Controllers
{
    public class SearchController : ApiController
    {
        private SearchBusiness searchBusiness;

        public SearchController()
        {
            searchBusiness = new SearchBusiness();
        }

        [HttpPost]
        [Route("api/user/searchUser")]
        public IList<SearchDTO> SearchUser([FromBody] SearchModel searchString)
        {
            SearchDTO newSearchString = new SearchDTO()
            {
                UserID = Guid.Parse(searchString.UserID),
                SearchString = searchString.SearchString
            };
            IList<SearchDTO> AllResults = searchBusiness.SearchAllUser(newSearchString.SearchString, newSearchString.UserID);
            return AllResults;
        }

        [HttpPost]
        [Route("api/user/searchHashTag")]
        public IList<SearchDTO> SearchTag([FromBody] SearchModel searchString)
        {
            SearchDTO newSearchString = new SearchDTO()
            {
                UserID = Guid.Parse(searchString.UserID),
                SearchString = searchString.SearchString
            };
            IList<SearchDTO> AllResults = searchBusiness.SearchAllHashtag(newSearchString.SearchString, newSearchString.UserID);
            return AllResults;
        }
    }
}
