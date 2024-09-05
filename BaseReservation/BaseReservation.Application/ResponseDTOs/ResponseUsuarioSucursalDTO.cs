namespace BaseReservation.Application.ResponseDTOs;

public record ResponseUsuarioSucursalDto
{
    public short Id { get; set; }

    public short IdUsuario { get; set; }

    public byte IdSucursal { get; set; }

    public virtual ResponseSucursalDto Sucursal { get; set; } = null!;

    public virtual ResponseUsuarioDto Usuario { get; set; } = null!;
}