using BaseReservation.Application.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestScheduleDto : RequestBaseDto
{
    public WeekDayApplication Day { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }
}