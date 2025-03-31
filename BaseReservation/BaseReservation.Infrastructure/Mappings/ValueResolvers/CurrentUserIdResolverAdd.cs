using AutoMapper;
using BaseReservation.Domain.Core.Models;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces.Authorization;

namespace BaseReservation.Application.ValueResolvers;

public class CurrentUserIdResolverAdd(IServiceUserContext serviceUserContext) : IValueResolver<RequestBaseDto, BaseEntity, string>
{
    public string Resolve(RequestBaseDto source, BaseEntity destination, string destMember, ResolutionContext context) =>
        serviceUserContext.UserId!;
}