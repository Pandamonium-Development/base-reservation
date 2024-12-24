using BaseReservation.Application.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestHolidayDto : RequestBaseDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public Month Month { get; set; }

    public byte Day { get; set; }
}