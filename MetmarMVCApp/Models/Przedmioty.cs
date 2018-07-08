namespace MetmarMVCApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Przedmioty")]
    public partial class Przedmioty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Przedmioty()
        {
            FakturaPrzedmiot = new HashSet<FakturaPrzedmiot>();
        }

        public int Id { get; set; }

        public string Nazwa { get; set; }
        
        public int? IdKategorii { get; set; }

        public decimal Kaucja { get; set; }

        public decimal StawkaDzien { get; set; }

        public decimal StawkaGodzinowa { get; set; }

        public decimal Cena { get; set; }

        public int? IsPrice { get; set; }

        public int? Wartosc { get; set; }

        public int? IsSki { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FakturaPrzedmiot> FakturaPrzedmiot { get; set; }

        public virtual Kategorie Kategorie { get; set; }
    }
}
