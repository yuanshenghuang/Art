namespace Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cursu
    {
        public Cursu()
        {
            CursusInschrijvings = new HashSet<CursusInschrijving>();
        }

        [Key]
        public int CursusId { get; set; }

        [Required]
        [StringLength(20)]
        public string CursusNaam { get; set; }

        [Required]
        [StringLength(50)]
        public string CursusInfo { get; set; }

        public virtual ICollection<CursusInschrijving> CursusInschrijvings { get; set; }
    }
}
