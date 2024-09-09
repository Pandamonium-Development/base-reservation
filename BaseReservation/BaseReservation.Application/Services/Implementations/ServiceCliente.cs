using AutoMapper;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCliente(IRepositoryCliente repository, IMapper mapper) : IServiceCliente
{
    /// <inheritdoc />
    public async Task<ICollection<ResponseClienteDto>> ListAllAsync()
    {
        var clientes = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponseClienteDto>>(clientes);
    }
}