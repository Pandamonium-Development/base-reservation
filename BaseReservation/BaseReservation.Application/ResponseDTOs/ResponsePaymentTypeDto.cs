namespace BaseReservation.Application.ResponseDTOs;

public record ResponsePaymentTypeDto
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public int ReferenceNumber { get; set; }

    public virtual ICollection<ResponseInvoiceDto> Invoices { get; set; } = new List<ResponseInvoiceDto>();
}