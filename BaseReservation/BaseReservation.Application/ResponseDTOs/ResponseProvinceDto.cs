namespace BaseReservation.Application.ResponseDTOs;

public record ResponseProvinceDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ResponseCantonDto> Cantons { get; set; } = new List<ResponseCantonDto>();
}