using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Core.DTOs;

namespace TrifoyHaxball.Service.Validations
{
    public class PlayerSaveDtoValidator:AbstractValidator<PlayerSaveDto>
    {
        public PlayerSaveDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} alanı null olamaz").NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
            RuleFor(x => x.Password).NotNull().WithMessage("{PropertyName} alanı null olamaz").NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
            RuleFor(x => x.Password.Length).InclusiveBetween(3,30).WithMessage("{PropertyName} alanı 3-30 karakter arası olmalıdır");
        }
    }
}
