using System.Net;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace BaseReservation.Application.Common;

public abstract class BaseException : Exception
{
    public abstract LogLevel LogLevel { get; set; }

    public abstract HttpStatusCode HttpStatusCode { get; set; }

    protected BaseException(string mensaje) : base(mensaje)
    {
    }

#pragma warning disable SYSLIB0051 // Type or member is obsolete
    protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
#pragma warning restore SYSLIB0051 // Type or member is obsolete
}