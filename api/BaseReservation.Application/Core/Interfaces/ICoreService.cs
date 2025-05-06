using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BaseReservation.Application.Core.Interfaces;

public interface ICoreService<out T>
{
    ILogger<T> Logger { get; }

    IMapper AutoMapper { get; }

    IUnitOfWork UnitOfWork { get; }
}