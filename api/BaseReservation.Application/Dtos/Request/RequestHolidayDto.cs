using BaseReservation.Application.Enums;

namespace BaseReservation.Application.Dtos.Request;

public record RequestHolidayDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public MonthApplication Month { get; set; }

    public byte Day { get; set; }
}