namespace BaseReservation.Application.RequestDTOs;

public record RequestInvoiceDto : RequestBaseDto
{
    public long Id { get; set; }
    public byte BranchId { get; set; }

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

    public IEnumerable<RequestInvoiceDetailDto>? InvoiceDetails { get; set; }
}