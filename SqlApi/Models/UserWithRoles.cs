using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    public class UserWithRoles
    {
        [Key, Column(Order = 0)]
        public int USER_ID { get; set; }
        [Key, Column(Order = 1)]
        public int MODULE_INCKEY { get; set; }
        public string USER_NAME { get; set; }
        public string USER_PASSWORD { get; set; }
        public string USER_FIRSTNAME { get; set; }
        public string USER_LASTNAME { get; set; }
        public bool ACTIVE { get; set; }
        public string USER_ROLE { get; set; }
        public string USER_MAIL { get; set; }

        public bool USER_AUTH { get; set; }

        public bool SELECT_AUTH { get; set; }

        public bool INSERT_AUTH { get; set; }

        public bool UPDATE_AUTH { get; set; }

        public bool DELETE_AUTH { get; set; }
        

    }
}
