namespace BaseReservation.Application.RequestDTOs;

public record RequestSucursalFeriadoDto
{
    public short Id { get; set; }

    public byte IdFeriado { get; set; }

    public byte IdSucursal { get; set; }

    public DateOnly Fecha { get; set; }

    public short Anno { get; set; }
}