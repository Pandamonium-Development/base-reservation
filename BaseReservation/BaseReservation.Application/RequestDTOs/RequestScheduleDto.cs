using BaseReservation.Application.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestScheduleDto : RequestBaseDto
{
    public short Id { get; set; }

    public WeekDay Day { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }
}