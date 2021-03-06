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
        public Nullable<decimal> Hoogte { get; set; }
        public Nullable<decimal> Breedte { get; set; }     
     
    }
}