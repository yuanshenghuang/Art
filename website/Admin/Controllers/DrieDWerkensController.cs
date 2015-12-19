using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin.Models;
using System.IO;

namespace Admin.Controllers
{
    [Authorize]
    public class DrieDWerkensController : Controller
    {
        private AdminDbContext db = new AdminDbContext();

        // GET: DrieDWerkens
        public ActionResult Index()
        {   
            return View(db.DrieDWerken.ToList());
        }

        // GET: DrieDWerkens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrieDWerken drieDWerken = db.DrieDWerken.Find(id);
            if (drieDWerken == null)
            {
                return HttpNotFound();
            }
            return View(drieDWerken);
        }

        // GET: DrieDWerkens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DrieDWerkens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KunstwerkId,Categorie,Titel,BeschrijvingNL,BeschrijvingEN,BeschrijvingFR,BeschrijvingDE,Code,Materiaal,Hoogte,Breedte,Diepte,Foto")] DrieDWerken drieDWerken, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if(upload !=null && upload.ContentLength >0)
                {
                    var root = Server.MapPath("~");
                    string path = Path.GetFullPath(Path.Combine(root, @"../Images/" + upload.FileName));
                  
                    upload.SaveAs(path); 

                    drieDWerken.Foto = upload.FileName;
                }
             
                db.KunstWerken.Add(drieDWerken);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drieDWerken);
        }

        // GET: DrieDWerkens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrieDWerken drieDWerken = db.DrieDWerken.Find(id);
            if (drieDWerken == null)
            {
                return HttpNotFound();
            }
            return View(drieDWerken);
        }

        // POST: DrieDWerkens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KunstwerkId,Categorie,Titel,BeschrijvingNL,BeschrijvingEN,BeschrijvingFR,BeschrijvingDE,Code,Materiaal,Hoogte,Breedte,Diepte,Foto")] DrieDWerken drieDWerken, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if(upload != null && upload.ContentLength >0)
                {
                    var root = Server.MapPath("~");
                    string path = Path.GetFullPath(Path.Combine(root, @"../Images/" + drieDWerken.Foto));
                    if(System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    var newPath = Path.GetFullPath(Path.Combine(root, @"../Images/" + upload.FileName));
                    upload.SaveAs(newPath);

                    drieDWerken.Foto = upload.FileName;
                }

                db.Entry(drieDWerken).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drieDWerken);
        }

        // GET: DrieDWerkens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrieDWerken drieDWerken = db.DrieDWerken.Find(id);
            if (drieDWerken == null)
            {
                return HttpNotFound();
            }
            return View(drieDWerken);
        }

        // POST: DrieDWerkens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DrieDWerken drieDWerken = db.DrieDWerken.Find(id);

            if(drieDWerken.Foto != null)
            {
                var root = Server.MapPath("~");
                string path = Path.GetFullPath(Path.Combine(root, @"../Images/" + drieDWerken.Foto));
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
          

            db.KunstWerken.Remove(drieDWerken);
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
