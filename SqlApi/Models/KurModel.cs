using System;
using System.ComponentModel.DataAnnotations;

namespace SqlApi.Models
{
    public class KurModel
    {
        [Key] 
        public int ID { get; set; }
        public decimal ALT_LIMIT { get; set; }
        public decimal UST_LIMIT { get; set; }
        public int BAZ_PARA_BR { get; set; }
        public int KARSIT_PARA_BR { get; set; }
        public decimal KUR { get; set; }
        public decimal VADE { get; set; }
        public string VADE_BR { get; set; }
        public DateTime KAYIT_TARIHI { get; set; }
        public DateTime DEGISIKLIK_TARIHI { get; set; }
        public int DEGISIKLIK_YAPAN_ID { get; set; }
        public int KAYIT_YAPAN_ID { get; set; }
    }
}
