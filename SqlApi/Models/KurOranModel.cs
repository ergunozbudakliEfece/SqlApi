using System;
using System.ComponentModel.DataAnnotations;

namespace SqlApi.Models
{
    public class KurOranModel
    {
        public int INCKEY { get; set; }
        public decimal KUR { get; set; }
        public decimal FAIZ_ORANI { get; set; }
        public DateTime KAYIT_TARIHI { get; set; }
    }
}
