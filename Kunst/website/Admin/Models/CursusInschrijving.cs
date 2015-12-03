namespace Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CursusInschrijving")]
    public partial class CursusInschrijving
    {
        public int CursusInschrijvingId { get; set; }

        [Required]
        [StringLength(20)]
        public string VoorNaam { get; set; }

        [Required]
        [StringLength(20)]
        public string AchterNaam { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string TelefoonGsm { get; set; }

        public int CursusId { get; set; }

        public string datum { get; set; }

        public virtual Cursu Cursu { get; set; }
    }
}
