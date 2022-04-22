using System.ComponentModel.DataAnnotations;

namespace SqlApi.Models
{
    public class Roles
    {
        [Key]
        public string ROLE_ID { get; set; }
        public string ROLE_NAME { get; set; }
    }
}
