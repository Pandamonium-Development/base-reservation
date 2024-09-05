using BaseReservation.Application.ResponseDTOs.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestInventarioDto: RequestBaseDto
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public byte IdSucursal { get; set; }

    public TipoInventario TipoInventario { get; set; }

    public bool Activo { get; set; }
}
