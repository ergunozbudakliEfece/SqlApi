using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlApi.Models
{
    public class Fav
    {
        [Key, Column(Order = 0)]
        public int USER_ID { get; set; }
        [Key, Column(Order = 1)]
        public string FAVORITE_STOKS { get; set; }
       
    }
}
