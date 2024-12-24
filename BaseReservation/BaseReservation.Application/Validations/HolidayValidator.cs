using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class HolidayValidator : AbstractValidator<Holiday>
{
    public HolidayValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty().WithMessage("Nombre no puede ser vacío")
            .NotNull().WithMessage("Nombre es requerido");

        RuleFor(m => m.Month)
            .IsInEnum();

        RuleFor(m => m.Day)
            .InclusiveBetween((byte)1, (byte)31).WithMessage("Día incorrecto");

    }
}