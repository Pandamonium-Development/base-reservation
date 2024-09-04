using ArtInk.Infraestructure.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestHorarioDto: RequestBaseDto
{
    public short Id { get; set; }

    public DiaSemana Dia { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }
}