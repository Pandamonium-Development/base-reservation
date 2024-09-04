namespace BaseReservation.Application.RequestDTOs;

public class RequestSucursalHorarioBloqueoDto
{
    public long Id { get; set; }

    public short IdSucursalHorario { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public bool Activo { get; set; }
}