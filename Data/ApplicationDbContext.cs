using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cari.Models;

namespace Cari.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cari.Models.Customer> Customer { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Cari.Models.CariHareket> CariHareket { get; set; } = default!;
        public DbSet<Cari.Models.Fatura> Fatura { get; set; } = default!;
        public DbSet<Cari.Models.FaturaKalemleri> FaturaKalemleri { get; set; } = default!;
        public DbSet<Cari.Models.Birimler> Birimler { get; set; } = default!;
        public DbSet<Cari.Models.FaturaDetay> FaturaDetay { get; set; } = default!;
    }
}