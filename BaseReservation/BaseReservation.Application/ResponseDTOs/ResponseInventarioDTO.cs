using System.ComponentModel;
using BaseReservation.Application.ResponseDTOs.Base;
using BaseReservation.Application.ResponseDTOs.Enums;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseInventarioDTO : BaseEntity
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    [DisplayName("Sucursal")]
    public byte IdSucursal { get; set; }

    public TipoInventario TipoInventario { get; set; }

    public bool Activo { get; set; }

    public virtual ResponseSucursalDto Sucursal { get; set; } = null!;

    public virtual ICollection<ResponseInventarioProductoDto> InventarioProductos { get; set; } = new List<ResponseInventarioProductoDto>();
}