using System.Net;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace BaseReservation.Application.Comunes;

[Serializable]
public class BaseReservationException: BaseException
{
    public override LogLevel LogLevel { get; set; } = LogLevel.Information;

    public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.Conflict;

    public BaseReservationException(string mensaje) : base(mensaje)
    {
    }

    protected BaseReservationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}