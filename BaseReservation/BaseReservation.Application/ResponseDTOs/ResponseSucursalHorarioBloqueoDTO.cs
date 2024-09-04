namespace BaseReservation.Application.ResponseDTOs;

public record ResponseSucursalHorarioBloqueoDTO
{
    public long Id { get; set; }

    public short IdSucursalHorario { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public bool Activo { get; set; }

    public virtual ResponseSucursalHorarioDTO SucursalHorario { get; set; } = null!;
}