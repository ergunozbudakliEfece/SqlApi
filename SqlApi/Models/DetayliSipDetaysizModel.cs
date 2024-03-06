using System;

namespace SqlApi.Models
{
    public class DetayliSipDetaysizModel
    {
        public string? SIPARIS_NO { get; set; }
        public DateTime? TARIH { get; set; }
        public string? CARI_ISIM { get; set; }
        public string? ACIKLAMA { get; set; }
        public string? TESLIM_YERI { get; set; }
        public string? CARI_DOVIZ_TIPI { get; set; }
        public string? PLASIYER_ADI { get; set; }
        public decimal? GENELTOPLAM { get; set; }
        public decimal? DOV_GENELTOPLAM { get; set; }
        public string? DOV_TIP { get; set; }
        public string? URETILECEKMI { get; set; }
        public string? SSIP_CHECK { get; set; }
        public string? OZEL_KOD1 { get; set; }
        public Int16? ODEMEGUNU { get; set; }

    }
}
