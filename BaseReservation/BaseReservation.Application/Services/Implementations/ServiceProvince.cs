using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceProvince(IRepositoryProvince repository, IMapper mapper) : IServiceProvince
{
    /// <inheritdoc />
    public async Task<ResponseProvinceDto> FindByIdAsync(byte id)
    {
        var province = await repository.FindByIdAsync(id);
        if (province == null) throw new NotFoundException("Provincia no encontrada.");

        return mapper.Map<ResponseProvinceDto>(province);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseProvinceDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseProvinceDto>>(list);

        return collection;
    }
}