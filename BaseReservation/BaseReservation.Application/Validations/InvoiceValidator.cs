using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class InvoiceValidator : AbstractValidator<Invoice>
{
    public InvoiceValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Por favor ingrese el nombre del cliente")
            .MaximumLength(80).WithMessage("El nombre no puede tener más de 80 caracteres");
    }
}
