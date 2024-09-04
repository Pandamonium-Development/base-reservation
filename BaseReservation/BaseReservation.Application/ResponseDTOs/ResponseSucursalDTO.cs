using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseSucursalDTO : BaseEntity
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Telefono { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public short IdDistrito { get; set; }

    public string? DireccionExacta { get; set; }

    public bool Activo { get; set; }

    public virtual ResponseDistritoDTO? Distrito { get; set; } = null!;

    public virtual ICollection<ResponseInventarioDTO> Inventarios { get; set; } = new List<ResponseInventarioDTO>();

    public virtual ICollection<ResponseSucursalHorarioDTO> SucursalHorarios { get; set; } = new List<ResponseSucursalHorarioDTO>();

    public virtual ICollection<ResponseUsuarioSucursalDTO> UsuarioSucursals { get; set; } = new List<ResponseUsuarioSucursalDTO>();

    public virtual ICollection<ResponseSucursalFeriadoDTO> SucursalFeriados { get; set; } = new List<ResponseSucursalFeriadoDTO>();

    public virtual ICollection<ResponseReservaDTO> Reservas { get; set; } = new List<ResponseReservaDTO>();

    public virtual ICollection<ResponsePedidoDTO> Pedidos { get; set; } = new List<ResponsePedidoDTO>();

    public virtual ICollection<ResponseFacturaDTO> Facturas { get; set; } = new List<ResponseFacturaDTO>();
}