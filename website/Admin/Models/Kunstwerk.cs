using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class Kunstwerk
    {
        public int KunstwerkId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Categorie { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Title")]
        public string Titel { get; set; }

        public string BeschrijvingNL { get; set; }
        public string BeschrijvingEN { get; set; }
        public string BeschrijvingFR { get; set; }
        public string BeschrijvingDE { get; set; }

        public string Foto { get; set; }

        public string Materiaal { get; set; }

      
       
    }
}