namespace BaseReservation.Application.ResponseDTOs;

public record ResponseReservaServicioDto
{
    public int Id { get; set; }

    public int IdReserva { get; set; }

    public byte IdServicio { get; set; }

    public virtual ResponseReservaDto Reserva { get; set; } = null!;

    public virtual ResponseServicioDto Servicio { get; set; } = null!;
}