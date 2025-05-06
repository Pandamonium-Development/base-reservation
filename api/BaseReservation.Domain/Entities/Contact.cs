using Microsoft.EntityFrameworkCore;
using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("Contact")]
[Index("VendorId", Name = "IX_Contact_VendorId")]
public partial class Contact : BaseEntity
{
    [StringLength(80)]
    public string FirstName { get; set; } = null!;

    [StringLength(80)]
    public string LastName { get; set; } = null!;

    public int Telephone { get; set; }

    [StringLength(150)]
    public string Email { get; set; } = null!;

    public long VendorId { get; set; }

    [ForeignKey("VendorId")]
    [InverseProperty("Contacts")]
    public virtual Vendor VendorIdNavigation { get; set; } = null!;
}