using AutoMapper;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCliente(IRepositoryCliente repository, IMapper mapper) : IServiceCliente
{
    /// <inheritdoc />
    public async Task<bool> DeleteCustomerAsync(short id)
    {
        if (!await repository.ExistsCustomerAsync(id)) throw new NotFoundException("Cliente no encontrado.");
        return await repository.DeleteCustomerAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseClienteDto?> FindByIdAsync(short id)
    {
        var customer = await repository.FindByIdAsync(id);
        if (customer == null) throw new NotFoundException("Cliente no encontrado.");

        return mapper.Map<ResponseClienteDto>(customer);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseClienteDto>> ListAllAsync()
    {
        var customers = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponseClienteDto>>(customers);
    }
}