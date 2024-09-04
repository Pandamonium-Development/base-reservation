using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces.Authorization;
using BaseReservation.Infrastructure.Models;
using AutoMapper;

namespace BaseReservation.Application.ValueResolvers;

public class CurrentUserIdResolverModify(IServiceUserContext serviceUserContext) : IValueResolver<RequestBaseDto, BaseModel, string?>
{
    public string? Resolve(RequestBaseDto source, BaseModel destination, string? destMember, ResolutionContext context) =>
        serviceUserContext.UserId!;
}