using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cari.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Unvan { get; set; }
        public string? VergiDairesi { get; set; }
        public string? VergiNo { get; set; }
        public string? Telefon { get; set; }
        public string? Adres { get; set; }
        public int? IlId { get; set; }
        public int? IlceId { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
    }
}
