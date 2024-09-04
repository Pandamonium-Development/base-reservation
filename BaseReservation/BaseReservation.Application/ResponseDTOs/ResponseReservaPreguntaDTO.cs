using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseReservaPreguntaDTO : BaseEntity
{
    public int Id { get; set; }

    public int IdReserva { get; set; }

    public string Pregunta { get; set; } = null!;

    public bool Activo { get; set; }

    public string? Respuesta { get; set; }

    public virtual ResponseReservaDTO Reserva { get; set; } = null!;
}