namespace BaseReservation.Application.RequestDTOs;

public record RequestTaxDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }
}