using Kunst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kunst.Controllers
{
    public class CursusInschrijvingsController : BaseController
    {
        private EventsConnectionEntities _dataContext = new EventsConnectionEntities();

        public ActionResult Create(int? id)
        {
            if(id == null )
            {
                return RedirectToAction("Index","Home");
            }
            string cursusid = Convert.ToString(id);
            ViewBag.cursusId = cursusid;

            return PartialView();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]      
        public ActionResult Create([Bind(Include="CursusInschrijvingId,VoorNaam,AchterNaam,Email,TelefoonGsm,CursusId,datum")] CursusInschrijving cursusInschrijving)
        {
            if (ModelState.IsValid)
            {
                _dataContext.CursusInschrijving.Add(cursusInschrijving);
                _dataContext.SaveChanges();
                return View("Details",cursusInschrijving);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Details(CursusInschrijving cursusInschrijving)
        {

            if (cursusInschrijving == null)
            {
                return HttpNotFound();
            }
            return View(cursusInschrijving);
        }

       
    }
}