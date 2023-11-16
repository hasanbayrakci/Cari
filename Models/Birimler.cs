using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cari.Models
{
    public class Birimler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public required string Tanim { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
    }
}
