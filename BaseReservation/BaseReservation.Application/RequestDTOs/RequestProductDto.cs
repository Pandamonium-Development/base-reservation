namespace BaseReservation.Application.RequestDTOs;

public record RequestProductDto : RequestBaseDto
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public byte CategoryId { get; set; }

    public decimal Price { get; set; }

    public string Sku { get; set; } = null!;

    public byte UnitMeasureId { get; set; }

    public bool Active { get; set; }
}
