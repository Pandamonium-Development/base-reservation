namespace BaseReservation.Application.ResponseDTOs;

public record ResponseTaxDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }

    public virtual ICollection<ResponseInvoiceDto> Invoices { get; set; } = new List<ResponseInvoiceDto>();
}