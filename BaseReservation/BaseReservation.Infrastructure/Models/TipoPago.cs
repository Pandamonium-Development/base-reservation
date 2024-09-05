using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("TipoPago")]
public partial class TipoPago
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Descripcion { get; set; } = null!;

    public int Referencia { get; set; }

    [InverseProperty("IdTipoPagoNavigation")]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    [InverseProperty("IdTipoPagoNavigation")]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}