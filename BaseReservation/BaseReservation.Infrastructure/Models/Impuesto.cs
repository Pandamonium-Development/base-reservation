using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("Impuesto")]
public partial class Impuesto
{
    [Key]
    public byte Id { get; set; }

    [StringLength(40)]
    public string Nombre { get; set; } = null!;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Porcentaje { get; set; }

    [InverseProperty("IdImpuestoNavigation")]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    [InverseProperty("IdImpuestoNavigation")]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}