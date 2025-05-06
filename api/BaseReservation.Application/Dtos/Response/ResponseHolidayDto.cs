using BaseReservation.Application.Enums;
using BaseReservation.Application.Dtos.Response.Base;

namespace BaseReservation.Application.Dtos.Response;

public record ResponseHolidayDto : BaseEntity
{
    public string Name { get; set; } = null!;

    public MonthApplication Month { get; set; }

    public byte Day { get; set; }

    public virtual ICollection<ResponseBranchHolidayDto> BranchHolidays { get; set; } = new List<ResponseBranchHolidayDto>();
}