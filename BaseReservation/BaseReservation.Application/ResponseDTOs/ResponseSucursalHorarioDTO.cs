namespace BaseReservation.Application.ResponseDTOs;

public record ResponseSucursalHorarioDTO
{
    public short Id { get; set; }

    public byte IdSucursal { get; set; }

    public short IdHorario { get; set; }

    public virtual ResponseHorarioDto Horario { get; set; } = null!;

    public virtual ResponseSucursalDTO Sucursal { get; set; } = null!;

    public virtual ICollection<ResponseSucursalHorarioBloqueoDTO> SucursalHorarioBloqueos { get; set; } = new List<ResponseSucursalHorarioBloqueoDTO>();
}