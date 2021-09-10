using SnehMicroblog.DAL;
using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Business
{
    public class SearchBusiness
    {
        private SearchDBContext searchDBContext;

        public SearchBusiness()
        {
            searchDBContext = new SearchDBContext();
        }

        public IList<SearchDTO> SearchAllUser(string searchString , Guid UserId)
        {
            IList<SearchDTO> allUsers = searchDBContext.GetAllUsers(searchString, UserId);
            if (allUsers != null)
            {
                return allUsers;
            }
            else
            {
                return null;
            }
        }

        public IList<SearchDTO> SearchAllHashtag(string searchString, Guid UserId)
        {
            IList<SearchDTO> getAllResults = searchDBContext.GetAllHashtag(searchString, UserId);
            if(getAllResults != null)
            {
                return getAllResults;
            }
            else
            {
                return null;
            }
        }
    }
}
