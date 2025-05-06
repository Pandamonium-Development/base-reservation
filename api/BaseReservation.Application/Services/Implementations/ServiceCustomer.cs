using AutoMapper;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.Dtos.Response;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCustomer(ICoreService<Customer> coreService, IMapper mapper) : IServiceCustomer
{
    /// <inheritdoc />
    public async Task<bool> DeleteCustomerAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Customer>().ExistsAsync(id)) throw new NotFoundException("Cliente no encontrado.");

        var spec = new BaseSpecification<Customer>(x => x.Id == id);
        var customer = await coreService.UnitOfWork.Repository<Customer>().FirstOrDefaultAsync(spec);
        customer!.Active = false;

        coreService.UnitOfWork.Repository<Customer>().Update(customer);
        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseCustomerDto?> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Customer>().ExistsAsync(id)) throw new NotFoundException("Cliente no encontrado.");

        var spec = new BaseSpecification<Customer>(x => x.Id == id);
        var customer = await coreService.UnitOfWork.Repository<Customer>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseCustomerDto>(customer);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseCustomerDto>> ListAllAsync()
    {
        var customers = await coreService.UnitOfWork.Repository<Customer>().ListAllAsync();
        return mapper.Map<ICollection<ResponseCustomerDto>>(customers);
    }
}