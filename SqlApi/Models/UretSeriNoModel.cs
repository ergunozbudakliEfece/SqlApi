namespace SqlApi.Models
{
    public class UretSeriNoModel
    {
        public string SERI_NO { get; set; }
        public decimal TUTAR { get; set; }
        public string DOV_TIP { get; set; }
        public decimal? GIRILEN_USD_KUR { get; set; } 
        public decimal? GIR_KUR_HES_USD_FIYAT { get; set; }
        public decimal? TCMB_DOV_SATIS { get; set; }
        public decimal? TL_USD_HES_FIYAT { get; set; }
    }
}
