namespace SqlApi.Models
{
    public class UretimBekleyen
    {
        public string? INCKEY { get; set; }
        public string? MAK_KODU { get; set; }
        public string? IS_EMRI_NO { get; set; }
        public string? GIRDI_SERI_NO { get; set; }
        public decimal? GIRDI_MIKTAR { get; set; }
        public decimal? GIRDI_MIKTAR2 { get; set; }
        public string? GIRDI_OLCU_BR1 { get; set; }
        public string? GIRDI_OLCU_BR2 { get; set; }
        public decimal? SERI_BAKIYE { get; set; }
        public string? CIKTI_STOK_KODU { get; set; }
        public string? CIKTI_STOK_ADI { get; set; }
        public decimal? CIKTI_STOK_MIKTAR { get; set; }
        public decimal? CIKTI_STOK_MIKTAR2 { get; set; }
    }
}
