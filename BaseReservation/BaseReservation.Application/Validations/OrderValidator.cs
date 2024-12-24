using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
    }
}