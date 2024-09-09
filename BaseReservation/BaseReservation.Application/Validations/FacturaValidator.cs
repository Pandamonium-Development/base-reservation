using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class FacturaValidator : AbstractValidator<Factura>
{
    public FacturaValidator()
    {
        RuleFor(x => x.NombreCliente)
            .NotEmpty().WithMessage("Por favor ingrese el nombre del cliente")
            .MaximumLength(80).WithMessage("El nombre no puede tener más de 80 caracteres");
    }
}
