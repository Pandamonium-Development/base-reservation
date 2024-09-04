namespace BaseReservation.Application.ResponseDTOs;

public record ResponseSucursalFeriadoDTO
{
    public short Id { get; set; }

    public byte IdFeriado { get; set; }

    public byte IdSucursal { get; set; }

    public DateOnly Fecha { get; set; }

    public short Anno { get; set; }

    public virtual ResponseFeriadoDTO Feriado { get; set; } = null!;

    public virtual ResponseSucursalDTO Sucursal { get; set; } = null!;
}