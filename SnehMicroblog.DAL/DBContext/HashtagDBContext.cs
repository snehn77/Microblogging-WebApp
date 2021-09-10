using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.DAL
{
    public class HashtagDBContext : IDisposable
    {
        snehEntities DBContext;

        public HashtagDBContext()
        {
            DBContext = new snehEntities();
        }

        public bool AddHashtages(List<string> hashTags, Guid tweetID)
        {
            foreach(string hashTag in hashTags)
            {
                Tag newTag = new Tag()
                {
                    ID = Guid.NewGuid(),
                    TweetID = tweetID,
                    TagName = hashTag
                };
                DBContext.Tags.Add(newTag);
                DBContext.SaveChanges();
            }
            return true;
        }

        public bool DeleteTag(Guid tweetId)
        {
            IList<Tag> hashtagList = DBContext.Tags.Where(x => x.TweetID == tweetId).ToList();
            if(hashtagList.Count > 0)
            {
                foreach(var item in hashtagList)
                {
                    DBContext.Entry(item).State = EntityState.Deleted;
                    DBContext.SaveChanges();
                }
                return true;
            }
            else
            {
                return false;
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

        ~HashtagDBContext()
        {
            Dispose(false);
        }

    }
}
