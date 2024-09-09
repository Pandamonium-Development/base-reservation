using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class FeriadoValidator : AbstractValidator<Feriado>
{
    public FeriadoValidator()
    {
        RuleFor(m => m.Nombre)
            .NotEmpty().WithMessage("Nombre no puede ser vacío")
            .NotNull().WithMessage("Nombre es requerido");

        RuleFor(m => m.Mes)
            .IsInEnum();

        RuleFor(m => m.Dia)
            .InclusiveBetween((byte)1, (byte)31).WithMessage("Día incorrecto");

    }
}