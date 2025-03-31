using BaseReservation.Application.Enums;
using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseHolidayDto : BaseEntity
{
    public string Name { get; set; } = null!;

    public MonthApplication Month { get; set; }

    public byte Day { get; set; }

    public virtual ICollection<ResponseBranchHolidayDto> BranchHolidays { get; set; } = new List<ResponseBranchHolidayDto>();
}