using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    public class UserAuth
    {
        [Key, Column(Order = 0)]
        public int ROLE_ID { get; set; }
        [Key, Column(Order = 1)]
        public int USER_ID { get; set; }
        public int MODULE_INCKEY { get; set; }
        [Key, Column(Order = 2)]
        public int MODULE_ID { get; set; }
        [Key, Column(Order = 3)]
        public int PROGRAM_ID { get; set; }
        public bool USER_AUTH { get; set; }

        public bool SELECT_AUTH { get; set; }

        public bool INSERT_AUTH { get; set; }

        public bool UPDATE_AUTH { get; set; }

        public bool DELETE_AUTH { get; set; }
    }
}
