using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseServicioDto : BaseEntity
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public byte IdTipoServicio { get; set; }

    public decimal Tarifa { get; set; }

    public string? Observacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<ResponseDetalleFacturaDTO> DetalleFacturas { get; set; } = new List<ResponseDetalleFacturaDTO>();

    public virtual ResponseTipoServicioDto TipoServicio { get; set; } = null!;

    public virtual ICollection<ResponseReservaServicioDto> ReservaServicios { get; set; } = new List<ResponseReservaServicioDto>();
}