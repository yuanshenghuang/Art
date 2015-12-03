using Kunst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kunst.Controllers
{
    public class CursusController : BaseController
    {
        private EventsConnectionEntities _dataContext = new EventsConnectionEntities();

       
        public ActionResult Index()
        {
            var query = from cursus in _dataContext.Cursus
                        select cursus;
            var listcursus = query.ToList();          

            return PartialView(listcursus);
        }

       


    }
}