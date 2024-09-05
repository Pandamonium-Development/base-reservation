using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseReservation.Application.Services.Interfaces
{
    public interface IServiceSucursalHorarioBloqueo
    {
        Task<ICollection<ResponseSucursalHorarioBloqueoDto>> ListAllBySucursalHorarioAsync(short idSucursalHorario);

        Task<ResponseSucursalHorarioBloqueoDto> FindByIdAsync(long id);

        Task<ResponseSucursalHorarioBloqueoDto> CreateSucursalHorarioBloqueoAsync(RequestSucursalHorarioBloqueoDto bloqueoDTO);

        Task<bool> CreateSucursalHorarioBloqueoAsync(short idSucursalHorario, IEnumerable<RequestSucursalHorarioBloqueoDto> bloqueos);

        Task<ResponseSucursalHorarioBloqueoDto> UpdateSucursalHorarioBloqueoAsync(long id, RequestSucursalHorarioBloqueoDto bloqueoDTO);
    }
}