using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class DrieDWerken : Kunstwerk
    {
        [Required]
        public int Code { get; set; }

        public string CreatieMat { get; set; }
        public string UitvoeringsMat { get; set; }
   
       
        
    }
}