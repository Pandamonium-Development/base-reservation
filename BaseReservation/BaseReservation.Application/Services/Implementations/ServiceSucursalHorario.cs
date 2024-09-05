using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceSucursalHorario(IRepositorySucursalHorario repository, IMapper mapper,
                                    IValidator<SucursalHorario> sucursalHorarioValidator) : IServiceSucursalHorario
{
    public async Task<bool> CreateSucursalHorarioAsync(byte idSucursal, IEnumerable<RequestSucursalHorarioDto> sucursalHorarios)
    {
        var horarios = await ValidateHorarios(idSucursal, sucursalHorarios);

        var result = await repository.CreateSucursalHorariosAsync(idSucursal, horarios);
        if (!result) throw new ListNotAddedException("Error al guardar horarios.");

        return result;
    }

    public async Task<ResponseSucursalHorarioDto?> FindByIdAsync(short id)
    {
        var sucursalHorario = await repository.FindByIdAsync(id);
        if (sucursalHorario == null) throw new NotFoundException("Horario en sucursal no encontrado.");

        return mapper.Map<ResponseSucursalHorarioDto>(sucursalHorario);
    }

    public async Task<ICollection<ResponseSucursalHorarioDto>> ListAllBySucursalAsync(byte idSucursal)
    {
        var list = await repository.ListAllBySucursalAsync(idSucursal);

        var collection = mapper.Map<ICollection<ResponseSucursalHorarioDto>>(list);

        return collection;
    }

    private async Task<IEnumerable<SucursalHorario>> ValidateHorarios(byte idSucursal, IEnumerable<RequestSucursalHorarioDto> sucursalHorarios)
    {
        var horariosExistentes = mapper.Map<List<SucursalHorario>>(sucursalHorarios);
        foreach (var item in horariosExistentes)
        {
            item.IdSucursal = idSucursal;
            await sucursalHorarioValidator.ValidateAndThrowAsync(item);
        }
        return horariosExistentes;
    }
}