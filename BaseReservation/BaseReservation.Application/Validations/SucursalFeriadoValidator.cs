using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class SucursalFeriadoValidator : AbstractValidator<SucursalFeriado>
{
    public SucursalFeriadoValidator()
    {
        RuleFor(m => m.IdFeriado)
            .NotEmpty().WithMessage("Debe especificar el feriado");

        RuleFor(m => m.IdSucursal)
            .NotEmpty().WithMessage("Debe especificar la sucursal");

        RuleFor(m => m.Anno)
            .GreaterThanOrEqualTo((short)DateTime.Now.Year).WithMessage(m => $"Año({m.Anno}) no puede ser menor al actual");
    }
}