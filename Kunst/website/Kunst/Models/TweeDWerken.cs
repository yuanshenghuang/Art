﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kunst.Models
{
    public class TweeDWerken : Kunstwerk
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Code { get; set; }
        public string CreatieMatDrager { get; set; }
        public string CreatieMatGebruikt { get; set; }
        public string Signatie { get; set; }   
     
    }
}