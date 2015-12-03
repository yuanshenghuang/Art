using Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.ViewModels
{
    public class InschrijvingenViewModel
    {
        public IEnumerable<Cursu> Cursus { get; set; }
        public IEnumerable<CursusInschrijving> Inschrijvingen { get; set; }
    }
}