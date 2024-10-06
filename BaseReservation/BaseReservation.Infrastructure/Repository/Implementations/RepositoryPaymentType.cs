using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryPaymentType(BaseReservationContext context) : IRepositoryPaymentType
{
    /// <inheritdoc />
    public async Task<ICollection<PaymentType>> ListAllAsync() => await context.Set<PaymentType>().AsNoTracking().ToListAsync();
}