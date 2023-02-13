using AnadoluPrmPracticum.Dto.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnadoluPrmPracticum.Dto.Validations
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(x => x.ID).GreaterThan(0).WithMessage("Kullanıcı ID'si 0 dan büyük olmalıdır!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez!").MinimumLength(3).WithMessage("Kullanıcı adı minimum 3 karakter olmalıdır!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email adres kımı boş geçilemez!").EmailAddress().WithMessage("Email adresi, email adres formatında olmalıdır!");
            RuleFor(x => x.FirstName).MinimumLength(2).MaximumLength(50).WithMessage("İsim minimum 2 karakter ile 50 karakter arasında olmalıdır!");
            RuleFor(x => x.LastName).MinimumLength(3).MaximumLength(50).WithMessage("Soyisim minimum 2 karakter ile 50 karakter arasında olmalıdır!");
            RuleFor(x => x.Role).NotEmpty().WithMessage("Kullanıcı rolü boş geçilemez!").MinimumLength(3).MaximumLength(50).WithMessage("Kullanıcı rolü minimum 2 karakter ile 50 karakter arasında olmalıdır!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre kısmı boş geçilemez!").MinimumLength(6).MaximumLength(20).WithMessage("Şifre uzunluğu 6 karater ile 20 karakter arasında olmalıdır!");
        }
    }
}
