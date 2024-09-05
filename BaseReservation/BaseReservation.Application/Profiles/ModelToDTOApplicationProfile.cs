using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;
using AutoMapper;

namespace BaseReservation.Application.Profiles;

public class ModelToDtoApplicationProfile :Profile
{
    public ModelToDtoApplicationProfile()
    {
        CreateMap<Usuario, ResponseUsuarioDto>()
            .ForMember(dest => dest.Rol, inp => inp.MapFrom(ori => ori.IdRolNavigation))
            .ForMember(dest => dest.Genero, inp => inp.MapFrom(ori => ori.IdGeneroNavigation))
            .ForMember(dest => dest.Distrito, inp => inp.MapFrom(ori => ori.IdDistritoNavigation));
        CreateMap<Producto, ResponseProductoDto>()
            .ForMember(dest => dest.UnidadMedida, inp => inp.MapFrom(ori => ori.IdUnidadMedidaNavigation))
            .ForMember(dest => dest.Categoria, inp => inp.MapFrom(ori => ori.IdCategoriaNavigation));
        CreateMap<UnidadMedida, ResponseUnidadMedidaDto>();
        CreateMap<Rol, ResponseRolDto>();
        CreateMap<Categoria, ResponseCategoriaDto>();
        CreateMap<Factura, ResponseFacturaDto>()
            .ForMember(dest => dest.Cliente, inp => inp.MapFrom(ori => ori.IdClienteNavigation))
            .ForMember(dest => dest.TipoPago, inp => inp.MapFrom(ori => ori.IdTipoPagoNavigation))
            .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation))
            .ForMember(dest => dest.Impuesto, inp => inp.MapFrom(ori => ori.IdImpuestoNavigation))
            .ForMember(dest => dest.Pedido, inp => inp.MapFrom(ori => ori.IdPedidoNavigation));
        CreateMap<Pedido, ResponsePedidoDto>()
           .ForMember(dest => dest.Cliente, inp => inp.MapFrom(ori => ori.IdClienteNavigation))
           .ForMember(dest => dest.TipoPago, inp => inp.MapFrom(ori => ori.IdTipoPagoNavigation))
           .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation))
           .ForMember(dest => dest.Impuesto, inp => inp.MapFrom(ori => ori.IdImpuestoNavigation))
           .ForMember(dest => dest.Reserva, inp => inp.MapFrom(ori => ori.IdReservaNavigation));
        CreateMap<Sucursal, ResponseSucursalDto>()
            .ForMember(dest => dest.Distrito, inp => inp.MapFrom(ori => ori.IdDistritoNavigation));
        CreateMap<UsuarioSucursal, ResponseUsuarioSucursalDto>()
        CreateMap<Inventario, ResponseInventarioDto>();
            .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation))
            .ForMember(dest => dest.Usuario, inp => inp.MapFrom(ori => ori.IdUsuarioNavigation));
        CreateMap<SucursalFeriado, ResponseSucursalFeriadoDto>()
             .ForMember(dest => dest.Feriado, inp => inp.MapFrom(ori => ori.IdFeriadoNavigation))
             .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation));
        CreateMap<ReservaPregunta, ResponseReservaPreguntaDto>()
            .ForMember(dest => dest.Reserva, inp => inp.MapFrom(ori => ori.IdReservaNavigation));
        CreateMap<Reserva, ResponseReservaDto>()
            .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation))
            .ForMember(dest => dest.Cliente, inp => inp.MapFrom(ori => ori.IdClienteNavigation));
        CreateMap<SucursalHorario, ResponseSucursalHorarioDto>()
            .ForMember(dest => dest.Horario, inp => inp.MapFrom(ori => ori.IdHorarioNavigation))
            .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation));
        CreateMap<SucursalHorarioBloqueo, ResponseSucursalHorarioBloqueoDto>()
            .ForMember(dest => dest.SucursalHorario, inp => inp.MapFrom(ori => ori.IdSucursalHorarioNavigation));
        CreateMap<Horario, ResponseHorarioDto>();
        CreateMap<Distrito, ResponseDistritoDto>()
            .ForMember(dest => dest.Canton, inp => inp.MapFrom(ori => ori.IdCantonNavigation));
        CreateMap<ReservaServicio, ResponseReservaServicioDto>()
            .ForMember(dest => dest.Reserva, inp => inp.MapFrom(ori => ori.IdReservaNavigation))
            .ForMember(dest => dest.Servicio, inp => inp.MapFrom(ori => ori.IdServicioNavigation));
        CreateMap<Servicio, ResponseServicioDto>()
            .ForMember(dest => dest.TipoServicio, inp => inp.MapFrom(ori => ori.IdTipoServicioNavigation));
        CreateMap<TipoServicio, ResponseTipoServicioDto>();
        CreateMap<Canton, ResponseCantonDto>()
            .ForMember(dest => dest.Provincia, inp => inp.MapFrom(ori => ori.IdProvinciaNavigation));
        CreateMap<Contacto, ResponseContactoDto>()
            .ForMember(dest => dest.Proveedor, inp => inp.MapFrom(ori => ori.IdProveedorNavigation));
        CreateMap<DetalleFactura, ResponseDetalleFacturaDto>()
            .ForMember(dest => dest.Factura, inp => inp.MapFrom(ori => ori.IdFacturaNavigation))
            .ForMember(dest => dest.Servicio, inp => inp.MapFrom(ori => ori.IdServicioNavigation));
        CreateMap<DetallePedido, ResponseDetallePedidoDto>()
            .ForMember(dest => dest.Pedido, inp => inp.MapFrom(ori => ori.IdPedidoNavigation))
            .ForMember(dest => dest.Servicio, inp => inp.MapFrom(ori => ori.IdServicioNavigation));
        CreateMap<DetalleFacturaProducto, ResponseDetalleFacturaProductoDto>()
            .ForMember(dest => dest.DetalleFactura, inp => inp.MapFrom(ori => ori.IdDetalleFacturaNavigation))
            .ForMember(dest => dest.Producto, inp => inp.MapFrom(ori => ori.IdProductoNavigation));
        CreateMap<DetallePedidoProducto, ResponseDetallePedidoProductoDto>()
            .ForMember(dest => dest.DetallePedido, inp => inp.MapFrom(ori => ori.IdDetallePedidoNavigation))
            .ForMember(dest => dest.Producto, inp => inp.MapFrom(ori => ori.IdProductoNavigation));
        CreateMap<Feriado, ResponseFeriadoDto>();
        CreateMap<Genero, ResponseGeneroDto>();
        CreateMap<Impuesto, ResponseImpuestoDto>();
        CreateMap<Inventario, ResponseInventarioDto>()
            .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation));
        CreateMap<InventarioProducto, ResponseInventarioProductoDto>()
            .ForMember(dest => dest.Inventario, inp => inp.MapFrom(ori => ori.IdInventarioNavigation))
            .ForMember(dest => dest.Producto, inp => inp.MapFrom(ori => ori.IdProductoNavigation));
        CreateMap<Proveedor, ResponseProveedorDto>()
             .ForMember(dest => dest.Distrito, inp => inp.MapFrom(ori => ori.IdDistritoNavigation));
        CreateMap<Provincia, ResponseProvinciaDto>();
        CreateMap<TipoPago, ResponseTipoPagoDto>();
        CreateMap<TipoServicio, ResponseTipoServicioDto>();
        CreateMap<UsuarioSucursal, ResponseUsuarioSucursalDto>()
             .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation))
             .ForMember(dest => dest.Usuario, inp => inp.MapFrom(ori => ori.IdUsuarioNavigation));
        CreateMap<Cliente, ResponseClienteDto>()
              .ForMember(dest => dest.Distrito, inp => inp.MapFrom(ori => ori.IdDistritoNavigation));
        CreateMap<InventarioProductoMovimiento, ResponseInventarioProductoMovimientoDto>()
            .ForMember(dest => dest.InventarioProducto, inp => inp.MapFrom(ori => ori.IdInventarioProductoNavigation));
    }
}
