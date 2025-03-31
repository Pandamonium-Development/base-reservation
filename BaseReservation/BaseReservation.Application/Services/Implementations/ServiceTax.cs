using AutoMapper;
using BaseReservation.Infrastructure;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceTax(ICoreService<Tax> coreService, IMapper mapper) : IServiceTax
{
    /// <inheritdoc />
    public async Task<ICollection<ResponseTaxDto>> ListAllAsync()
    {
        var taxes = await coreService.UnitOfWork.Repository<Tax>().ListAllAsync();
        return mapper.Map<ICollection<ResponseTaxDto>>(taxes);
    }
}