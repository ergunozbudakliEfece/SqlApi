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
            modelBuilder.Entity<DetayliSipModel>().HasNoKey();
            modelBuilder.Entity<DetayliMSIP>().HasNoKey();
            modelBuilder.Entity<DetayliSipDetaysizModel>().HasNoKey();
            modelBuilder.Entity<DetaysizSSIPModel>().HasNoKey();
            modelBuilder.Entity<DetayliSSIPModel>().HasNoKey();
            modelBuilder.Entity<AlimSatimModel>().HasNoKey();
            modelBuilder.Entity<BarkodModel>().HasNoKey();
            modelBuilder.Entity<UretSeriNoModel>().HasNoKey();
            modelBuilder.Entity<TerminModel>().HasNoKey();
            modelBuilder.Entity<IsEmriModel>().HasNoKey();
            modelBuilder.Entity<FiatModel>().HasNoKey();
            modelBuilder.Entity<CariModel>().HasNoKey();
            modelBuilder.Entity<DetSipMod>().HasNoKey();
            modelBuilder.Entity<Cikti_SERI_MODEL>().HasNoKey();
        }
        public Microsoft.EntityFrameworkCore.DbSet<Cikti_SERI_MODEL> NOVA_VW_CIKTI_SERI_URETME { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<CariModel> TBLCASABIT { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<IsEmriModel> NOVA_VW_ISEMRI { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<FiatModel> NOVA_VW_FIYATGIRISI { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<BarkodModel> UUR_VW_URETIM_ETIKETTR { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UretSeriNoModel> NOVA_VW_SERI_BILGI { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<TerminModel> NOVA_VW_URETIM_TERMIN { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AlimSatimModel> NOVA_VW_ALIM_SATIM { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<StokListe> NOVA_VW_STOK_BAKIYE { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<DetayliSipModel> NOVA_VW_DETAYLI_URETMSIP { get; set; } 
            public Microsoft.EntityFrameworkCore.DbSet<DetayliMSIP> NOVA_VW_DETAYLI_MSIP { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<DetayliSipDetaysizModel> NOVA_VW_DETAYSIZ_MSIP { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<DetaysizSSIPModel> NOVA_VW_DETAYSIZ_SSIP { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<DetayliSSIPModel> NOVA_VW_DETAYLI_SSIP { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<DetSipMod> URETMSIP { get; set; }

    }
}
