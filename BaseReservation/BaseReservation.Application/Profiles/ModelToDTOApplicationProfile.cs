using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;
using AutoMapper;

namespace BaseReservation.Application.Profiles;

public class ModelToDtoApplicationProfile :Profile
{
    public ModelToDtoApplicationProfile()
    {
        CreateMap<Usuario, ResponseUsuarioDTO>()
            .ForMember(dest => dest.Rol, inp => inp.MapFrom(ori => ori.IdRolNavigation))
            .ForMember(dest => dest.Genero, inp => inp.MapFrom(ori => ori.IdGeneroNavigation))
            .ForMember(dest => dest.Distrito, inp => inp.MapFrom(ori => ori.IdDistritoNavigation));
        CreateMap<Producto, ResponseProductoDTO>()
            .ForMember(dest => dest.UnidadMedida, inp => inp.MapFrom(ori => ori.IdUnidadMedidaNavigation))
            .ForMember(dest => dest.Categoria, inp => inp.MapFrom(ori => ori.IdCategoriaNavigation));
        CreateMap<Categoria, ResponseCategoriaDto>();
        CreateMap<UnidadMedida, ResponseUnidadMedidaDTO>();
        CreateMap<Rol, ResponseRolDTO>();
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
        CreateMap<Sucursal, ResponseSucursalDTO>()
            .ForMember(dest => dest.Distrito, inp => inp.MapFrom(ori => ori.IdDistritoNavigation));
        CreateMap<Inventario, ResponseInventarioDto>();
        CreateMap<UsuarioSucursal, ResponseUsuarioSucursalDTO>()
            .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation))
            .ForMember(dest => dest.Usuario, inp => inp.MapFrom(ori => ori.IdUsuarioNavigation));
        CreateMap<SucursalFeriado, ResponseSucursalFeriadoDTO>()
             .ForMember(dest => dest.Feriado, inp => inp.MapFrom(ori => ori.IdFeriadoNavigation))
             .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation));
        CreateMap<ReservaPregunta, ResponseReservaPreguntaDTO>()
            .ForMember(dest => dest.Reserva, inp => inp.MapFrom(ori => ori.IdReservaNavigation));
        CreateMap<Reserva, ResponseReservaDTO>()
            .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation))
            .ForMember(dest => dest.Cliente, inp => inp.MapFrom(ori => ori.IdClienteNavigation));
        CreateMap<SucursalHorario, ResponseSucursalHorarioDTO>()
            .ForMember(dest => dest.Horario, inp => inp.MapFrom(ori => ori.IdHorarioNavigation))
            .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation));
        CreateMap<SucursalHorarioBloqueo, ResponseSucursalHorarioBloqueoDTO>()
            .ForMember(dest => dest.SucursalHorario, inp => inp.MapFrom(ori => ori.IdSucursalHorarioNavigation));
        CreateMap<Horario, ResponseHorarioDto>();
        CreateMap<Distrito, ResponseDistritoDto>()
            .ForMember(dest => dest.Canton, inp => inp.MapFrom(ori => ori.IdCantonNavigation));
        CreateMap<ReservaServicio, ResponseReservaServicioDTO>()
            .ForMember(dest => dest.Reserva, inp => inp.MapFrom(ori => ori.IdReservaNavigation))
            .ForMember(dest => dest.Servicio, inp => inp.MapFrom(ori => ori.IdServicioNavigation));
        CreateMap<Servicio, ResponseServicioDTO>()
            .ForMember(dest => dest.TipoServicio, inp => inp.MapFrom(ori => ori.IdTipoServicioNavigation));
        CreateMap<TipoServicio, ResponseTipoServicioDTO>();
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
        CreateMap<Proveedor, ResponseProveedorDTO>()
             .ForMember(dest => dest.Distrito, inp => inp.MapFrom(ori => ori.IdDistritoNavigation));
        CreateMap<Provincia, ResponseProvinciaDTO>();
        CreateMap<TipoPago, ResponseTipoPagoDTO>();
        CreateMap<TipoServicio, ResponseTipoServicioDTO>();
        CreateMap<UsuarioSucursal, ResponseUsuarioSucursalDTO>()
             .ForMember(dest => dest.Sucursal, inp => inp.MapFrom(ori => ori.IdSucursalNavigation))
             .ForMember(dest => dest.Usuario, inp => inp.MapFrom(ori => ori.IdUsuarioNavigation));
        CreateMap<Cliente, ResponseClienteDto>()
              .ForMember(dest => dest.Distrito, inp => inp.MapFrom(ori => ori.IdDistritoNavigation));
        CreateMap<InventarioProductoMovimiento, ResponseInventarioProductoMovimientoDto>()
            .ForMember(dest => dest.InventarioProducto, inp => inp.MapFrom(ori => ori.IdInventarioProductoNavigation));
    }
}
