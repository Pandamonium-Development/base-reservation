using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations
{
    public class ServiceServicio(IRepositoryServicio repository, IMapper mapper,
                                IValidator<Servicio> servicioValidator) : IServiceServicio
    {
        public async Task<ResponseServicioDto> CreateServicioAsync(RequestServicioDto servicio)
        {
            var result = await repository.CreateServicioAsync(mapper.Map<Servicio>(servicio));
            if (result == null) throw new NotFoundException("Servicio no creado.");
            return mapper.Map<ResponseServicioDto>(result);
        }

        public async Task<ResponseServicioDto> FindByIdAsync(byte id)
        {
            var servicio = await repository.FindByIdAsync(id);
            if (servicio == null) throw new NotFoundException("Servicio no encontrado.");

            return mapper.Map<ResponseServicioDto>(servicio);
        }

        public async Task<ICollection<ResponseServicioDto>> ListALLAsync()
        {
            var list = await repository.ListAllAsync();
            var collection = mapper.Map<ICollection<ResponseServicioDto>>(list);

            return collection;
        }

        public async Task<ResponseServicioDto> UpdateServicioAsync(byte id, RequestServicioDto servicioDTO)
        {
            if (!await repository.ExistsServicioAsync(id)) throw new NotFoundException("Servicio no encontrado.");

            var sucursal = await ValidateServicio(servicioDTO);
            sucursal.Id = id;
            var result = await repository.UpdateServicioAsync(sucursal);

            return mapper.Map<ResponseServicioDto>(result);
        }

        private async Task<Servicio> ValidateServicio(RequestServicioDto servicioDTO)
        {
            var servicio = mapper.Map<Servicio>(servicioDTO);
            await servicioValidator.ValidateAndThrowAsync(servicio);
            return servicio;
        }
    }
}