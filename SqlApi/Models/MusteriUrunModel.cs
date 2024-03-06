using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace SqlApi.Models
{
    public class MusteriUrunModel
    {
        [Key, Column(Order = 0)]
        public int URUN_ID { get; set; }
        public string? URUN_GRUBU { get; set; }
        [Key, Column(Order = 1)]
        public int? MUSTERI_ID { get; set; }
        public string? KAYIT_YAPAN_KULLANICI { get; set; }

        public decimal? YILLIK_KULLANIM { get; set; }
        public string? OLCU_BIRIMI { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? KAYIT_TARIHI { get; set; }
        public string? DEGISIKLIK_YAPAN_KULLANICI { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DEGISIKLIK_TARIHI { get; set; }
    }
}