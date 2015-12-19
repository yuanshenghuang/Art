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
    [Authorize]
    public class TweeDWerkensController : Controller
    {
        private AdminDbContext db = new AdminDbContext();

        // GET: TweeDWerkens
        public ActionResult Index()
        {
            return View(db.TweeDWerken.ToList());
        }

        // GET: TweeDWerkens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TweeDWerken tweeDWerken = db.TweeDWerken.Find(id);
            if (tweeDWerken == null)
            {
                return HttpNotFound();
            }
            return View(tweeDWerken);
        }

        // GET: TweeDWerkens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TweeDWerkens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KunstwerkId,Categorie,Titel,BeschrijvingNL,BeschrijvingEN,BeschrijvingFR,BeschrijvingDE,Code,Materiaal,Hoogte,Breedte,Foto")] TweeDWerken tweeDWerken, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if(upload != null && upload.ContentLength >0)
                {
                    var root = Server.MapPath("~");
                    //var path = System.IO.Path.GetFullPath(System.IO.Path.Combine(root, "../Images/", upload.FileName));
                    //var path = System.IO.Path.Combine(root, "../Images/", upload.FileName);
                    var path = System.IO.Path.GetFullPath(root+ "../Images/"+ upload.FileName);
                    upload.SaveAs(path);

                    tweeDWerken.Foto = upload.FileName;
                }               

                db.KunstWerken.Add(tweeDWerken);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tweeDWerken);
        }

        // GET: TweeDWerkens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TweeDWerken tweeDWerken = db.TweeDWerken.Find(id);
            if (tweeDWerken == null)
            {
                return HttpNotFound();
            }
            return View(tweeDWerken);
        }

        // POST: TweeDWerkens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KunstwerkId,Categorie,Titel,BeschrijvingNL,BeschrijvingEN,BeschrijvingFR,BeschrijvingDE,Code,Materiaal,Hoogte,Breedte,Foto")] TweeDWerken tweeDWerken, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if(upload != null && upload.ContentLength > 0)
                {
                    var root = Server.MapPath("~");
                    var path = System.IO.Path.Combine(root, "../Images/", tweeDWerken.Foto);
                    if(System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    var newPath = System.IO.Path.Combine(root, "../Images/", tweeDWerken.Foto);
                    upload.SaveAs(newPath);

                    tweeDWerken.Foto = upload.FileName;
                }

                db.Entry(tweeDWerken).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tweeDWerken);
        }

        // GET: TweeDWerkens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TweeDWerken tweeDWerken = db.TweeDWerken.Find(id);
            if (tweeDWerken == null)
            {
                return HttpNotFound();
            }
            return View(tweeDWerken);
        }

        // POST: TweeDWerkens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TweeDWerken tweeDWerken = db.TweeDWerken.Find(id);

            if(tweeDWerken.Foto != null)
            {
                var root = Server.MapPath("~");
                var path = System.IO.Path.Combine(root, "../Images/",tweeDWerken.Foto);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
          
            db.KunstWerken.Remove(tweeDWerken);
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
