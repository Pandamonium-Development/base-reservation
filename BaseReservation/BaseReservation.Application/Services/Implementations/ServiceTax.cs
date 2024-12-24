using AutoMapper;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceTax(IRepositoryTax repository, IMapper mapper) : IServiceTax
{
    /// <inheritdoc />
    public async Task<ICollection<ResponseTaxDto>> ListAllAsync()
    {
        var collection = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponseTaxDto>>(collection);
    }
}