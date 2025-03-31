using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponsePaymentTypeDto : BaseSimpleEntity
{
    public string Description { get; set; } = null!;

    public int ReferenceNumber { get; set; }

    public virtual ICollection<ResponseInvoiceDto> Invoices { get; set; } = new List<ResponseInvoiceDto>();
}