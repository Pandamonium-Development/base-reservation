using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class InventarioProductoMovimientoValidator : AbstractValidator<InventarioProductoMovimiento>
{
    public InventarioProductoMovimientoValidator()
    {
        RuleFor(m => m.TipoMovimiento)
            .IsInEnum().WithMessage("Tipo de movimiento inválido");

        RuleFor(m => m.Cantidad)
            .GreaterThan((byte)0).WithMessage("Debe ingresar una cantidad mayor a 0");
    }
}