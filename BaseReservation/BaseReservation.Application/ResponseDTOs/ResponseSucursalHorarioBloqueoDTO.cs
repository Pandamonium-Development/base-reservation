namespace BaseReservation.Application.ResponseDTOs;

public record ResponseSucursalHorarioBloqueoDto
{
    public long Id { get; set; }

    public short IdSucursalHorario { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public bool Activo { get; set; }

    public virtual ResponseSucursalHorarioDto SucursalHorario { get; set; } = null!;
}