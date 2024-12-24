namespace BaseReservation.Application.ResponseDTOs;

public record ResponseDistrictDto
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public byte CantonId { get; set; }

    public virtual ICollection<ResponseCustomerDto> Customers { get; set; } = new List<ResponseCustomerDto>();

    public virtual ResponseCantonDto Canton { get; set; } = null!;

    public virtual ICollection<ResponseVendorDto> Vendors { get; set; } = new List<ResponseVendorDto>();

    public virtual ICollection<ResponseBranchDto> Branches { get; set; } = new List<ResponseBranchDto>();

    public virtual ICollection<ResponseUserDto> Users { get; set; } = new List<ResponseUserDto>();
}