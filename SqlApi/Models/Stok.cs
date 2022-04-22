using System.ComponentModel.DataAnnotations;

namespace SqlApi.Models
{
    public class Stok
    {   [Key]
        public string STOK_KODU { get; set; }
        public string STOK_ADI { get; set; }
        public string GRUP_KODU { get; set; }
        public string GRUP_ISIM { get; set; }
        public string KOD_1 { get; set; }
        public decimal OLCU_BIRIMI { get; set; }
        public decimal BAKIYE { get; set; }
        public decimal AD_BAKIYE { get; set; }
        public decimal TOP_CEVRIM { get; set; }
        public decimal CEVRIM { get; set; }
        public decimal BEKLEYEN_SIPARIS { get; set; }
        public decimal BEK_SIP_ADET { get; set; }
        public decimal SATISA_HAZIR { get; set; }
        public decimal SAT_HZR_ADET { get; set; }
        public decimal TOPTAN_FIYAT { get; set; }
        public decimal PARAKENDE_FIYAT { get; set; }
        public decimal SON_FIAT { get; set; }
        public decimal TOPTAN_ISK { get; set; }
        public decimal PERAKENDE_ISK { get; set; }
        public decimal BEKL_IE_MIKTAR { get; set; }
        public decimal BEKL_IE_AD { get; set; }
        public decimal FAB_STOK_MIK { get; set; }
        public decimal FAB_STOK_AD { get; set; }
    }
}
