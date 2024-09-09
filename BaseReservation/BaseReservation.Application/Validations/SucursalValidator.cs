using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class SucursalValidator : AbstractValidator<Sucursal>
{
    public SucursalValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("Por favor ingrese el nombre")
            .MaximumLength(80).WithMessage("El nombre no puede tener mas de 80 caracteres");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("Por favor ingrese la descripción")
            .MaximumLength(150).WithMessage("El nombre no puede tener mas de 150 caracteres");

        RuleFor(x => x.Telefono)
            .NotEmpty().WithMessage("Por favor ingrese el teléfono");

        RuleFor(x => x.CorreoElectronico)
            .NotEmpty().WithMessage("Por favor ingrese el correo electrónico")
            .EmailAddress().WithMessage("Por favor ingrese una dirección de correo electrónico válido");

        RuleFor(x => x.IdDistrito)
            .NotEmpty().WithMessage("Por favor ingrese un distrito válido");

        RuleFor(x => x.DireccionExacta)
            .NotEmpty().WithMessage("Por favor ingrese un distrito válido")
            .When(x => x.DireccionExacta != null);
    }
}