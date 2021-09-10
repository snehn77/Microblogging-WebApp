using SnehMicroblog.Business;
using SnehMicroblog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SnehMicroblog.Presentation.Controllers
{
    public class AnalyticsController : ApiController
    {
        [HttpGet]
        [Route("api/analytics")]
        public AnalyticsDTO GetAnalysis()
        {
            AnalyticsBusiness analysis = new AnalyticsBusiness();
            return analysis.Analysis();
        }
    }
}
