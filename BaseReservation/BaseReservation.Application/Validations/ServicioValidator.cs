using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class ServicioValidator : AbstractValidator<Servicio>
{
    public ServicioValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("Por favor ingrese el nombre")
            .MaximumLength(80).WithMessage("El nombre no puede tener mas de 80 caracteres");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("Por favor ingrese la descripción")
            .MaximumLength(150).WithMessage("El nombre no puede tener mas de 150 caracteres");

        RuleFor(x => x.Observacion)
            .NotEmpty().WithMessage("Por favor ingrese la observación")
            .MaximumLength(150).WithMessage("La observación no puede tener mas de 150 caracteres");

        RuleFor(x => x.Tarifa)
            .NotEmpty().WithMessage("Por favor ingrese el correo electrónico");

        RuleFor(x => x.IdTipoServicio)
            .NotEmpty().WithMessage("Por favor ingrese un Tipo de servicio válido");

    }
}