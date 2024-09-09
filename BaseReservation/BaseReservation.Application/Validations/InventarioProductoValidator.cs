using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class InventarioProductoValidator : AbstractValidator<InventarioProducto>
{
    public InventarioProductoValidator()
    {
        RuleFor(m => m.Minima)
            .GreaterThan(0).WithMessage("Cantidad mínima debe ser mayor a 0")
            .LessThanOrEqualTo(x => x.Maxima).WithMessage("Cantidad mínima debe ser menor a la cantidad máxima");

        RuleFor(m => m.Maxima)
            .GreaterThan(0).WithMessage("Cantidad máxima debe ser mayor a 0");
    }
}