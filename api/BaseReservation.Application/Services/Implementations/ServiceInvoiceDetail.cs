using AutoMapper;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.Dtos.Response;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceInvoiceDetail(ICoreService<InvoiceDetail> coreService, IMapper mapper) : IServiceInvoiceDetail
{
    /// <inheritdoc />
    public async Task<ResponseInvoiceDetailDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<InvoiceDetail>().ExistsAsync(id)) throw new NotFoundException("Detalle Factura no encontrado.");

        var spec = new BaseSpecification<InvoiceDetail>(x => x.Id == id);
        var invoiceDetail = await coreService.UnitOfWork.Repository<InvoiceDetail>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseInvoiceDetailDto>(invoiceDetail);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInvoiceDetailDto>> ListAllByInvoiceAsync(long invoiceId)
    {
        var spec = new BaseSpecification<InvoiceDetail>(x => x.InvoiceId == invoiceId);
        var invoiceDetails = await coreService.UnitOfWork.Repository<InvoiceDetail>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseInvoiceDetailDto>>(invoiceDetails);
    }
}