using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ValueResolvers;
using AutoMapper;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Profiles;

public class DtoToModelApplicationProfile : Profile
{
    public DtoToModelApplicationProfile()
    {
        CreateMap<RequestBaseDto, BaseModel>()
            .ForMember(m => m.CreatedBy, opts =>
            {
                opts.MapFrom<CurrentUserIdResolverAdd>();
            })
            .ForMember(m => m.UpdatedBy, opts =>
            {
                opts.MapFrom<CurrentUserIdResolverModify>();
            });

        CreateMap<RequestProductDto, Product>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        CreateMap<RequestBranchDto, Branch>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        CreateMap<RequestServiceDto, Service>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        CreateMap<RequestHolidayDto, Holiday>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        CreateMap<RequestScheduleDto, Schedule>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        CreateMap<RequestInventoryDto, Inventory>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        CreateMap<RequestInventoryProductDto, InventoryProduct>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        CreateMap<RequestInvoiceDto, Invoice>()
            .IncludeBase<RequestBaseDto, BaseModel>();
        
        CreateMap<RequestOrderDto, Order>()
            .IncludeBase<RequestBaseDto, BaseModel>();
        
        CreateMap<RequestReservationDto, Reservation>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        CreateMap<RequestReservationQuestionDto, ReservationQuestion>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        CreateMap<RequestInventoryProductTransactionDto, InventoryProductTransaction>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        CreateMap<RequestVendorDto, Vendor>()
            .IncludeBase<RequestBaseDto, BaseModel>();

        
        CreateMap<RequestTypeServiceDto, TypeService>();
        CreateMap<RequestBranchScheduleDto, BranchSchedule>();
        CreateMap<RequestBranchScheduleBlockDto, BranchScheduleBlock>();
        CreateMap<RequestBranchHolidayDto, BranchHoliday>();
        CreateMap<RequestInvoiceDetailDto,  InvoiceDetail>();
        CreateMap<RequestOrderDetailDto, OrderDetail>();
        CreateMap<RequestReservationDetailDto, ReservationDetail>();
        CreateMap<RequestUserBranchDto, UserBranch>();
    }
}