using System.ComponentModel.DataAnnotations;

namespace SqlApi.Models
{
    public class User
    {
        [Key]
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; } = null!;
        public string USER_PASSWORD { get; set; } = null!;
        public string USER_FIRSTNAME { get; set; } = null!;
        public string USER_LASTNAME { get; set; } = null!;
        public bool ACTIVE { get; set; } 
        public string USER_ROLE { get; set; } 
        public string USER_MAIL { get; set; } = null!;



    }
}
