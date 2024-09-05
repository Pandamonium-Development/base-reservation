using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceImpuesto
{
    Task<ICollection<ResponseImpuestoDto>> ListAllAsync();
}
