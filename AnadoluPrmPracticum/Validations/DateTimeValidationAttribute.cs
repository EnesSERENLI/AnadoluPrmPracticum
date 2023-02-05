using AnadoluPrmPracticum.Entities;
using System.ComponentModel.DataAnnotations;

namespace AnadoluPrmPracticum.Validations
{
    public class DateTimeValidationAttribute : ValidationAttribute
    {
        public DateTimeValidationAttribute()
        {
            MaxRelaseDate = DateTime.UtcNow.Date;
        }
        public DateTime MaxRelaseDate { get; set; }

        public string GetErrorMessage() => $"Oyunun çıkış tarihi şuan ki tarihten büyük olamaz. !!!!!";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var game = (Game)validationContext.ObjectInstance;
            var dateIsValid = game.ReleaseDate <= MaxRelaseDate; 

            return dateIsValid ? ValidationResult.Success : new ValidationResult(GetErrorMessage());
        }
    }
}
