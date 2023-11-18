using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cari.Models
{
    public class FaturaDetay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FaturaId { get; set; }
        [Display(Name ="Fatura Kalemi")]
        public int FaturaKalemleriId { get; set; }
        [Display(Name = "Özelliği")]
        public string? Ozelligi { get; set; }
        [Display(Name = "Birimi")]
        public int BirimlerId { get; set; }
        public decimal Kdv { get; set; }
        public decimal Miktar { get; set; }
        [Display(Name = "Birim Fiyat")]
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
    }
}
