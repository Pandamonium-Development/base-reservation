using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseReservation.Infrastructure.Enums;

namespace BaseReservation.Infrastructure.Models;

[Table("Holiday")]
public partial class Holiday : BaseModel
{
    [Key]
    public byte Id { get; set; }

    [StringLength(80)]
    public string Name { get; set; } = null!;

    [StringLength(25)]
    public Month Month { get; set; }

    public byte Day { get; set; }

    public bool Active { get; set; }

    [InverseProperty("HolidayIdNavigation")]
    public virtual ICollection<BranchHoliday> BranchHolidays { get; set; } = new List<BranchHoliday>();
}