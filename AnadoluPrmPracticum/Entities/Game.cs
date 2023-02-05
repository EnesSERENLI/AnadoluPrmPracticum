using AnadoluPrmPracticum.Validations;
using System.ComponentModel.DataAnnotations;

namespace AnadoluPrmPracticum.Entities
{
    public class Game //Şuanda model kontroleri buradan yapılıyor ancak veri tabanı eklendiğinde DTO'lar oluşturulacak.
    {
        public int ID { get; set; } //Burası primary key olacak buraya özel bir attibute yazmıyorum şuanda. 
        [Required(ErrorMessage = "Lütfen bir oyun adı giriniz.!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen oyun fiyatı giriniz.")]
        [Range(minimum:100,1000,ErrorMessage ="Oyun fiyatı en az 100 TL ve en çok 1000 TL arasında olabilir.")]
        public decimal Price { get; set; }
        [Required]
        [DateTimeValidationAttribute] //CustomAttribute
        public DateTime ReleaseDate { get; set; }
    }
}
