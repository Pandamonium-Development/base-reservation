using FluentValidation;
using BaseReservation.Infrastructure;

namespace BaseReservation.Domain.Validations;

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