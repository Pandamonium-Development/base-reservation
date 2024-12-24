using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("User")]
[Index("DistrictId", Name = "IX_User_DistrictId")]
[Index("GenderId", Name = "IX_User_GenderId")]
[Index("RoleId", Name = "IX_User_RoleId")]
public partial class User : BaseModel
{
    [Key]
    public short Id { get; set; }

    [StringLength(50)]
    public string CardId { get; set; } = null!;

    [StringLength(80)]
    public string FirstName { get; set; } = null!;

    [StringLength(80)]
    public string LastName { get; set; } = null!;

    public int Telephone { get; set; }

    [StringLength(150)]
    public string Email { get; set; } = null!;

    public short DistrictId { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    public DateOnly Birthday { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    public byte GenderId { get; set; }

    public bool Active { get; set; }

    [StringLength(200)]
    public string? ProfilePictureUrl { get; set; }

    public byte RoleId { get; set; }

    [ForeignKey("DistrictId")]
    [InverseProperty("Users")]
    public virtual District DistrictIdNavigation { get; set; } = null!;

    [ForeignKey("GenderId")]
    [InverseProperty("Users")]
    public virtual Gender GenderIdNavigation { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role RoleIdNavigation { get; set; } = null!;

    [InverseProperty("UserIdNavigation")]
    public virtual ICollection<TokenMaster> TokenMasters { get; set; } = new List<TokenMaster>();

    [InverseProperty("UserIdNavigation")]
    public virtual ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
}