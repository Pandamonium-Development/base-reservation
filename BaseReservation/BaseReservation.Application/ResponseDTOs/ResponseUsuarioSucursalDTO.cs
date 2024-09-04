namespace BaseReservation.Application.ResponseDTOs;

public record ResponseUsuarioSucursalDTO
{
    public short Id { get; set; }

    public short IdUsuario { get; set; }

    public byte IdSucursal { get; set; }

    public virtual ResponseSucursalDTO Sucursal { get; set; } = null!;

    public virtual ResponseUsuarioDTO Usuario { get; set; } = null!;
}