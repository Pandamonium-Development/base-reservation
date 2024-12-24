namespace BaseReservation.Application.RequestDTOs;

public class RequestUserLoginDto
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}