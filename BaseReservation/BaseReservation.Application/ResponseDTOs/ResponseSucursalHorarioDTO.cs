namespace BaseReservation.Application.ResponseDTOs;

public record ResponseSucursalHorarioDto
{
    public short Id { get; set; }

    public byte IdSucursal { get; set; }

    public short IdHorario { get; set; }

    public virtual ResponseHorarioDTO Horario { get; set; } = null!;

    public virtual ResponseSucursalDto Sucursal { get; set; } = null!;

    public virtual ICollection<ResponseSucursalHorarioBloqueoDto> SucursalHorarioBloqueos { get; set; } = new List<ResponseSucursalHorarioBloqueoDto>();
}