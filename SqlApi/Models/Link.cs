using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    public class Link
    {
        [Key]
        public int ID { get; set; }
        public string SITUATION { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? START_DATE { get; set; }
        public int? DURATION { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? END_DATE { get; set; }
        public int VERIFICATION_ID { get; set; }
    }
}
