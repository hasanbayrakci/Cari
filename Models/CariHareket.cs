using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cari.Models
{
    public class CariHareket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Firma")]
        public int CustomerId { get; set; }
        [Display(Name = "İşlem Türü")]
        public int IslemTuru { get; set; } //1:Giriş, 2:Çıkış
        public decimal Tutar {  get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;

        public Dictionary<int, string> IslemTuruArray { get; } = new Dictionary<int, string>
        {
            { 1, "Giriş" },
            { 2, "Çıkış" }
        };

    }


}
