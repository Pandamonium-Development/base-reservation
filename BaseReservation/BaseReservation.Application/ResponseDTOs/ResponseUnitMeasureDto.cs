namespace BaseReservation.Application.ResponseDTOs;

public record ResponseUnitMeasureDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public virtual ICollection<ResponseProductDto> Products { get; set; } = new List<ResponseProductDto>();
}