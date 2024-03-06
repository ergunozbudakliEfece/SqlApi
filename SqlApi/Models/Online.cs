using System.ComponentModel.DataAnnotations;

namespace SqlApi.Models
{
    public class Online
    {[Key]
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string ONLINE_STATUS { get; set; }
    }
}
