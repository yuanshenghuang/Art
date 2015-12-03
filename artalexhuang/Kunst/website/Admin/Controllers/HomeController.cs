using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "admin")]
        //[Authorize]
        public ActionResult Index()
        {
            return View();
        }

       
    }
}