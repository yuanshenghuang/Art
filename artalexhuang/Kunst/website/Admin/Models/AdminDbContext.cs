using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Admin.Models
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext()
            : base("WebConnection")
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            string root = Path.GetFullPath(Path.Combine(path, @"../"));
            string pathCombine = System.IO.Path.Combine(root, @"Kunst\App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", pathCombine);
        }

            
        public DbSet<DrieDWerken> DrieDWerken { get; set; }
        public DbSet<TweeDWerken> TweeDWerken { get; set; }      
        public DbSet<Kunstwerk> KunstWerken { get; set; }

        
    }
}