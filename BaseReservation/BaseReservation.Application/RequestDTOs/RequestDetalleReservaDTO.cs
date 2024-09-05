namespace BaseReservation.Application.RequestDTOs;

public record RequestDetalleReservaDto
{
    public int Id { get; set; }

    public int IdReserva { get; set; }

    public byte IdServicio { get; set; }
}