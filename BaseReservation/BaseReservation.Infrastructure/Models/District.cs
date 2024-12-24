using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("District")]
[Index("CantonId", Name = "IX_District_CantonId")]
public partial class District
{
    [Key]
    public short Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public byte CantonId { get; set; }

    [InverseProperty("DistrictIdNavigation")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [ForeignKey("CantonId")]
    [InverseProperty("Districts")]
    public virtual Canton CantonIdNavigation { get; set; } = null!;

    [InverseProperty("DistrictIdNavigation")]
    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();

    [InverseProperty("DistrictIdNavigation")]
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    [InverseProperty("DistrictIdNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}