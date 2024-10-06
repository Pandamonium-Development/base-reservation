using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class ReservationDetailValidator : AbstractValidator<ReservationDetail>
{
    public ReservationDetailValidator()
    {
      RuleFor(m => m.ReservationId)
        .NotEmpty().WithMessage("Debe especificar la reserva");

      RuleFor(m => m.ReservationId)
          .NotEmpty().WithMessage("Debe especificar el servicio");
    }
}