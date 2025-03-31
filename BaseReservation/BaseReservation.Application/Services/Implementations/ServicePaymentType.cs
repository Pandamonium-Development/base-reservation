using AutoMapper;
using BaseReservation.Infrastructure;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServicePaymentType(ICoreService<PaymentType> coreService, IMapper mapper) : IServicePaymentType
{
    /// <inheritdoc />
    public async Task<ICollection<ResponsePaymentTypeDto>> ListAllAsync()
    {
        var paymentTypes = await coreService.UnitOfWork.Repository<PaymentType>().ListAllAsync();
        return mapper.Map<ICollection<ResponsePaymentTypeDto>>(paymentTypes);
    }
}