namespace BaseReservation.Application.RequestDTOs;

public record RequestTaxDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }
}