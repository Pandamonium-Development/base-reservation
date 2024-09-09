using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class InventarioValidator : AbstractValidator<Inventario>
{
    public InventarioValidator()
    {
    }
}