using AutoMapper;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Application.Validations;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCliente(IRepositoryCliente repository, IMapper mapper, 
                            IValidator<Cliente> customerValidator) : IServiceCliente
{
    /// <inheritdoc />
    public async Task<bool> DeleteCustomerAsync(short id)
    {
        if (!await repository.ExisteCustomerAsync(id)) throw new NotFoundException("Cliente no encontrado.");
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
        var clientes = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponseClienteDto>>(clientes);
    }
}