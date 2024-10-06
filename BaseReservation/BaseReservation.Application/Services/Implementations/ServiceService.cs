using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceService(IRepositoryService repository, IMapper mapper,
                                IValidator<Service> serviceValidator) : IServiceService
{
    /// <inheritdoc />
    public async Task<ResponseServiceDto> CreateServiceAsync(RequestServiceDto serviceDto)
    {
        var result = await repository.CreateServiceAsync(mapper.Map<Service>(serviceDto));
        if (result == null) throw new NotFoundException("Servicio no creado.");

        return mapper.Map<ResponseServiceDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteServiceAsync(byte id)
    {
        if (!await repository.ExistsServiceAsync(id)) throw new NotFoundException("Servicio no encontrado.");
        return await repository.DeleteServiceAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseServiceDto> FindByIdAsync(byte id)
    {
        var service = await repository.FindByIdAsync(id);
        if (service == null) throw new NotFoundException("Servicio no encontrado.");

        return mapper.Map<ResponseServiceDto>(service);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseServiceDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseServiceDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ResponseServiceDto> UpdateServiceAsync(byte id, RequestServiceDto serviceDto)
    {
        if (!await repository.ExistsServiceAsync(id)) throw new NotFoundException("Servicio no encontrado.");

        var service = await ValidateService(serviceDto);
        service.Id = id;
        var result = await repository.UpdateServiceAsync(service);

        return mapper.Map<ResponseServiceDto>(result);
    }

    /// <inheritdoc />
    private async Task<Service> ValidateService(RequestServiceDto serviceDto)
    {
        var service = mapper.Map<Service>(serviceDto);
        await serviceValidator.ValidateAndThrowAsync(service);

        return service;
    }
}