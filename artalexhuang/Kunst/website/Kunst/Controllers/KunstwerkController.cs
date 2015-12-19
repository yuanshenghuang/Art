using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kunst.Models;
using Kunst.Helpers;
using System.IO;
using Kunst.ViewModels;

namespace Kunst.Controllers
{
  
    public class KunstwerkController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Kunstwerk(string category)
        {            
            var list = from x in db.KunstWerken select x;
            
            list = list.Where(x => x.Categorie == category).ToList().Select(item => new Kunstwerk { KunstwerkId = item.KunstwerkId, Titel = item.Titel}).AsQueryable();
                   
            return PartialView(list);              
        }



        public ActionResult KunstwerkDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string cultureName = null;


            // Attempt to read the culture cookie from http Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
            {
                cultureName = "nl-be";
            }

            KunstwerkViewModel mymodel = new KunstwerkViewModel();

            var kunstWerken = (from x in db.KunstWerken where x.KunstwerkId == id select x);
            var drieDWerken = (from x in db.DrieDWerken where x.KunstwerkId == id select x);
            var tweeDWerken = (from x in db.TweeDWerken where x.KunstwerkId == id select x);           
          

            if (cultureName == "fr")
            {
                mymodel.Kunstwerks = kunstWerken.ToList().Select(item => new Kunstwerk { Titel = item.Titel, Categorie = item.Categorie, BeschrijvingFR = item.BeschrijvingFR, Foto = item.Foto });
                mymodel.DrieDwerkens = drieDWerken.ToList();
                mymodel.TweeDwerkens = tweeDWerken.ToList();  
            }
            else if (cultureName == "de")
            {
                mymodel.Kunstwerks = kunstWerken.ToList().Select(item => new Kunstwerk { Titel = item.Titel, Categorie = item.Categorie, BeschrijvingDE = item.BeschrijvingDE, Foto = item.Foto });
                mymodel.DrieDwerkens = drieDWerken.ToList();
                mymodel.TweeDwerkens = tweeDWerken.ToList(); 
            }
            else if (cultureName == "en")
            {
                mymodel.Kunstwerks = kunstWerken.ToList().Select(item => new Kunstwerk { Titel = item.Titel, Categorie = item.Categorie, BeschrijvingEN = item.BeschrijvingEN, Foto = item.Foto });
                mymodel.DrieDwerkens = drieDWerken.ToList();
                mymodel.TweeDwerkens = tweeDWerken.ToList();            
            }
            else
            {
                mymodel.Kunstwerks = kunstWerken.ToList().Select(item => new Kunstwerk { Titel = item.Titel, Categorie = item.Categorie, BeschrijvingNL = item.BeschrijvingNL, Foto = item.Foto });
                mymodel.DrieDwerkens = drieDWerken.ToList();
                mymodel.TweeDwerkens = tweeDWerken.ToList();
            }

            return PartialView(mymodel);
        }

     










        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }



        public ActionResult GetImage(string imageName)
        {

            var root = Server.MapPath("~");
            //var root = root1.Remove(root1.Length - 7) ;
            string filePath = Path.GetFullPath(Path.Combine(root, @"..\Images\" + imageName));
            //var path = Path.Combine(filePath, imageName);
            //path = Path.GetFullPath(path);


            return File(filePath, "imageName/jpg");
        }

        //public ActionResult GetDoc(string docName)
        //{

        //    var root = Server.MapPath("~");
          
        //    string filePath = Path.GetFullPath(Path.Combine(root, @"..\Documenten\" + docName));          


        //    return File(filePath, "docName/docx");
        //}

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
