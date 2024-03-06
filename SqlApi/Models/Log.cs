using System;
using System.ComponentModel.DataAnnotations;

namespace SqlApi.Models
{
    public class Log
    {
        [Key]
        public int INCKEY { get; set; }
        public int USER_ID { get; set; }
        public string? USER_NAME { get; set; }
        public int? MODULE_ID { get; set; }
        public int? PROGRAM_ID { get; set; }
        public string ACTIVITY_START { get; set; }
        public string? ACTIVITY_TYPE { get; set; }
        public string? TRANSACT { get; set; }
        public string? ACTIVITY_END { get; set; }
       

    }
}


