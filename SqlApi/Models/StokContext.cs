using Microsoft.EntityFrameworkCore;

namespace SqlApi.Models
{
    public class StokContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public StokContext(DbContextOptions<StokContext> options)
       : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StokListe>().HasNoKey();
        }

        public Microsoft.EntityFrameworkCore.DbSet<StokListe> BTE_VW_STOK_BAKIYE { get; set; }
       
    }
}
