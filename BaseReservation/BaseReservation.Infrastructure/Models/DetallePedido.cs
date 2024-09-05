using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("DetallePedido")]
[Index("IdPedido", Name = "IX_DetallePedido_IdPedido")]
[Index("IdServicio", Name = "IX_DetallePedido_IdServicio")]
public partial class DetallePedido
{
    [Key]
    public long Id { get; set; }

    public long IdPedido { get; set; }

    public byte? IdServicio { get; set; }

    public short? IdProducto { get; set; }

    public byte NumeroLinea { get; set; }

    public short Cantidad { get; set; }

    [Column(TypeName = "money")]
    public decimal TarifaServicio { get; set; }

    [Column(TypeName = "money")]
    public decimal MontoSubtotal { get; set; }

    [Column(TypeName = "money")]
    public decimal MontoImpuesto { get; set; }

    [Column(TypeName = "money")]
    public decimal MontoTotal { get; set; }

    [InverseProperty("IdDetallePedidoNavigation")]
    public virtual ICollection<DetallePedidoProducto> DetallePedidoProductos { get; set; } = new List<DetallePedidoProducto>();

    [ForeignKey("IdPedido")]
    [InverseProperty("DetallePedidos")]
    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("DetallePedidos")]
    public virtual Producto? IdProductoNavigation { get; set; }

    [ForeignKey("IdServicio")]
    [InverseProperty("DetallePedidos")]
    public virtual Servicio? IdServicioNavigation { get; set; }
}