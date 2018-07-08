namespace MetmarMVCApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FakturaPrzedmiot")]
    public partial class FakturaPrzedmiot
    {
        public int Id { get; set; }

        public int? IdFaktura { get; set; }

        public int? IdPrzedmiot { get; set; }

        public int? Ilosc { get; set; }

        public int? IloscCzas { get; set; }

        public virtual Faktura Faktura { get; set; }

        public virtual Przedmioty Przedmioty { get; set; }
    }
}
