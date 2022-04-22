using System.ComponentModel.DataAnnotations;

namespace SqlApi.Models
{
    public class Log
    {
        [Key]
        public int INCKEY { get; set; }
        public string? USER_ID { get; set; }
        public string? USER_NAME { get; set; }
        public int? MODULE_ID { get; set; }
        public int? PROGRAM_ID { get; set; }
        public bool? LOG_DATE { get; set; }
        public string? ACTIVITY_TYPE { get; set; }
        public string? OPERATION { get; set; }
    }
}


