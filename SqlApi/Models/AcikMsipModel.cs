using System;

namespace SqlApi.Models
{
    public class AcikMsipModel
    {
        public string FISNO {  get; set; }
        public string TARIH { get; set; }
        public string CARI_ISIM { get; set; }
        public string PLASIYER { get; set; }
        public string STOK_ADI { get; set; }
        public decimal STHAR_GCMIK { get; set; }
        public string OLCU_BR1 { get; set; }
        public decimal STHAR_GCMIK2 { get; set; }
        public string OLCU_BR2 { get; set; }
        public decimal TESLIM_MIKTARI { get; set; }
        public string TESLIMAT_ORANI { get; set; }
        public string DEPO_ISMI { get; set; }
    }
}
