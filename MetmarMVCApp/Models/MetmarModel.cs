namespace MetmarMVCApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MetmarModel : DbContext
    {
        public MetmarModel()
            : base("name=MetmarModel2")
        {
        }

        public virtual DbSet<Faktura> Faktura { get; set; }
        public virtual DbSet<FakturaPrzedmiot> FakturaPrzedmiot { get; set; }
        public virtual DbSet<Kategorie> Kategorie { get; set; }
        public virtual DbSet<Klienci> Klienci { get; set; }
        public virtual DbSet<Przedmioty> Przedmioty { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faktura>()
                .HasMany(e => e.FakturaPrzedmiot)
                .WithOptional(e => e.Faktura)
                .HasForeignKey(e => e.IdFaktura);

            modelBuilder.Entity<Kategorie>()
                .HasMany(e => e.Przedmioty)
                .WithOptional(e => e.Kategorie)
                .HasForeignKey(e => e.IdKategorii);

            modelBuilder.Entity<Klienci>()
                .HasMany(e => e.Faktura)
                .WithOptional(e => e.Klienci)
                .HasForeignKey(e => e.IdKlienta);

            modelBuilder.Entity<Przedmioty>()
                .HasMany(e => e.FakturaPrzedmiot)
                .WithOptional(e => e.Przedmioty)
                .HasForeignKey(e => e.IdPrzedmiot);
        }
    }
}
