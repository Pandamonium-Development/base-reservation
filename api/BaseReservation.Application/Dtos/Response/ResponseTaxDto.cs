using BaseReservation.Application.Dtos.Response.Base;

namespace BaseReservation.Application.Dtos.Response;

public record ResponseTaxDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }

    public virtual ICollection<ResponseInvoiceDto> Invoices { get; set; } = new List<ResponseInvoiceDto>();
}