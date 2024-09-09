using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class ReservaValidator : AbstractValidator<Reserva>
{
    public ReservaValidator()
    {
        RuleFor(x => x.Fecha)
            .NotEmpty().WithMessage("Por favor ingrese la fecha")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("La fecha debe ser hoy o una fecha futura");

        RuleFor(x => x.Hora)
            .NotEmpty().WithMessage("Por favor ingrese la hora");

        RuleFor(x => x.IdSucursal)
            .GreaterThan((byte)0).WithMessage("Por favor ingrese un Id de sucursal válido");
    }
}