using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    public class UretimGirdiModel
    {   [Key]
        public int INCKEY { get; set; }
        public string? MAK_KODU { get; set; }
        public string? GIRDI_SERI_NO { get; set; }
        public decimal? GIRDI_MIKTAR { get; set; }
        public decimal? GIRDI_MIKTAR2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TARIH { get; set; }
        public int? URETIM_KAYIT_ID { get; set; }
        public int? URETIM_ONAY_ID { get; set; }
        public bool? URETILDIMI { get; set; }
    }
}
