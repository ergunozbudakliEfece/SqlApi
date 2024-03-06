﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    public class MusteriRandevu
    {
        [Key]
        public int INCKEY { get; set; }
        public int? MUSTERI_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PLANLANAN_TARIH { get; set; }
        public int? KAYIT_YAPAN_KULLANICI_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? KAYIT_TARIHI { get; set; }
        public int? DEGISIKLIK_YAPAN_KULLANICI_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DEGISIKLIK_TARIHI { get; set; }
        public string? RANDEVU_NOTU { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? GERCEKLESEN_TARIH { get; set; }

    }
}