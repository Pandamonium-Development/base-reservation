using BaseReservation.Utils;

namespace BaseReservation.WebAPI;

/// <summary>
/// Class to specify error properties for exceptions
/// </summary>
public class ErrorDetailsBaseReservation
{
    public string Type { get; set; } = string.Empty;

    public int StatusCode { get; set; }

    public string? Message { get; set; }

    public string? Detail { get; set; }

    public LogLevel LogLevel { get; set; }

    public override string ToString() => Serialization.Serialize(this);
}