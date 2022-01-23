using FluentValidation;
using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;

namespace FrwkBootCampFidelidade.Dominio.WalletContext.Validator
{
    public class WalletTypeValidator : AbstractValidator<WalletType>
    {
        public WalletTypeValidator()
        {
            RuleFor(x => x.Name)
                .Length(5, 30).WithMessage("Tamanho mínimo de 5 e máximo de 30.");
            
        }
    }
}
