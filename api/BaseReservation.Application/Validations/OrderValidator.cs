using FluentValidation;
using BaseReservation.Infrastructure;

namespace BaseReservation.Application.Validations;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
    }
}