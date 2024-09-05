using AutoMapper;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceDetalleFactura(IRepositoryDetalleFactura repository, IMapper mapper) : IServiceDetalleFactura
{
    public async Task<ResponseDetalleFacturaDto?> FindByIdAsync(long id)
    {
        var detalleFactura = await repository.FindByIdAsync(id);
        if (detalleFactura == null) throw new NotFoundException("Detalle Factura no encontrada.");

        return mapper.Map<ResponseDetalleFacturaDto>(detalleFactura);
    }

    public async Task<ICollection<ResponseDetalleFacturaDto>> ListAllByFacturaAsync(long idFactura)
    {
        var list = await repository.ListAllByFacturaAsync(idFactura);
        var collection = mapper.Map<ICollection<ResponseDetalleFacturaDto>>(list);

        return collection;
    }
}