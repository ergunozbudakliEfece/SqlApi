using System;

namespace SqlApi.Models
{
    public class BarkodModel
    {
        public string SERI_NO { get; set; }
        public string STOK_ADI { get; set; }
        public string GRUP_ISIM { get; set; }
        public string GCKOD { get; set; }
        public string STHAR_GCKOD { get; set; }
        public string STHAR_BGTIP { get; set; }
        public decimal MIKTAR { get; set; }
        public string OLCU_BR1 { get; set; }
        public decimal MIKTAR2 { get; set; }
        public string OLCU_BR2 { get; set; }
        public string SERI_NO_3 { get; set; }
        public string SERI_NO_4 { get; set; }
        public string ACIKLAMA_4 { get; set; }
        public string ACIK2 { get; set; }
        public string ACIK1 { get; set; }
       
        public DateTime TARIH { get; set; }

    }
}
