using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kunst.Models
{
    public class KunstwerkViewModel
    {
        public IEnumerable<Kunstwerk> Kunstwerks { get; set; }
        public IEnumerable<DrieDWerken> DrieDwerkens { get; set; }
        public IEnumerable<TweeDWerken> TweeDwerkens { get; set; }
        public IEnumerable<GeschrevenWerken> GeschrevenWerkens { get; set; }
        public IEnumerable<Afmetingen> afmetingens { get; set; }
    }
}