namespace Admin.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CurussenModel : DbContext
    {
        public CurussenModel()
            : base("name=CurussenModel")
        {
        }

        public virtual DbSet<Cursu> Cursus { get; set; }
        public virtual DbSet<CursusInschrijving> CursusInschrijvings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cursu>()
                .HasMany(e => e.CursusInschrijvings)
                .WithRequired(e => e.Cursu)
                .WillCascadeOnDelete(false);
        }
    }
}
