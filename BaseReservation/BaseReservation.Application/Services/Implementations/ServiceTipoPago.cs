using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceTipoPago(IRepositoryTipoPago repository, IMapper mapper) : IServiceTipoPago
{
    public async Task<ICollection<ResponseTipoPagoDto>> ListAllAsync()
    {
        var coleccion = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponseTipoPagoDto>>(coleccion);
    }
}