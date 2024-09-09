using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceHorario
{
    /// <summary>
    ///  Creates a new ResponseHorarioDto 
    /// </summary>
    /// <param name="horarioDTO"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseHorarioDto> CreateHorarioAsync(RequestHorarioDto horarioDto);

    /// <summary>
    /// Updates an existing ResponseHorarioDto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="horarioDTO"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseHorarioDto> UpdateHorarioAsync(short id, RequestHorarioDto horarioDto);

    /// <summary>
    /// Lists all ResponseHorarioDto.
    /// </summary>
    /// <returns>ICollection of ResponseHorarioDto</returns>
    Task<ICollection<ResponseHorarioDto>> ListAllAsync();

    /// <summary>
    ///Finds a ResponseHorarioDto by its unique identifier
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseHorarioDto> FindByIdAsync(short id);
}
