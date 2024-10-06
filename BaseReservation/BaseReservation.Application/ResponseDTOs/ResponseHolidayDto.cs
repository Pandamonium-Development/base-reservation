using BaseReservation.Application.Enums;
using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseHolidayDto : BaseEntity
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public Month Month { get; set; }

    public byte Day { get; set; }

    public virtual ICollection<ResponseBranchHolidayDto> BranchHolidays { get; set; } = new List<ResponseBranchHolidayDto>();
}