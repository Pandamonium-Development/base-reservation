namespace BaseReservation.Application.ResponseDTOs;

public record ResponseDistritoDto
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public byte IdCanton { get; set; }

    public virtual ICollection<ResponseClienteDto> Clientes { get; set; } = new List<ResponseClienteDto>();

    public virtual ResponseCantonDto Canton { get; set; } = null!;

    public virtual ICollection<ResponseProveedorDto> Proveedores { get; set; } = new List<ResponseProveedorDto>();

    public virtual ICollection<ResponseSucursalDto> Sucursales { get; set; } = new List<ResponseSucursalDto>();

    public virtual ICollection<ResponseUsuarioDto> Usuarios { get; set; } = new List<ResponseUsuarioDto>();
}