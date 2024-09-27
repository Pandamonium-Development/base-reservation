using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Customer")]
[Index("DistrictId", Name = "IX_Customer_DistrictId")]
public partial class Customer : BaseModel
{
    [Key]
    public short Id { get; set; }

    [StringLength(80)]
    public string Name { get; set; } = null!;

    [StringLength(80)]
    public string LastName { get; set; } = null!;

    [StringLength(150)]
    public string Email { get; set; } = null!;

    public int Telephone { get; set; }

    public short DistrictId { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    public bool Active { get; set; }

    [InverseProperty("CustomerIdNavigation")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("DistrictId")]
    [InverseProperty("Customers")]
    public virtual District DistrictIdNavigation { get; set; } = null!;

    [InverseProperty("CustomerIdNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("CustomerIdNavigation")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}