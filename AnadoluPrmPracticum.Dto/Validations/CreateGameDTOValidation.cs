using AnadoluPrmPracticum.Dto.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AnadoluPrmPracticum.Dto.Validations
{
    public class CreateGameDTOValidation : AbstractValidator<CreateGameDTO>
    {
        public CreateGameDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Oyun adı boş geçilemez").MinimumLength(3).MaximumLength(255).WithMessage("Oyun adı min:3, max:255 karakter olabilir!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Oyun fiyatı boş olamaz!").GreaterThanOrEqualTo(100).WithMessage("Minimum oyun tutarı 100 TL olmalıdır.");
            RuleFor(x => x.ReleaseDate).NotEmpty<CreateGameDTO, DateTime>().LessThanOrEqualTo(DateTime.Now.Date).WithMessage("Oyun çıkış tarihi şuanki tarihten büyük olamaz!");
        }
    }
}
