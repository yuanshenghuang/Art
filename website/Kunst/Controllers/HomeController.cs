using Kunst.Helpers;
using Kunst.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Kunst.ViewModels;

namespace Kunst.Controllers
{   
    public class HomeController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {  //install nuget package PagedList.Mvc
            //using PagedList
            KunstwerkViewModel viewmodel = new KunstwerkViewModel()
            {
                Kunstwerks = db.KunstWerken.ToList().Select(item => new Kunstwerk { KunstwerkId=item.KunstwerkId, Foto = item.Foto })
            };

            int pageNumber = (page ?? 1);
            int pageSize = 3;

            return View(viewmodel.Kunstwerks.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return Redirect("http://localhost:64576/Home/Index");
        }
            
      
    }
}