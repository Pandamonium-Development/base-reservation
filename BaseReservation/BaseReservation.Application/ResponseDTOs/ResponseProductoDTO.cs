using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseProductoDTO : BaseEntity
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public byte IdCategoria { get; set; }

    public decimal Costo { get; set; }

    public string Sku { get; set; } = null!;

    public byte IdUnidadMedida { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<ResponseDetalleFacturaProductoDto> DetalleFacturaProductos { get; set; } = new List<ResponseDetalleFacturaProductoDto>();

    public virtual ResponseCategoriaDto Categoria { get; set; } = null!;

    public virtual ResponseUnidadMedidaDTO UnidadMedida { get; set; } = null!;

    public virtual ICollection<ResponseInventarioDto> Inventarios { get; set; } = new List<ResponseInventarioDto>();

    public virtual ICollection<ResponseInventarioProductoDto> InventarioProductos { get; set; } = new List<ResponseInventarioProductoDto>();
}