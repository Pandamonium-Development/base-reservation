using AutoMapper;
using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceDistrict(IRepositoryDistrict repository, IMapper mapper) : IServiceDistrict
{
    /// <inheritdoc />
    public async Task<ResponseDistrictDto> FindByIdAsync(byte id)
    {
        var district = await repository.FindByIdAsync(id);
        if (district == null) throw new NotFoundException("District no encontrado.");

        return mapper.Map<ResponseDistrictDto>(district);
    }
    /// <inheritdoc />
    public async Task<ICollection<ResponseDistrictDto>> ListAllByCantonAsync(byte cantonId)
    {
        var list = await repository.ListAllByCantonAsync(cantonId);
        var collection = mapper.Map<ICollection<ResponseDistrictDto>>(list);

        return collection;
    }
}
