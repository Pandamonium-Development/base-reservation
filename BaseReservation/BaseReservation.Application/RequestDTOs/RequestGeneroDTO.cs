namespace BaseReservation.Application.RequestDTOs;

public record RequestGeneroDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;
}