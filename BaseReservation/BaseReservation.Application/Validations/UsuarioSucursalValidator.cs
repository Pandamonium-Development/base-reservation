using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class UsuarioSucursalValidator : AbstractValidator<UsuarioSucursal>
{
    public UsuarioSucursalValidator()
    {
        RuleFor(m => m.IdSucursal)
            .NotEqual((byte)0).WithMessage("Debe indicar una sucursal");

        RuleFor(m => m.IdUsuario)
            .NotEqual((short)0).WithMessage("Debe indicar el usuario");
    }
}