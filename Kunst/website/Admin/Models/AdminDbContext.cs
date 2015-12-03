using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext()
            : base("WebConnection")
        {

        }

        public DbSet<Afmetingen> Afmetingen { get; set; }        
        public DbSet<DrieDWerken> DrieDWerken { get; set; }
        public DbSet<TweeDWerken> TweeDWerken { get; set; }
        public DbSet<GeschrevenWerken> GeschrevenWerken { get; set; }
        public DbSet<Kunstwerk> KunstWerken { get; set; }

        
    }
}