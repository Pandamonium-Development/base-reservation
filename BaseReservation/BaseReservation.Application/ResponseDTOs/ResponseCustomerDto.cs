using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseCustomerDto : BaseEntity
{
    public short Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Telephone { get; set; }

    public short DistrictId { get; set; }

    public string? Address { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<ResponseInvoiceDto> Invoices { get; set; } = new List<ResponseInvoiceDto>();

    public virtual ICollection<ResponseReservationDto> Reservations { get; set; } = new List<ResponseReservationDto>();

    public virtual ResponseDistrictDto District { get; set; } = null!;
}