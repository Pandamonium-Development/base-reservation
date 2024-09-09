using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class DetalleReservaValidator : AbstractValidator<DetalleReserva>
{
    public DetalleReservaValidator()
    {
      RuleFor(m => m.IdReserva)
        .NotEmpty().WithMessage("Debe especificar la reserva");

      RuleFor(m => m.IdServicio)
          .NotEmpty().WithMessage("Debe especificar el servicio");
    }
}