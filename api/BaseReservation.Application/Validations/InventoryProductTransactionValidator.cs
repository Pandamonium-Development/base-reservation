using FluentValidation;
using BaseReservation.Infrastructure;

namespace BaseReservation.Application.Validations;

public class InventoryProductTransactionValidator : AbstractValidator<InventoryProductTransaction>
{
    public InventoryProductTransactionValidator()
    {
        RuleFor(m => m.TransactionType)
            .IsInEnum().WithMessage("Tipo de movimiento inválido");

        RuleFor(m => m.Quantity)
            .GreaterThan((byte)0).WithMessage("Debe ingresar una cantidad mayor a 0");
    }
}