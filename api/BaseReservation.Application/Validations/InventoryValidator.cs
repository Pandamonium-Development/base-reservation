using FluentValidation;
using BaseReservation.Infrastructure;

namespace BaseReservation.Application.Validations;

public class InventoryValidator : AbstractValidator<Inventory>
{
    public InventoryValidator()
    {
    }
}