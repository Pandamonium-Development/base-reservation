using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceServicio(IRepositoryServicio repository, IMapper mapper,
                                IValidator<Servicio> serviceValidator) : IServiceServicio
{
    /// <inheritdoc />
    public async Task<ResponseServicioDto> CreateServiceAsync(RequestServicioDto servicio)
    {
        var result = await repository.CreateServiceAsync(mapper.Map<Servicio>(servicio));
        if (result == null) throw new NotFoundException("Servicio no creado.");

        return mapper.Map<ResponseServicioDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteServiceAsync(byte id)
    {
        if (!await repository.ExistsServiceAsync(id)) throw new NotFoundException("Servicio no encontrado.");
        return await repository.DeleteServiceAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseServicioDto> FindByIdAsync(byte id)
    {
        var service = await repository.FindByIdAsync(id);
        if (service == null) throw new NotFoundException("Servicio no encontrado.");

        return mapper.Map<ResponseServicioDto>(service);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseServicioDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseServicioDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ResponseServicioDto> UpdateServiceAsync(byte id, RequestServicioDto serviceDto)
    {
        if (!await repository.ExistsServiceAsync(id)) throw new NotFoundException("Servicio no encontrado.");

        var service = await ValidateService(serviceDto);
        service.Id = id;
        var result = await repository.UpdateServiceAsync(service);

        return mapper.Map<ResponseServicioDto>(result);
    }

    /// <inheritdoc />
    private async Task<Servicio> ValidateService(RequestServicioDto serviceDto)
    {
        var service = mapper.Map<Servicio>(serviceDto);
        await serviceValidator.ValidateAndThrowAsync(service);

        return service;
    }
}