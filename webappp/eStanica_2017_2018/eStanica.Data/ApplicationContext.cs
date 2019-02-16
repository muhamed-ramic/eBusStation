using System;
using System.Collections.Generic;
using System.Text;
using eStanica.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace eStanica.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }
        public DbSet<Klijenti> Klijenti { get; set; }
        public DbSet<Autobus> Autobusi { get; set; }
        public DbSet<AutorizacijskiToken> AutorizacijskiToken { get; set; }
        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Karta> Karte { get; set; }
        public DbSet<Kartica> Kartice { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<KretanjeAutobusa> KretanjeAutobusa { get; set; }
        public DbSet<Linije> Linije { get; set; }
        public DbSet<Popust> Popusti { get; set; }
        public DbSet<PosjecujeLokacije> PosjecujeLokacije { get; set; }
        public DbSet<PosjedujeAutobuse> PosjedujeAutobuse { get; set; }
        public DbSet<Prevoznik> Prevoznici { get; set; }
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<SaobracajniDnevnik> SaobracajniDnevnik { get; set; }
        public DbSet<SredstvoPlacanja> SredstvoPlacanja { get; set; }
        public DbSet<Stanica> Stanice { get; set; }
        public DbSet<TipKarte> TipoviKarte { get; set; }
        public DbSet<Transakcija> Transakcije { get; set; }
        public DbSet<TransakcijaStavke> TransakcijaStavke { get; set; }
        public DbSet<Uloge> Uloge { get; set; }
        public DbSet<Uposlenik> Uposlenici { get; set; }
        public DbSet<LinijePopust> PopustNaLiniji { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
