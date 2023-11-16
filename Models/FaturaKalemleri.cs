using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cari.Models
{
    public class FaturaKalemleri
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public required string Tanim { get; set; }
        public string? Ozelligi { get; set; }
        [Required]
        public required int Birimi { get; set; }
        [Required]
        public required Decimal Kdv { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
    }
}
