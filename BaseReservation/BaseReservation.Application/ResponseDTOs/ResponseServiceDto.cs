using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseServiceDto : BaseEntity
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte TypeServiceId { get; set; }

    public decimal Price { get; set; }

    public string? Observation { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<ResponseInvoiceDetailDto> InvoiceDetails { get; set; } = new List<ResponseInvoiceDetailDto>();

    public virtual ResponseTypeServiceDto TypeService { get; set; } = null!;

    public virtual ICollection<ResponseReservationDetailDto> DetalleReservas { get; set; } = new List<ResponseReservationDetailDto>();
}