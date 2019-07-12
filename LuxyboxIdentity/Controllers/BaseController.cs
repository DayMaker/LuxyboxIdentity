﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LuxyboxIdentity.Controllers
{
    public class BaseController : Controller
    {
        protected Data.Entities dbContext = null;
        protected override void Initialize(RequestContext requestContext)
        {
            dbContext = new Data.Entities();
            base.Initialize(requestContext);
        }
        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
   
    
}