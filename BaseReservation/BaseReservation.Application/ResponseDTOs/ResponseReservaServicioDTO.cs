namespace BaseReservation.Application.ResponseDTOs;

public record ResponseReservaServicioDTO
{
    public int Id { get; set; }

    public int IdReserva { get; set; }

    public byte IdServicio { get; set; }

    public virtual ResponseReservaDTO Reserva { get; set; } = null!;

    public virtual ResponseServicioDTO Servicio { get; set; } = null!;
}