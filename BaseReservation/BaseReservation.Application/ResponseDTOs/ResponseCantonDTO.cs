namespace BaseReservation.Application.ResponseDTOs;

public record ResponseCantonDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public byte ProvinceId { get; set; }

    public virtual ICollection<ResponseDistrictDto> Districts { get; set; } = new List<ResponseDistrictDto>();

    public virtual ResponseProvinceDto Province { get; set; } = null!;
}