using App = BaseReservation.Application.Enums;
using AutoMapper;
using Infra = BaseReservation.Infrastructure.Enums;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Application.ResponseDTOs.Base;
using BaseReservation.Application.ValueResolvers;

namespace BaseReservation.Application.Profiles;

public class MiscApplicationProfile : Profile
{
    public MiscApplicationProfile()
    {
        CreateMap<App.WeekDay, Infra.WeekDay>().ReverseMap();

        CreateMap<BaseEntity, BaseModel>()
            .ForMember(m => m.CreatedBy, opts =>
            {
                opts.MapFrom<CurrentUserIdResolverBaseEntityAdd>();
            })
            .ForMember(m => m.UpdatedBy, opts =>
            {
                opts.MapFrom<CurrentUserIdResolverBaseEntityModify>();
            });

        CreateMap<ResponseReservationDto, Reservation>()
            .IncludeBase<BaseEntity, BaseModel>();

        CreateMap<ResponseReservationQuestionDto, ReservationQuestion>()
            .IncludeBase<BaseEntity, BaseModel>();

        CreateMap<ResponseReservationDetailDto, ReservationDetail>();

        CreateMap<ResponseOrderDto, Order>()
            .IncludeBase<BaseEntity, BaseModel>();

        CreateMap<ResponseOrderDetailDto, OrderDetail>();

        CreateMap<ResponseCustomerDto, Customer>()
            .IncludeBase<BaseEntity, BaseModel>();

        CreateMap<ResponsePaymentTypeDto, PaymentType>();
        CreateMap<ResponseTaxDto, Tax>();

        CreateMap<ResponseBranchDto, Branch>()
            .IncludeBase<BaseEntity, BaseModel>();
    }
}