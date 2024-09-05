using BaseReservation.Application.ResponseDTOs.Base;
using BaseReservation.Infrastructure.Enums;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseHorarioDTO : BaseEntity
{
    public short Id { get; set; }

    public DiaSemana Dia { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public virtual ICollection<ResponseSucursalHorarioDto> SucursalHorarios { get; set; } = new List<ResponseSucursalHorarioDto>();
}