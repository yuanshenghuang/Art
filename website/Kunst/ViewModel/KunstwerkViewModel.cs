using Kunst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kunst.ViewModels
{
    public class KunstwerkViewModel
    {
        public IEnumerable<Kunstwerk> Kunstwerks { get; set; }
        public IEnumerable<DrieDWerken> DrieDwerkens { get; set; }
        public IEnumerable<TweeDWerken> TweeDwerkens { get; set; }     
      
    }
}