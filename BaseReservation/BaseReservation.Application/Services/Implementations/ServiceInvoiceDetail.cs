using AutoMapper;
using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceInvoiceDetail(IRepositoryInvoiceDetail repository, IMapper mapper) : IServiceInvoiceDetail
{
    /// <inheritdoc />
    public async Task<ResponseInvoiceDetailDto> FindByIdAsync(long id)
    {
        var invoiceDetail = await repository.FindByIdAsync(id);
        if (invoiceDetail == null) throw new NotFoundException("Detalle Factura no encontrada.");

        return mapper.Map<ResponseInvoiceDetailDto>(invoiceDetail);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInvoiceDetailDto>> ListAllByInvoiceAsync(long invoiceId)
    {
        var list = await repository.ListAllByInvoiceAsync(invoiceId);
        var collection = mapper.Map<ICollection<ResponseInvoiceDetailDto>>(list);

        return collection;
    }
}