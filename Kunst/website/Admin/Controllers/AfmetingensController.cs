using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin.Models;

namespace Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class AfmetingensController : Controller
    {
        private AdminDbContext db = new AdminDbContext();

        // GET: Afmetingens
        public ActionResult Index(int id)
        {
            ViewBag.KunstID = id;
            var AfmetingenList = db.Afmetingen.Where(a => a.KunstwerkID == id).ToList();
            return View(AfmetingenList);
        }

        // GET: Afmetingens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afmetingen afmetingen = db.Afmetingen.Find(id);
            if (afmetingen == null)
            {
                return HttpNotFound();
            }
            return View(afmetingen);
        }

        // GET: Afmetingens/Create
        public ActionResult Create(int id)
        {
            Afmetingen afmeting = new Afmetingen();
            afmeting.KunstwerkID = id;
            return View(afmeting);
        }

        // POST: Afmetingens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AfmetingenId,Hoogte,Breedte,Diepte,Diameter,KunstwerkID")] Afmetingen afmetingen)
        {
            if (ModelState.IsValid)
            {
                db.Afmetingen.Add(afmetingen);
                db.SaveChanges();
                string url = Url.Action("Index", "Afmetingens", new { id = afmetingen.KunstwerkID });
                return Json(new{success=true,url=url});
            }

            return View(afmetingen);
        }

        // GET: Afmetingens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afmetingen afmetingen = db.Afmetingen.Find(id);
            if (afmetingen == null)
            {
                return HttpNotFound();
            }
            return View(afmetingen);
        }

        // POST: Afmetingens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AfmetingenId,Hoogte,Breedte,Diepte,Diameter,KunstwerkID")] Afmetingen afmetingen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(afmetingen).State = EntityState.Modified;
                db.SaveChanges();
                string url = Url.Action("Index", "Afmetingens", new { id = afmetingen.KunstwerkID });
                return Json(new { success = true, url = url });
            }
            return View(afmetingen);
        }

        // GET: Afmetingens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afmetingen afmetingen = db.Afmetingen.Find(id);
            if (afmetingen == null)
            {
                return HttpNotFound();
            }
            return View(afmetingen);
        }

        // POST: Afmetingens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Afmetingen afmetingen = db.Afmetingen.Find(id);
            db.Afmetingen.Remove(afmetingen);
            db.SaveChanges();
            string url = Url.Action("Index", "Afmetingens", new { id = afmetingen.KunstwerkID });
            return Json(new { success = true, url = url });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
