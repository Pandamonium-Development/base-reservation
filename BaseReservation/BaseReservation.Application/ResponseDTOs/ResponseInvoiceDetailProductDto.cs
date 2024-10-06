namespace BaseReservation.Application.ResponseDTOs;

public record ResponseInvoiceDetailProductDto
{
    public long Id { get; set; }

    public long InvoiceDetailId { get; set; }

    public short ProductId { get; set; }

    public decimal Quantity { get; set; }

    public virtual ResponseInvoiceDetailDto InvoiceDetail { get; set; } = null!;

    public virtual ResponseProductDto Product { get; set; } = null!;
}