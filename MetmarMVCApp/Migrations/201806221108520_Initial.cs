namespace MetmarMVCApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            //RenameColumn("dbo.Kategorie", "Nazwa", "KAtegoria");
            CreateTable(
                "dbo.Kategorie",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Nazwa = c.String(maxLength: 50),
                        Kategoria = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Przedmioty",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        IdKategorii = c.Int(),
                        Kaucja = c.Decimal(precision: 18, scale: 2),
                        StawkaDzien = c.Decimal(precision: 18, scale: 2),
                        StawkaGodzinowa = c.Decimal(precision: 18, scale: 2),
                        Cena = c.Decimal(precision: 18, scale: 2),
                        IsPrice = c.Int(),
                        Wartosc = c.Int(),
                        IsSki = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategorie", t => t.IdKategorii)
                .Index(t => t.IdKategorii);
            
            CreateTable(
                "dbo.Klienci",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        Pesel = c.String(),
                        Telefon = c.String(),
                        Adres = c.String(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            //RenameColumn("dbo.Kategorie", "Nazwa", "KAtegoria");
            DropForeignKey("dbo.Przedmioty", "IdKategorii", "dbo.Kategorie");
            DropIndex("dbo.Przedmioty", new[] { "IdKategorii" });
            DropTable("dbo.Klienci");
            DropTable("dbo.Przedmioty");
            DropTable("dbo.Kategorie");
        }
    }
}
