//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SnehMicroblog.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LikeTweet
    {
        public System.Guid ID { get; set; }
        public System.Guid TweetID { get; set; }
        public System.Guid UserID { get; set; }
    
        public virtual Tweet Tweet { get; set; }
        public virtual User User { get; set; }
    }
}
