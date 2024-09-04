namespace BaseReservation.Application.RequestDTOs;

public record RequestCantonDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public byte IdProvincia { get; set; }
}