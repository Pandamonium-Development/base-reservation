using AutoMapper;
using FluentValidation;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.Dtos.Request;
using BaseReservation.Application.Dtos.Response;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceService(ICoreService<Service> coreService, IMapper mapper,
                                IValidator<Service> serviceValidator) : IServiceService
{
    /// <inheritdoc />
    public async Task<ResponseServiceDto> CreateServiceAsync(RequestServiceDto serviceDto)
    {
        var service = await ValidateService(serviceDto);

        var result = await coreService.UnitOfWork.Repository<Service>().AddAsync(service);
        await coreService.UnitOfWork.SaveChangesAsync();
        if (result == null) throw new NotFoundException("Servicio no creado.");

        return mapper.Map<ResponseServiceDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteServiceAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Service>().ExistsAsync(id)) throw new NotFoundException("Servicio no encontrado.");

        var spec = new BaseSpecification<Service>(x => x.Id == id);
        var service = await coreService.UnitOfWork.Repository<Service>().FirstOrDefaultAsync(spec);
        service!.Active = false;

        coreService.UnitOfWork.Repository<Service>().Update(service);
        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseServiceDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<Service>(x => x.Id == id);
        var service = await coreService.UnitOfWork.Repository<Service>().FirstOrDefaultAsync(spec);
        if (service == null) throw new NotFoundException("Servicio no encontrado.");

        return mapper.Map<ResponseServiceDto>(service);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseServiceDto>> ListAllAsync()
    {
        var services = await coreService.UnitOfWork.Repository<Service>().ListAllAsync();

        return mapper.Map<ICollection<ResponseServiceDto>>(services);
    }

    /// <inheritdoc />
    public async Task<ResponseServiceDto> UpdateServiceAsync(long id, RequestServiceDto serviceDto)
    {
        if (!await coreService.UnitOfWork.Repository<Service>().ExistsAsync(id)) throw new NotFoundException("Servicio no encontrado.");

        var service = await ValidateService(serviceDto);
        service.Id = id;

        coreService.UnitOfWork.Repository<Service>().Update(service);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }

    /// <inheritdoc />
    private async Task<Service> ValidateService(RequestServiceDto serviceDto)
    {
        var service = mapper.Map<Service>(serviceDto);
        await serviceValidator.ValidateAndThrowAsync(service);

        return service;
    }
}