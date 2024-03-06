using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    
    public class LogInTBL
    {
        [Key]
        public int INCKEY { get; set; }
       
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LOG_DATETIME { get; set; }
       
        public string ACTIVITY_TYPE { get; set; }

        public string PLATFORM{ get; set; }

    }
}
