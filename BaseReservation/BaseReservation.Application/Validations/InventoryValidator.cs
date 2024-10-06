using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class InventoryValidator : AbstractValidator<Inventory>
{
    public InventoryValidator()
    {
    }
}