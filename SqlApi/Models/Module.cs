using System.ComponentModel.DataAnnotations;

namespace SqlApi.Models
{
    public class Module
    {
        
            [Key]
            public int INCKEY { get; set; }
            public int MODULE_ID { get; set; }
            public string MODULE_NAME { get; set; }
            public int PROGRAM_ID { get; set; }
            public string PROGRAM_NAME { get; set; }
            public string ACTIVE { get; set; }
    }
}
