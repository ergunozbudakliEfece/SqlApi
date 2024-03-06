using Microsoft.EntityFrameworkCore;

namespace SqlApi.Models
{
    public class FavContext : DbContext
    {
        public FavContext(DbContextOptions<FavContext> options)
             : base(options)
        {

        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fav>().HasKey(ba => new { ba.USER_ID, ba.FAVORITE_STOKS });
            modelBuilder.Entity<UretimTakip>().HasNoKey();
            modelBuilder.Entity<IsEmri>().HasNoKey();
            modelBuilder.Entity<MakModel>().HasNoKey();
            modelBuilder.Entity<UretimBekleyen>().HasNoKey();

            modelBuilder.Entity<UretimBekleyenCiktiVW>().HasNoKey();
            modelBuilder.Entity<UretimBekleyenGirdiVW>().HasNoKey();

            modelBuilder.Entity<UretimBekleyenCikti>().HasNoKey();
            modelBuilder.Entity<UretimBekleyenGirdiModel>().HasNoKey();
            modelBuilder.Entity<UretimBekleyenCiktiModel>().HasNoKey();
            modelBuilder.Entity<IESIRANO>().HasNoKey();
            modelBuilder.Entity<StokKartlariModel>().HasNoKey();
            modelBuilder.Entity<MSIPAcik>().HasNoKey();
            modelBuilder.Entity<Cikti_SERI_MODEL>().HasNoKey();
            modelBuilder.Entity<CariModel>().HasNoKey();
            modelBuilder.Entity<OzelKod1>().HasNoKey();
            modelBuilder.Entity<OzelKod2>().HasNoKey();
            modelBuilder.Entity<StokModel>().HasNoKey();
            modelBuilder.Entity<TeklifModel>().HasNoKey();
            modelBuilder.Entity<TeklifMasModel>().HasNoKey();
            modelBuilder.Entity<FatuEkModel>().HasNoKey();
        }
        public DbSet<Cikti_SERI_MODEL> NOVA_VW_CIKTI_SERI_URETME { get; set; }
        public DbSet<CariModel> TBLCASABIT { get; set; }
        public DbSet<StokModel> TBLSTSABIT { get; set; }
        public DbSet<OzelKod1> TBLOZELKOD1 { get; set; }
        public DbSet<OzelKod2> TBLOZELKOD2 { get; set; }
        public DbSet<TeklifModel> TBLTEKLIFTRA { get; set; }
        public DbSet<TeklifMasModel> TBLTEKLIFMAS { get; set; }
        public DbSet<FatuEkModel> TBLFATUEK { get; set; }
        public DbSet<UretimBekleyenGirdiVW> NOVA_VW_URETIM_BEKL_GIRDI_BAKIYE { get; set; }
        public DbSet<MSIPAcik> NOVA_VW_ACIK_MSIP_NO { get; set; }
        public DbSet<UretimBekleyenCiktiVW> NOVA_VW_URETIM_BEKL_CIKTI_BAKIYE { get; set; }
        public DbSet<UretimBekleyenGirdiModel> NOVA_VW_URET_BEKL_GIRDI { get; set; }
        public DbSet<UretimBekleyenCiktiModel> NOVA_VW_URET_BEKL_CIKTI { get; set; }
        public DbSet<StokKartlariModel> NOVA_VW_STOK_KARTLARI { get; set; }
        
        public DbSet<MakModel> NOVA_VW_ACIK_IE_MAK { get; set; }
        public DbSet<IESIRANO> NOVA_VW_IE_SIRANO { get; set; }
        public DbSet<Fav> TBL_NOVA_FAVORITE_STOK { get; set; }
        public DbSet<UretimTakip> NOVA_VW_URETIM_SUREC_TAKIP { get; set; }
        public DbSet<IsEmri> NOVA_VW_SERINO_ISEMRI_TAKIP { get; set; }
    }
}
