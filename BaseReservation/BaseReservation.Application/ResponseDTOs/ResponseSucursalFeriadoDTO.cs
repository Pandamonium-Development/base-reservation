namespace BaseReservation.Application.ResponseDTOs;

public record ResponseSucursalFeriadoDto
{
    public short Id { get; set; }

    public byte IdFeriado { get; set; }

    public byte IdSucursal { get; set; }

    public DateOnly Fecha { get; set; }

    public short Anno { get; set; }

    public virtual ResponseFeriadoDto Feriado { get; set; } = null!;

    public virtual ResponseSucursalDto Sucursal { get; set; } = null!;
}