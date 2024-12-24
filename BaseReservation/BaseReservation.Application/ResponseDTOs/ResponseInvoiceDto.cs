using BaseReservation.Application.ResponseDTOs.Base;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseInvoiceDto : BaseEntity
{
    public long Id { get; set; }

    public short CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public long? OrderId { get; set; }

    public DateOnly Date { get; set; }

    public byte PaymentTypeId { get; set; }

    public short Number { get; set; }

    public byte TaxId { get; set; }

    public decimal TaxRate { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public byte BranchId { get; set; }

    public virtual ICollection<ResponseInvoiceDetailDto> InvoiceDetails { get; set; } = new List<ResponseInvoiceDetailDto>();

    public virtual ResponseCustomerDto Customer { get; set; } = null!;

    public virtual ResponseTaxDto TaxInfo { get; set; } = null!;

    public virtual ResponsePaymentTypeDto PaymentType { get; set; } = null!;

    public virtual ResponseOrderDto? Order { get; set; } = null!;

    public virtual Branch Branch { get; set; } = null!;
}