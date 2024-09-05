namespace BaseReservation.Application.ResponseDTOs;

public partial class ResponseInventarioProductoDto
{
    public long Id { get; set; }

    public short IdInventario { get; set; }

    public short IdProducto { get; set; }

    public decimal Disponible { get; set; }

    public decimal Minima { get; set; }

    public decimal Maxima { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ResponseInventarioDTO Inventario { get; set; } = null!;

    public virtual ResponseProductoDto Producto { get; set; } = null!;
}