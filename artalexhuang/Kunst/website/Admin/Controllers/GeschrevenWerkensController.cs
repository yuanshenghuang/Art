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
    public class GeschrevenWerkensController : Controller
    {
        private AdminDbContext db = new AdminDbContext();

        // GET: GeschrevenWerkens
        public ActionResult Index()
        {
            return View(db.GeschrevenWerken.ToList());
        }

        // GET: GeschrevenWerkens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeschrevenWerken geschrevenWerken = db.GeschrevenWerken.Find(id);
            if (geschrevenWerken == null)
            {
                return HttpNotFound();
            }
            return View(geschrevenWerken);
        }

        // GET: GeschrevenWerkens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeschrevenWerkens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KunstwerkId,Categorie,Titel,BeschrijvingNL,BeschrijvingEN,BeschrijvingFR,BeschrijvingDE")] GeschrevenWerken geschrevenWerken, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if(upload != null && upload.ContentLength >0)
                {
                    var root = Server.MapPath("~");
                    var path = System.IO.Path.Combine(root, "../Images/", upload.FileName);
                    upload.SaveAs(path);
                    geschrevenWerken.Foto = upload.FileName;
                }

                db.KunstWerken.Add(geschrevenWerken);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(geschrevenWerken);
        }

        // GET: GeschrevenWerkens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeschrevenWerken geschrevenWerken = db.GeschrevenWerken.Find(id);
            if (geschrevenWerken == null)
            {
                return HttpNotFound();
            }
            return View(geschrevenWerken);
        }

        // POST: GeschrevenWerkens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KunstwerkId,Categorie,Titel,BeschrijvingNL,BeschrijvingEN,BeschrijvingFR,BeschrijvingDE,Foto")] GeschrevenWerken geschrevenWerken,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if(upload !=null && upload.ContentLength >0)
                {
                    var root = Server.MapPath("~");
                    var path = System.IO.Path.Combine(root, "../Images/", geschrevenWerken.Foto);
                    if(System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    var newPath = System.IO.Path.Combine(root, "../Images/", upload.FileName);
                    upload.SaveAs(newPath);

                    geschrevenWerken.Foto = upload.FileName;

                }
                db.Entry(geschrevenWerken).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(geschrevenWerken);
        }

        // GET: GeschrevenWerkens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeschrevenWerken geschrevenWerken = db.GeschrevenWerken.Find(id);
            if (geschrevenWerken == null)
            {
                return HttpNotFound();
            }
            return View(geschrevenWerken);
        }

        // POST: GeschrevenWerkens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GeschrevenWerken geschrevenWerken = db.GeschrevenWerken.Find(id);
            if(geschrevenWerken.Foto != null)
            {
                var root = Server.MapPath("~");
                var path = System.IO.Path.Combine(root, "../Images/", geschrevenWerken.Foto);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            db.KunstWerken.Remove(geschrevenWerken);
            db.SaveChanges();
            return RedirectToAction("Index");
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
