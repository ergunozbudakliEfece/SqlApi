
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    public class UretimTakip
    {
        public string? KAYIT_TIPI { get; set; }
        public string? SERI_NO { get; set; }
        public string? STOK_KODU { get; set; }
        public string? CARI_ISIM { get; set; }
        public string? STOK_ADI { get; set; }
        public string? GRUP_KODU { get; set; }
        public string? GRUP_ISIM { get; set; }
        public int? SIRA_NO { get; set; }
        public int? STRA_INC { get; set; }
        public DateTime? TARIH { get; set; }
        public string? GCKOD { get; set; }
        public decimal? MIKTAR { get; set; }
        public string? OLCU_BR1 { get; set; }
        public decimal? MIKTAR2 { get; set; }
        public string? OLCU_BR2 { get; set; }
        public string? BELGENO { get; set; }
        public string? BELGETIP { get; set; }
        public string? HARACIK { get; set; }
        public Int16? DEPOKOD { get; set; }
        public string? SIPNO { get; set; }
        public string? KARSISERI { get; set; }
        public string? YEDEK1 { get; set; }
        public string? KALINLIK { get; set; }
        public string? GENISLIK { get; set; }
        public string? MARKA_NO { get; set; }
        public string? KALITE { get; set; }
        public string? MENSEI { get; set; }
        public string? KAPLAMA_KALINLIK { get; set; }
        public string? HAMM_GIR_AGIRLIK { get; set; }
        public decimal? INIT_MIKTAR { get; set; }
        public decimal? AKTARILAN_MIKTAR { get; set; }
        public string? BARKOD { get; set; }
    
        public string? UST1_SERI_NO { get; set; }
        

    }
}
