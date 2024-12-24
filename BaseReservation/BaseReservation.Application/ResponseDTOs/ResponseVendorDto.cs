using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseVendorDto : BaseEntity
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public string CardId { get; set; } = null!;

    public string SocialReason { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public short DistrictId { get; set; }

    public string? Address { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<ResponseContactDto> Contacts { get; set; } = new List<ResponseContactDto>();

    public virtual ResponseDistrictDto District { get; set; } = null!;
}