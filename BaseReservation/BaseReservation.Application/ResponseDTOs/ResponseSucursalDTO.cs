using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseSucursalDto : BaseEntity
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Telefono { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public short IdDistrito { get; set; }

    public string? DireccionExacta { get; set; }

    public bool Activo { get; set; }

    public virtual ResponseDistritoDto? Distrito { get; set; } = null!;

    public virtual ICollection<ResponseInventarioDto> Inventarios { get; set; } = new List<ResponseInventarioDto>();

    public virtual ICollection<ResponseSucursalHorarioDto> SucursalHorarios { get; set; } = new List<ResponseSucursalHorarioDto>();

    public virtual ICollection<ResponseUsuarioSucursalDto> UsuarioSucursals { get; set; } = new List<ResponseUsuarioSucursalDto>();

    public virtual ICollection<ResponseSucursalFeriadoDto> SucursalFeriados { get; set; } = new List<ResponseSucursalFeriadoDto>();

    public virtual ICollection<ResponseReservaDto> Reservas { get; set; } = new List<ResponseReservaDto>();

    public virtual ICollection<ResponsePedidoDto> Pedidos { get; set; } = new List<ResponsePedidoDto>();

    public virtual ICollection<ResponseFacturaDto> Facturas { get; set; } = new List<ResponseFacturaDto>();
}