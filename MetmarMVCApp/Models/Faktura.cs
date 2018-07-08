namespace MetmarMVCApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Faktura")]
    public partial class Faktura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Faktura()
        {
            FakturaPrzedmiot = new HashSet<FakturaPrzedmiot>();
        }

        public int Id { get; set; }

        public int? IdKlienta { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }

        public decimal? Suma { get; set; }

        public virtual Klienci Klienci { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FakturaPrzedmiot> FakturaPrzedmiot { get; set; }
    }
}
