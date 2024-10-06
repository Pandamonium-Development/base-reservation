using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Contact")]
[Index("VendorId", Name = "IX_Contact_VendorId")]
public partial class Contact : BaseModel
{
    [Key]
    public short Id { get; set; }

    [StringLength(80)]
    public string FirstName { get; set; } = null!;

    [StringLength(80)]
    public string LastName { get; set; } = null!;

    public int Telephone { get; set; }

    [StringLength(150)]
    public string Email { get; set; } = null!;

    public byte VendorId { get; set; }

    public bool Active { get; set; }

    [ForeignKey("VendorId")]
    [InverseProperty("Contacts")]
    public virtual Vendor VendorIdNavigation { get; set; } = null!;
}