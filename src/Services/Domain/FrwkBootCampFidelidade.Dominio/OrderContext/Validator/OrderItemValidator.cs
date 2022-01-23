using FluentValidation;
using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;

namespace FrwkBootCampFidelidade.Dominio.OrderContext.Validator
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(c => c.OrderId)
                .NotEmpty().WithMessage("OrderId é obrigatório")
                .NotNull().WithMessage("OrderId é obrigatório");

            RuleFor(c => c.ProductId)
                .NotEmpty().WithMessage("ProductId é obrigatório")
                .NotNull().WithMessage("ProductId é obrigatório");

            RuleFor(c => c.Quantity)
                .NotEmpty().WithMessage("Quantity é obrigatório")
                .NotNull().WithMessage("Quantity é obrigatório");

        }
    }
}
