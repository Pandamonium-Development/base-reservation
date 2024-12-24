using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("UserBranch")]
[Index("BranchId", Name = "IX_UserBranch_BranchId")]
[Index("UserId", Name = "IX_UserBranch_UserId")]
public partial class UserBranch
{
    [Key]
    public short Id { get; set; }

    public short UserId { get; set; }

    public byte BranchId { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("UserBranches")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserBranches")]
    public virtual User UserIdNavigation { get; set; } = null!;
}