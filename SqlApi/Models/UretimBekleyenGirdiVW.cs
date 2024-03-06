namespace SqlApi.Models
{
    public class UretimBekleyenGirdiVW
    {
        public int INCKEY { get; set; }
        public string? MAK_KODU { get; set; }
        public string? GIRDI_SERI_NO { get; set; }
        public string? GIRDI_OLCU_BR1 { get; set; }
        public string? GIRDI_OLCU_BR2 { get; set; }
        public string? GIRDI_STOK_KODU { get; set; }
        public string? GIRDI_STOK_ADI { get; set; }
        public decimal? GIRDI_MIKTAR { get; set; }
        public decimal? GIRDI_MIKTAR2 { get; set; }
        public bool? URETILDIMI { get; set; }
        public string? ISEMRINO { get; set; }

    }
}
