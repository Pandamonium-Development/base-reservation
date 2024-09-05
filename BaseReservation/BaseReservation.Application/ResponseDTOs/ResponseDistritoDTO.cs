namespace BaseReservation.Application.ResponseDTOs;

public record ResponseDistritoDto
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public byte IdCanton { get; set; }

    public virtual ICollection<ResponseClienteDto> Clientes { get; set; } = new List<ResponseClienteDto>();

    public virtual ResponseCantonDto Canton { get; set; } = null!;

    public virtual ICollection<ResponseProveedorDTO> Proveedores { get; set; } = new List<ResponseProveedorDTO>();

    public virtual ICollection<ResponseSucursalDTO> Sucursales { get; set; } = new List<ResponseSucursalDTO>();

    public virtual ICollection<ResponseUsuarioDTO> Usuarios { get; set; } = new List<ResponseUsuarioDTO>();
}