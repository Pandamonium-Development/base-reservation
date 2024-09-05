using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceHorario
{
    Task<ResponseHorarioDto> CreateHorarioAsync(RequestHorarioDto horarioDto);

    Task<ResponseHorarioDto> UpdateHorarioAsync(RequestHorarioDto horarioDto);

    Task<ICollection<ResponseHorarioDto>> ListAllAsync();

    Task<ResponseHorarioDto?> FindByIdAsync(short id);

    Task<bool> ExistsHorarioAsync(short id);
}
