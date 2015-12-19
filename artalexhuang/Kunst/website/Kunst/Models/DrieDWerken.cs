using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kunst.Models
{
    public class DrieDWerken : Kunstwerk
    {
        [Required]
        public int Code { get; set; }
        public Nullable<decimal> Hoogte { get; set; }
        public Nullable<decimal> Breedte { get; set; }
        public Nullable<decimal> Diepte { get; set; }
    }
}