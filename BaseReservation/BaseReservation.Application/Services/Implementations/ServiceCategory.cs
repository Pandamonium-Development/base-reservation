using AutoMapper;
using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCategory(IRepositoryCategory repository, IMapper mapper) : IServiceCategory
{
    /// <inheritdoc />
    public async Task<ResponseCategoryDto> FindByIdAsync(byte id)
    {
        var category = await repository.FindByIdAsync(id);
        if (category == null) throw new NotFoundException("Categoría no se ha encontrado.");

        return mapper.Map<ResponseCategoryDto>(category);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseCategoryDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseCategoryDto>>(list);

        return collection;
    }
}