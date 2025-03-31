namespace BaseReservation.Application.RequestDTOs;

public record RequestInvoiceDetailProductDto : RequestBaseDto
{
    public long InvoiceDetailId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }
}