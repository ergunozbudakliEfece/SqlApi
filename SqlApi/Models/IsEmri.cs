using System;

namespace SqlApi.Models
{
    public class IsEmri
    {
        public string? ISEMRINO { get; set; }
        public string? HAMM_SERI_NO { get; set; }
        public string? HAMM_KODU { get; set; }
        public string? HAMM_STOK_ADI { get; set; }
        public string? HAMM_GRUP_KODU { get; set; }
        public string? HAMM_GRUP_ISIM { get; set; }
        public string? HAMM_KALINLIK { get; set; }
        public string? HAMM_GENISLIK { get; set; }
        public string? HAMM_OLCU_BR1 { get; set; }
        public decimal? HAMM_MIKTAR { get; set; } = 0;
        public decimal? HAMM_MIKTAR2 { get; set; } = 0;
        public string? FIRMA_BOBINNO { get; set; }
        public string? HAMM_KALITE { get; set; }
        public string? HAMM_KAPLAMA_KALINLIK { get; set; }
        public string? HAMM_MENSEI { get; set; }
        public DateTime? TARIH { get; set; }
        public string? STOK_KODU { get; set; }
        public string? STOK_ADI { get; set; }
        public string? STOK_GRUP_KODU { get; set; }
        public string? STOK_GRUP_ISIM { get; set; }
        public decimal? MIKTAR { get; set; } = 0;
        public int? MIKTAR2 { get; set; } = 0;
        public decimal? HAMM_SARF_MIKTAR1 { get; set; } = 0;
        public string? HAMM_SARF_MIKTAR2 { get; set; }
        public string? GENISLIK { get; set; }
        public DateTime? TESLIM_TARIHI { get; set; }
        public string? SIPARIS_NO { get; set; }
        public string? KAPALI { get; set; }
        public string? CARI_ISIM { get; set; }
        public string? PROJE_KODU { get; set; }
        public string? REVNO { get; set; }
        public int? ONCELIK { get; set; } = 0;

        public string? REFISEMRINO { get; set; }
        public string? REF_STOK_KODU { get; set; }
        public string? REF_STOK_ADI { get; set; }
        public decimal? REF_MIKTAR { get; set; }
        public string? REF_OLCU_BR1 { get; set; }
        public int? REF_MIKTAR2 { get; set; } = 0;
        public string? REF_OLCU_BR2 { get; set; }

        public string? YAPKOD { get; set; }
        public Int16? SIPKONT { get; set; }
        public string? TEPEMAM { get; set; }
        public string? TEPEYAPKOD { get; set; }
        public DateTime? TEPETARIH { get; set; }
        public DateTime? CALISMAZAMANI { get; set; }
        public string? TEPESIPNO { get; set; }
        public Int16? TEPESIPKONT { get; set; }
        public string? ONAYTIPI { get; set; }
        public int? ONAYNUM { get; set; } = 0;
        public Int16? SUBEKODU { get; set; }
        public string? REWORK { get; set; }
        public Int16? DEPO_KODU { get; set; }
        public string? SERINO { get; set; }
        public string? FASONCARI { get; set; }
        public Int16? CIKIS_DEPO_KODU { get; set; }
        public decimal? ASORTIKOD { get; set; } = 0;
        public int? ISEMRI_SIRA { get; set; } = 0;
        public string? USK_STATUS { get; set; }

        public string? REZERVASYON_STATUS { get; set; }

        public int? SIRA_ONCELIK { get; set; } = 0;
        public int? USTISEMRI_ID { get; set; } = 0;
        public DateTime? BASLAYABILECEGI_TARIH { get; set; }
        
        public string? HAT_KODU { get; set; }
        public string? OLCU_BR1 { get; set; }
        public string? OLCU_BR2 { get; set; }

    }
}
