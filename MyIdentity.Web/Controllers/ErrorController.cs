using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyIdentity.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error        
        public PartialViewResult UnAuthorized()
        {
            return PartialView("_UnAuthorized");
        }
    }
}