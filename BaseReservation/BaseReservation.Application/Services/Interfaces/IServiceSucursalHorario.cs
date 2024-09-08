using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces
{
    public interface IServiceSucursalHorario
    {
        /// <summary>
        /// Get list of all branch's schedules by branch
        /// </summary>
        /// <param name="idSucursal">Branch id</param>
        /// <returns>ICollection of ResponseSucursalHorarioDto</returns>
        Task<ICollection<ResponseSucursalHorarioDto>> ListAllBySucursalAsync(byte idSucursal);

        /// <summary>
        /// Get branch schedule with specific id
        /// </summary>
        /// <param name="id">Branch scheduel id to look for</param>
        /// <returns>ResponseSucursalHorarioDto</returns>
        Task<ResponseSucursalHorarioDto?> FindByIdAsync(short id);

        /// <summary>
        /// Create branch's schedules
        /// </summary>
        /// <param name="idSucursal">Branch id that receive schedules</param>
        /// <param name="sucursalHorarios">List of Branch's schedules will be added</param>
        /// <returns>bool</returns>
        Task<bool> CreateSucursalHorarioAsync(byte idSucursal, IEnumerable<RequestSucursalHorarioDto> sucursalHorarios);
    }
}