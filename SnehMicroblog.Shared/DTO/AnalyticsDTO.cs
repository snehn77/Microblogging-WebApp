﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnehMicroblog.Shared
{
    public class AnalyticsDTO
    {
        public string MostTrending { get; set;  }

        public string MostLiked { get; set; }

        public string MostTweetsBy { get; set; }

        public int TotalTweetsToday { get; set; }
    }
}
