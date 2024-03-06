namespace SqlApi.Models
{
    public class UretimBekleyenCiktiModel
    {
        public string? MAK_KODU { get; set; }
        public string? CIKTI_STOK_KODU { get; set; }
        public string? CIKTI_STOK_ADI { get; set; }
        public decimal? CIKTI_STOK_MIKTAR { get; set; }
        public decimal? CIKTI_STOK_MIKTAR2 { get; set; }
        public string? OLCU_BR1 { get; set; }
        public string? OLCU_BR2 { get; set; }
        public string? ISEMRINO { get; set; }
        public string? GENISLIK { get; set; }
    }
}
