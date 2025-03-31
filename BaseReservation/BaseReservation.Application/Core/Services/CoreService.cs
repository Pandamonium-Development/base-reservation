using AutoMapper;
using Microsoft.Extensions.Logging;
using BaseReservation.Application.Core.Interfaces;

namespace BaseReservation.Application.Core.Services;

public class CoreService<T>(ILogger<T> logger, IMapper autoMapper, IUnitOfWork unitOfWork) : ICoreService<T>
{
    public ILogger<T> Logger { get { return logger; } }
    public IMapper AutoMapper { get { return autoMapper; } }
    public IUnitOfWork UnitOfWork { get { return unitOfWork; } }
}