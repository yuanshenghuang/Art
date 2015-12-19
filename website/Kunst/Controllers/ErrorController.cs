using Kunst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kunst.Controllers
{
    
    public class ErrorController : Controller
    {
        
        public ActionResult NoPageFound()
        {            
            return View();
        }

	}
}