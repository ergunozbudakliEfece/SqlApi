using System;

namespace SqlApi.Models
{
    public class DetaysizSSIPModel
    {
        public string SIPARIS_NO { get; set; }
        public DateTime? TARIH { get; set; }
        public DateTime? TESLIM_TARIHI { get; set; }
        public string CARI_ISIM { get; set; }

        public string TESLIM_YERI { get; set; }
        public string CARI_DOVIZ_TIPI { get; set; }
        public string PLASIYER_ADI { get; set; }
        public decimal? GENELTOPLAM { get; set; }
        public decimal? DOV_GENELTOPLAM { get; set; }
        public string DOV_TIP { get; set; }
    }
}
