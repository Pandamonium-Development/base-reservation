namespace BaseReservation.Application.RequestDTOs;

public record RequestInvoiceDetailProductDto
{
    public long Id { get; set; }

    public long InvoiceDetailId { get; set; }

    public short ProductId { get; set; }

    public decimal Quantity { get; set; }
}