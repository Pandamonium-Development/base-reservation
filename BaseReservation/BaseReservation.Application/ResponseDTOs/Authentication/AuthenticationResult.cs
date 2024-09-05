namespace BaseReservation.Application.ResponseDTOs.Authentication;

public class AuthenticationResult: TokenModel
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}