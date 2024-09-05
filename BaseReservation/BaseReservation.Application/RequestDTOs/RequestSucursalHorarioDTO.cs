namespace BaseReservation.Application.RequestDTOs;

public record RequestSucursalHorarioDto
{
    public short Id { get; set; }

    public byte IdSucursal { get; set; }

    public short IdHorario { get; set; }
}