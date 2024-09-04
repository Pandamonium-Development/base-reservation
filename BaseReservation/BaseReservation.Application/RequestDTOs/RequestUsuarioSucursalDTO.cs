namespace BaseReservation.Application.RequestDTOs;

public record RequestUsuarioSucursalDto
{
    public short Id { get; set; }

    public short IdUsuario { get; set; }

    public byte IdSucursal { get; set; }
}