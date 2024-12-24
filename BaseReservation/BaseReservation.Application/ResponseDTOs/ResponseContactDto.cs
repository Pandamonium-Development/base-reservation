using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseContactDto : BaseEntity
{
    public short Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public byte VendorId { get; set; }

    public bool Active { get; set; }

    public virtual ResponseVendorDto Vendor { get; set; } = null!;
}