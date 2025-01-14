using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace SqlApi.Models
{
    public class MusteriRandevuModel
    {
       
        public int INCKEY { get; set; }
        public string MUSTERI { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PLANLANAN_TARIH { get; set; }
        public string KAYIT_YAPAN_KULLANICI { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? KAYIT_TARIHI { get; set; }
        public string? DEGISIKLIK_YAPAN_KULLANICI { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DEGISIKLIK_TARIHI { get; set; }
        public string? RANDEVU_NOTU { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? GERCEKLESEN_TARIH { get; set; }
        public string PLASIYER { get; set; }
        public string SUREC { get; set; }
        public string ILETISIM_KANALI { get; set; }
    }
}
