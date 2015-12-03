using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin.Models;
using Admin.ViewModels;

namespace Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CursusController : Controller
    {
        private CurussenModel db = new CurussenModel();

        // GET: Cursus
        public async Task<ActionResult> Index(int? id)
        {
            var viewModels = new InschrijvingenViewModel();
            viewModels.Cursus = await db.Cursus.ToListAsync();
            if (id != null)
            {   //explicit loading
                //var selectedCursus = await db.Cursus.Where(i => i.CursusId == id).SingleOrDefaultAsync();              
                //await db.Entry(selectedCursus).Collection(x => x.CursusInschrijvings).LoadAsync();

                //eager loading
                var selectedCursus = await db.Cursus.Where(i => i.CursusId == id).SingleOrDefaultAsync(); 
                
        
                viewModels.Inschrijvingen = selectedCursus.CursusInschrijvings;
               
            }          

            return View(viewModels);
        }

        // GET: Cursus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cursu cursu = await db.Cursus.FindAsync(id);
            if (cursu == null)
            {
                return HttpNotFound();
            }
            return View(cursu);
        }

        // GET: Cursus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cursus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CursusId,CursusNaam,CursusInfo")] Cursu cursu)
        {
            if (ModelState.IsValid)
            {
                db.Cursus.Add(cursu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cursu);
        }

        // GET: Cursus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cursu cursu = await db.Cursus.FindAsync(id);
            if (cursu == null)
            {
                return HttpNotFound();
            }
            return View(cursu);
        }

        // POST: Cursus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CursusId,CursusNaam,CursusInfo")] Cursu cursu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cursu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cursu);
        }

        // GET: Cursus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cursu cursu = await db.Cursus.FindAsync(id);
            if (cursu == null)
            {
                return HttpNotFound();
            }
            return View(cursu);
        }

        // POST: Cursus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cursu cursu = await db.Cursus.FindAsync(id);
            db.Cursus.Remove(cursu);
            await db.SaveChangesAsync();
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
