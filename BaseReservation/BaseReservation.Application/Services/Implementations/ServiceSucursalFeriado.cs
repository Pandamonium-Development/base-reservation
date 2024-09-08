using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceSucursalFeriado(IRepositorySucursalFeriado repository, IMapper mapper,
                                    IValidator<SucursalFeriado> sucursalFeriadoValidator) : IServiceSucursalFeriado
{
    /// <inheritdoc />
    public async Task<bool> CreateSucursalFeriadosAsync(byte idSucursal, IEnumerable<RequestSucursalFeriadoDto> sucursalFeriados)
    {
        var feriados = await ValidateFeriados(idSucursal, sucursalFeriados);

        var result = await repository.CreateSucursalFeriadosAsync(idSucursal, feriados);
        if (!result) throw new ListNotAddedException("Error al guardar feriados");

        return result;
    }

    /// <inheritdoc />
    public async Task<ResponseSucursalFeriadoDto> FindByIdAsync(short id)
    {
        var sucursalFeriado = await repository.FindByIdAsync(id);
        if (sucursalFeriado == null) throw new NotFoundException("Feriado en sucursal no encontrado.");

        return mapper.Map<ResponseSucursalFeriadoDto>(sucursalFeriado);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseSucursalFeriadoDto>> ListAllBySucursalAsync(byte idSucursal, short? anno)
    {
        var list = anno == null ? await repository.ListAllBySucursalAsync(idSucursal) :
                               await repository.ListAllBySucursalAsync(idSucursal, anno.Value);
        var collection = mapper.Map<ICollection<ResponseSucursalFeriadoDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    private async Task<IEnumerable<SucursalFeriado>> ValidateFeriados(byte idSucursal, IEnumerable<RequestSucursalFeriadoDto> sucursalFeriados)
    {
        var feriados = mapper.Map<List<SucursalFeriado>>(sucursalFeriados);
        foreach (var item in feriados)
        {
            item.IdSucursal = idSucursal;
            await sucursalFeriadoValidator.ValidateAndThrowAsync(item);
        }
        return feriados;
    }
}