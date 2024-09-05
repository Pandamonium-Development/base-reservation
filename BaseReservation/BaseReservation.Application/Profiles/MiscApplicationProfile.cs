﻿using App = BaseReservation.Application.ResponseDTOs.Enums;
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
        CreateMap<App.DiaSemana, Infra.DiaSemana>().ReverseMap();

        CreateMap<BaseEntity, BaseModel>()
            .ForMember(m => m.UsuarioCreacion, opts =>
            {
                opts.MapFrom<CurrentUserIdResolverBaseEntityAdd>();
            })
            .ForMember(m => m.UsuarioModificacion, opts =>
            {
                opts.MapFrom<CurrentUserIdResolverBaseEntityModify>();
            });

        CreateMap<ResponseReservaDTO, Reserva>()
            .IncludeBase<BaseEntity, BaseModel>();

        CreateMap<ResponseReservaPreguntaDTO, ReservaPregunta>()
            .IncludeBase<BaseEntity, BaseModel>();

        CreateMap<ResponseReservaServicioDTO, ReservaServicio>();

        CreateMap<ResponsePedidoDto, Pedido>()
            .IncludeBase<BaseEntity, BaseModel>();

        CreateMap<ResponseDetallePedidoDto, DetallePedido>();

        CreateMap<ResponseClienteDto, Cliente>()
            .IncludeBase<BaseEntity, BaseModel>();

        CreateMap<ResponseImpuestoDto, Impuesto>();
        CreateMap<ResponseTipoPagoDTO, TipoPago>();

        CreateMap<ResponseSucursalDTO, Sucursal>()
            .IncludeBase<BaseEntity, BaseModel>();
    }
}
