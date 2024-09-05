using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseServicioDTO : BaseEntity
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public byte IdTipoServicio { get; set; }

    public decimal Tarifa { get; set; }

    public string? Observacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<ResponseDetalleFacturaDto> DetalleFacturas { get; set; } = new List<ResponseDetalleFacturaDto>();

    public virtual ResponseTipoServicioDTO TipoServicio { get; set; } = null!;

    public virtual ICollection<ResponseReservaServicioDTO> ReservaServicios { get; set; } = new List<ResponseReservaServicioDTO>();
}