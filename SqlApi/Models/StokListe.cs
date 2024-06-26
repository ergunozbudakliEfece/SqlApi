﻿namespace SqlApi.Models
{
    public class StokListe
    {
        public string? STOK_KODU { get; set; }
        public string? STOK_ADI { get; set; }
        public string? GRUP_KODU { get; set; }
        public string? GRUP_ISIM { get; set; }
        public string? OLCU_BIRIMI { get; set; }
        public string? OLCU_BIRIMI2 { get; set; }
        public decimal? BAKIYE { get; set; }
        public decimal? AD_BAKIYE { get; set; }
        public decimal? BEKLEYEN_SIPARIS { get; set; }
        public decimal? BEK_SIP_ADET { get; set; }
        public decimal? SATISA_HAZIR { get; set; }
        public decimal? SAT_HZR_ADET { get; set; }
        public decimal? BEKL_IE_MIKTAR { get; set; }
        public decimal? BEKL_IE_AD { get; set; }
        public decimal? FAB_STOK_MIK { get; set; }
        public decimal? FAB_STOK_AD { get; set; }
        public decimal? SATISFIYAT1 { get; set; }
        public decimal? SATISFIYAT2 { get; set; }
        public decimal? SATISFIYAT3 { get; set; }
        public decimal? SATISFIYAT4 { get; set; }
        public decimal? ALISFIYAT1 { get; set; }
        public decimal? ALISFIYAT2 { get; set; }
        public decimal? ALISFIYAT3 { get; set; }
        public decimal? ALISFIYAT4 { get; set; }
    }
}
