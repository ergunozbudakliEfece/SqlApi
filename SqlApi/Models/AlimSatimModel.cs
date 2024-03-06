using System;

namespace SqlApi.Models
{
    public class AlimSatimModel
    {
        public string AY { get; set; }
        public int HAFTA { get; set; }
        public string QUARTER { get; set; }

        public string STOK_KODU { get; set; }
        public string STOK_ADI { get; set; }
     
        public string MUSTERI_ISMI { get; set; }
        public DateTime TARIH { get; set; }

        public decimal NET_TUTAR { get; set; }
    }
}
