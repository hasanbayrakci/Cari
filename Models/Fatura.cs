using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cari.Models
{
    public class Fatura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Firma")]
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "Sıra No")]
        public int SiraNo { get; set; }
        [Required]
        [Display(Name = "Fatura Tarihi")]
        public DateTime FaturaTarihi {  get; set; }
        public DateTime Tarih {  get; set; } = DateTime.Now;

    }
}
