using FluentValidation;
using BaseReservation.Infrastructure;

namespace BaseReservation.Domain.Validations;

public class InventoryValidator : AbstractValidator<Inventory>
{
    public InventoryValidator()
    {
    }
}