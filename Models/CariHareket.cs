using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cari.Models
{
    public class CariHareket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Firma_Id { get; set; }
        public int Islem_Turu { get; set; } //1:Giriş, 2:Çıkış
        public decimal Tutar {  get; set; }
        public decimal Kalan_Tutar { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;

        public Dictionary<int, string> IslemTuruArray { get; } = new Dictionary<int, string>
        {
            { 1, "Giriş" },
            { 2, "Çıkış" }
        };

    }


}
