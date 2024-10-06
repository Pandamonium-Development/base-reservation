using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServicePaymentType(IRepositoryPaymentType repository, IMapper mapper) : IServicePaymentType
{
    /// <inheritdoc />
    public async Task<ICollection<ResponsePaymentTypeDto>> ListAllAsync()
    {
        var collection = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponsePaymentTypeDto>>(collection);
    }
}