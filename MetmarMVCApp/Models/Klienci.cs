namespace MetmarMVCApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Klienci")]
    public partial class Klienci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Klienci()
        {
            Faktura = new HashSet<Faktura>();
        }

        public int Id { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Pesel { get; set; }

        public string Telefon { get; set; }

        public string Adres { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Faktura> Faktura { get; set; }
    }
}
