using BaseReservation.Application.ResponseDTOs.Base;
using BaseReservation.Application.Services.Interfaces.Authorization;
using BaseReservation.Infrastructure.Models;
using AutoMapper;

namespace BaseReservation.Application.ValueResolvers;

public class CurrentUserIdResolverBaseEntityModify(IServiceUserContext serviceUserContext) : IValueResolver<BaseEntity, BaseModel, string?>
{
    public string? Resolve(BaseEntity source, BaseModel destination, string? destMember, ResolutionContext context) =>
        serviceUserContext.UserId!;
}