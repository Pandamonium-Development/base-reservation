using AutoMapper;
using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCustomer(IRepositoryCustomer repository, IMapper mapper) : IServiceCustomer
{
    /// <inheritdoc />
    public async Task<bool> DeleteCustomerAsync(short id)
    {
        if (!await repository.ExistsCustomerAsync(id)) throw new NotFoundException("Cliente no encontrado.");
        return await repository.DeleteCustomerAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseCustomerDto?> FindByIdAsync(short id)
    {
        var customer = await repository.FindByIdAsync(id);
        if (customer == null) throw new NotFoundException("Cliente no encontrado.");

        return mapper.Map<ResponseCustomerDto>(customer);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseCustomerDto>> ListAllAsync()
    {
        var customers = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponseCustomerDto>>(customers);
    }
}