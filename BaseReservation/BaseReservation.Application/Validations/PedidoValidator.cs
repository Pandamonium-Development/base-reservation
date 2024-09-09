using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class PedidoValidator : AbstractValidator<Pedido>
{
    public PedidoValidator()
    {
    }
}