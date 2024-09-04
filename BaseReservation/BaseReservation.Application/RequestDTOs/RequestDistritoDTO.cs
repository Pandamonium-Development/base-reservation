namespace BaseReservation.Application.RequestDTOs;

public record RequestDistritoDto
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public byte IdCanton { get; set; }
}
