using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ImagesController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult GetImage(string imageName)
        {

            var root = Server.MapPath("~");
            //var root = root1.Remove(root1.Length - 7) ;
            string filePath = Path.GetFullPath(Path.Combine(root, @"..\Images\" + imageName));
            //var path = Path.Combine(filePath, imageName);
            //path = Path.GetFullPath(path);


            return File(filePath, "imageName/jpg");
        }
    }
}