using Dominio;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.DBContext
{
    public class AppDbContext : DbContext
    {
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{}

        public DbSet<WeekCryptoCurrency> WeekCryptoCurrency { get; set; }
        public DbSet<CryptoCurrency> CryptoCurrency { get; set; }
        public DbSet<CryptoCurrencyHistorical> CryptoCurrencyHistorical { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=CoinMarketCap;User Id=sa;Password=ODEGVUOVNU95pkoowvllgv20;")
                .LogTo(message => System.Diagnostics.Debug.WriteLine(message)); // Adicione esta linha para logar consultas;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<WeekCryptoCurrency>()
                .HasOne(week => week.CryptoCurrency)
                .WithMany()
                .HasForeignKey(week => week.CryptoCurrencyId);
        }
    }

}
