using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseReservaDto : BaseEntity
{
    public int Id { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public byte IdSucursal { get; set; }

    public short IdCliente { get; set; }

    public string NombreCliente { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ResponseSucursalDto Sucursal { get; set; } = null!;

    public virtual ResponseClienteDTO Cliente { get; set; } = null!;

    public virtual ICollection<ResponseReservaPreguntaDto> ReservaPregunta { get; set; } = new List<ResponseReservaPreguntaDto>();

    public virtual ICollection<ResponseReservaServicioDto> ReservaServicios { get; set; } = new List<ResponseReservaServicioDto>();

    public virtual ICollection<ResponsePedidoDTO> Pedidos { get; set; } = new List<ResponsePedidoDTO>();
}