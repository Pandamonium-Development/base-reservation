using System.Net;
using Microsoft.Extensions.Logging;

namespace BaseReservation.Application.Comunes;

public class BadRequestException: BaseException
{
    public override LogLevel LogLevel { get; set; } = LogLevel.Information;

    public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.NotFound;

    public BadRequestException(string mensaje) : base(mensaje)
    {
    }
}