using AutoMapper;
using BaseReservation.Application.Common;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceInvoice(IRepositoryInvoice repository, IRepositoryOrder repositoryOrder, IServiceOrder serviceOrder,
                            IMapper mapper, IValidator<Invoice> invoiceValidator) : IServiceInvoice
{
    /// <inheritdoc />
    public async Task<ResponseInvoiceDto> CreateInvoiceAsync(RequestInvoiceDto invoiceDto)
    {
        var invoice = await ValidateInvoice(invoiceDto);

        ResponseOrderDto? pedido = null;
        if (invoiceDto.OrderId != null && await repositoryOrder.ExistsOrderAsync(invoiceDto.OrderId.Value))
        {
            pedido = await serviceOrder.FindByIdAsync(invoiceDto.OrderId.Value);
            pedido.StatusOrderId = 1; // TODO: change this
            invoiceDto.BranchId = pedido.BranchId;
        }

        var result = await repository.CreateAsync(invoice, mapper.Map<Order>(pedido));
        if (result == null) throw new NotFoundException("Factura no creada.");

        return mapper.Map<ResponseInvoiceDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseInvoiceDto> FindByIdAsync(long id)
    {
        var invoice = await repository.FindByIdAsync(id);
        if (invoice == null) throw new NotFoundException("Factura no encontrada.");

        return mapper.Map<ResponseInvoiceDto>(invoice);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInvoiceDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        list = list.OrderByDescending(x => x.Date).ToList();
        var collection = mapper.Map<ICollection<ResponseInvoiceDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validates the provided Invoice data and returns a mapped Invoice entity.
    /// </summary>
    /// <param name="invoiceDto">The data transfer object containing the Invoice information to validate.</param>
    /// <returns>RequestInvoiceDto</returns>
    private async Task<Invoice> ValidateInvoice(RequestInvoiceDto invoiceDto)
    {
        var invoice = mapper.Map<Invoice>(invoiceDto);
        await invoiceValidator.ValidateAndThrowAsync(invoice);
        return invoice;
    }
}