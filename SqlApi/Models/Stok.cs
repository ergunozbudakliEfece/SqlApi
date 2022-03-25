namespace SqlApi.Models
{
    public class Stok
    {
        public string STOK_KODU { get; set; }
        public string STOK_ADI { get; set; }
        public string GRUP_KODU { get; set; }
        public string GRUP_ISIM { get; set; }
        public string KOD_1 { get; set; }
        public string OLCU_BIRIMI { get; set; }
        public string BAKIYE { get; set; }
        public string AD_BAKIYE { get; set; }
        public string TOP_CEVRIM { get; set; }
        public string CEVRIM { get; set; }
        public string BEKLEYEN_SIPARIS { get; set; }
        public string BEK_SIP_ADET { get; set; }
        public string SATISA_HAZIR { get; set; }
        public string SAT_HZR_ADET { get; set; }
        public string TOPTAN_FIYAT { get; set; }
        public string PARAKENDE_FIYAT { get; set; }
        public string SON_FIAT { get; set; }
        public string TOPTAN_ISK { get; set; }
        public string PERAKENDE_ISK { get; set; }
        public string BEKL_IE_MIKTAR { get; set; }
        public string BEKL_IE_AD { get; set; }
        public string FAB_STOK_MIK { get; set; }
        public string FAB_STOK_AD { get; set; }
    }
}
