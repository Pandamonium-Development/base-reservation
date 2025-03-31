using BaseReservation.Domain.Core.Models;
using BaseReservation.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("Holiday")]
public partial class Holiday : BaseEntity
{
    [StringLength(80)]
    public string Name { get; set; } = null!;

    [StringLength(25)]
    public Month Month { get; set; }

    public byte Day { get; set; }

    [InverseProperty("HolidayIdNavigation")]
    public virtual ICollection<BranchHoliday> BranchHolidays { get; set; } = new List<BranchHoliday>();
}