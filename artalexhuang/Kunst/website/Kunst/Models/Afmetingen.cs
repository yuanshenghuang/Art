using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kunst.Models
{
    public class Afmetingen
    {
        public int AfmetingenId { get; set; }
        public Nullable<decimal> Hoogte { get; set; }
        public Nullable<decimal> Breedte { get; set; }
        public Nullable<decimal> Diepte { get; set; }
        public Nullable<decimal> Diameter { get; set; }
        public int KunstwerkID { get; set; }
       
        public virtual Kunstwerk Kunstwerk { get; set; }
    }
}