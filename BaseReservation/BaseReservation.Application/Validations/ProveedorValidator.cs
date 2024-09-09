using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class ProveedorValidator : AbstractValidator<Proveedor>
{
    public ProveedorValidator()
    {
        RuleFor(m => m.Nombre)
            .NotNull().WithMessage("Nombre requerido")
            .NotEmpty().WithMessage("Debe especificar el nombre");

        RuleFor(m => m.CedulaJuridica)
            .NotNull().WithMessage("Cédula jurídica requerido")
            .NotEmpty().WithMessage("Debe especificar la Cédula jurídica");

        RuleFor(m => m.RasonSocial)
            .NotNull().WithMessage("Rason social requerido")
            .NotEmpty().WithMessage("Debe especificar la rason social");

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