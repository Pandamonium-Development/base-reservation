using AutoMapper;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceImpuesto(IRepositoryImpuesto repository, IMapper mapper) : IServiceImpuesto
{
    /// <inheritdoc />
    public async Task<ICollection<ResponseImpuestoDto>> ListAllAsync()
    {
        var coleccion = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponseImpuestoDto>>(coleccion);
    }
}