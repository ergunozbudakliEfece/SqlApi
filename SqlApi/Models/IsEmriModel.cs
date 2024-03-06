using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    public class IsEmriModel
    {
        public string? HAT_KODU { get; set; }
        public string? STOK_KODU { get; set; }
        public string? STOK_ADI { get; set; }
        public DateTime? TESLIM_TARIHI { get; set; }
        public string? SIPARIS_NO { get; set; }
        public string? CARI_ISIM { get; set; }
        public decimal? MIKTAR { get; set; }
        public int? MIKTAR2 { get; set; }
        public decimal? URETILEN_MIKTAR { get; set; }
        public decimal? URETILEN_MIKTAR2 { get; set; }
        public decimal? URETILECEK_MIKTAR { get; set; }
        public decimal? URETILECEK_MIKTAR2 { get; set; }
    }
}
