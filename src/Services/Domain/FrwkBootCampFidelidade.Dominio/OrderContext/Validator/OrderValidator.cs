using FluentValidation;
using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Dominio.OrderContext.Validator
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(c => c.CPF)
                .Length(1, 14).WithMessage("Mínimo de 1 e máximo 14")
                .NotEmpty().WithMessage("CPF é obrigatório")
                .NotNull().WithMessage("CPF é obrigatório");
        }
    }
}
