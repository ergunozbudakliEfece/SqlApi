using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace SqlApi.Models
{
    
    public class UserRole
    {   [Key]
        public int INCKEY { get; set; }
        public Int16 BRANCH_ID { get; set; }

        public Int16 ROLE_ID { get; set; }
       
        public int USER_ID { get; set; }

        public int MODULE_ID { get; set; }

        public int PROGRAM_ID { get; set; }

        public bool USER_AUTH { get; set; }

        public bool SELECT_AUTH { get; set; }

        public bool INSERT_AUTH { get; set; }

        public bool UPDATE_AUTH { get; set; }

        public bool DELETE_AUTH { get; set; }

        

        public int LOG_USER_ID { get; set; }

        public DateTime LOG_DATE { get; set; }

        public int LAST_UPD_USER_ID { get; set; }

        public DateTime LAST_UPD_DATE { get; set; }

    }
}
