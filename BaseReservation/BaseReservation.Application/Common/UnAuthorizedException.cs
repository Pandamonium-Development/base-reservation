using System.Net;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace BaseReservation.Application.Common;

[Serializable]
public class UnAuthorizedException : BaseException
{
    public override LogLevel LogLevel { get; set; } = LogLevel.Information;

    public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.NotFound;

    public UnAuthorizedException(string mensaje) : base(mensaje)
    {
    }

    protected UnAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}