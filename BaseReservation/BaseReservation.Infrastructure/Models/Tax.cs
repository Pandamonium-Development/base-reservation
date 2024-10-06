using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("Tax")]
public partial class Tax
{
    [Key]
    public byte Id { get; set; }

    [StringLength(40)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Rate { get; set; }

    [InverseProperty("TaxIdNavigation")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("TaxIdNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}