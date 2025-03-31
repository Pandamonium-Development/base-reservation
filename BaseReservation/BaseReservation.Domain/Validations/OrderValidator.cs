using FluentValidation;
using BaseReservation.Infrastructure;

namespace BaseReservation.Domain.Validations;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
    }
}