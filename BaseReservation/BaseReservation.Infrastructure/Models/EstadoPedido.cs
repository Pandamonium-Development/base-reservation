using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("EstadoPedido")]
public partial class EstadoPedido
{
    [Key]
    public byte Id { get; set; }

    [StringLength(25)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdEstadoPedidoNavigation")]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}