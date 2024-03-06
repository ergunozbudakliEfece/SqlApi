using System.ComponentModel.DataAnnotations;

namespace SqlApi.Models
{
    public class UrunGrubuModel
    {
        [Key]
        public int ID { get; set; }
        public string URUN_GRUBU { get; set; }
    }
}
