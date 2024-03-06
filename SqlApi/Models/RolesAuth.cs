using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    public class RolesAuth
    {
        [Key, Column(Order = 0)]
        public int ROLE_ID { get; set; }
        [Key, Column(Order = 1)]
        public int MODULE_INCKEY { get; set; }
        
        public int MODULE_ID { get; set; }
        public int PROGRAM_ID { get; set; }
        public bool USER_AUTH { get; set; }

        public bool SELECT_AUTH { get; set; }
        public bool INSERT_AUTH { get; set; }
        public bool UPDATE_AUTH { get; set; }
        public bool DELETE_AUTH { get; set; }
        public int? COLUMN_1 { get; set; }
        public int? COLUMN_2 { get; set; }
        public bool? COLUMN_3 { get; set; }
        public bool? COLUMN_4 { get; set; }
        public string? COLUMN_5 { get; set; }
        public string? COLUMN_6 { get; set; }
    }

    

}
