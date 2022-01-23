using FluentValidation;
using FrwkBootCampFidelidade.Dominio.RansomContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Dominio.RansomContext.Validator
{
    public class RansomValidator : AbstractValidator<Ransom>
    {
        public RansomValidator()
        {
            RuleFor(x => x.CPF)
                .Length(1, 14).WithMessage("Mínimo de 1 e máximo de 14");

            RuleFor(x => x.Beneficiary).Length(0,50);

            RuleFor(x => x.Agency)
                .Length(1, 4).WithMessage("Mínimo de 1 e máximo de 4");

        }
    }
}
