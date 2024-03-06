using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using static SqlApi.Controllers.AttendanceController;

namespace SqlApi.Models
{
    public class UserContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UserContext(DbContextOptions<UserContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserWithRoles>().HasKey(ba => new { ba.USER_ID, ba.MODULE_INCKEY });
            modelBuilder.Entity<RolesAuth>().HasKey(ba => new { ba.ROLE_ID, ba.MODULE_INCKEY });
            modelBuilder.Entity<UserAuth>().HasKey(ba => new { ba.ROLE_ID, ba.USER_ID, ba.MODULE_ID, ba.PROGRAM_ID });
            modelBuilder.Entity<MusteriUrunModel>().HasKey(ba => new { ba.URUN_GRUBU, ba.MUSTERI_ID});
            modelBuilder.Entity<LogIn>().HasNoKey();
            modelBuilder.Entity<CityModel>().HasNoKey();
            modelBuilder.Entity<MahalleModel>().HasNoKey();
            modelBuilder.Entity<DistrictModel>().HasNoKey();
            modelBuilder.Entity<STSABITMODEL>().HasNoKey();
            modelBuilder.Entity<UrunGrubuModel>().HasNoKey();
            modelBuilder.Entity<FiatModel>().HasNoKey();
            modelBuilder.Entity<FiyatModel>().HasNoKey();
            modelBuilder.Entity<KurOranModel>().HasNoKey();
        }
        public Microsoft.EntityFrameworkCore.DbSet<PersonalMesaiModel> TBL_HAT_GIRDI { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<PersonalMesaiModel> TBL_PERSONAL_ATTENDANCE { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<PersonalOffdateModel> TBL_PERSONAL_OFFDAY { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Link> TBL_NOVA_LINK { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<KurOranModel> TBL_NOVA_KUR_FAIZ_ORANI { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<MusteriModel> TBL_MUSTERI { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LoginModel> TBL_NOVA_LOGIN { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<KurModel> TBL_NOVA_KUR_YONETIMI { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<FiyatModel> TBL_NOVA_FIYAT_SIRA { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UrunGrubuModel> TBL_URUN_GRUBU { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<FiatModel> NOVA_VW_FIYATGIRISI2 { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<MusteriRandevu> TBL_MUSTERI_RANDEVU { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UretimCiktiModel> TBL_NOVA_URETIM_CIKTI_KAYIT { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UretimGirdiModel> TBL_NOVA_URETIM_GIRDI_KAYIT { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Log> TBL_USER_LOG { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> TBL_USER_DATA { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Roles> TBL_ROLES { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserWithRoles> VW_USER_WITH_ROLES { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Module> TBL_MODULES { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Online> TBL_USER_STATUS { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LogIn> VW_SESSION { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LogInTBL> TBL_USER_SESSION_LOG { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<RolesAuth> TBL_ROLES_AUTH { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Personel> TBL_PERSONAL_DATA { get; set; }
       
        public Microsoft.EntityFrameworkCore.DbSet<MusteriUrunModel> TBL_MUSTERI_URUN_BILGI { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<STSABITMODEL> TBLSTSABIT_NOVA { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserAuth> TBL_USER_AUTH { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<MahalleModel> TBL_MAHALLE { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<CityModel> TBL_CITIES { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<DistrictModel> TBL_DISTRICTS { get; set; }

    }

    public class HatGirdiModel
    {
        [Key]
        public int INCKEY { get; set; }
        public string SERI_NO { get; set; }
        public string HAT_KODU { get; set; }
        public int USER_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DATE { get; set; }
    }

    public class PersonalMesaiModel
    {
        [Key]
        public int? INCKEY { get; set; }
        public int USER_ID { get; set; }
       
        [Column(TypeName = "datetime")]
        public DateTime? DATE { get; set; }
        public string STARTEND { get; set; }
        public int? DEV_ID { get; set; }
        public int? START_INCKEY { get; set;}
    }
}
