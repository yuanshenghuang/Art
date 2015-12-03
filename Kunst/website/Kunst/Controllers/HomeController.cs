using Kunst.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kunst.Controllers
{
   
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Admin()
        {
            return Redirect("http://localhost:64576/Home/Index");
        }
            
      
    }
}