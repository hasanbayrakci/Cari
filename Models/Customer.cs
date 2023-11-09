using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cari.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Unvan { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
    }
}
