using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceImpuesto
{
    /// <summary>
    /// ICollection of ResponseImpuestoDto
    /// </summary>
    /// <returns>ResponseImpuestoDto</returns>
    Task<ICollection<ResponseImpuestoDto>> ListAllAsync();
}
