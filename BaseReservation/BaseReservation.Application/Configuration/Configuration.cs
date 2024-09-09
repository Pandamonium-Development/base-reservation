using BaseReservation.Application.Profiles;
using BaseReservation.Application.Services.Implementations;
using BaseReservation.Application.Services.Implementations.Authorization;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Application.Services.Interfaces.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BaseReservation.Application.Configuration;

public static class Configuration
{
    /// <summary>
    /// Configure all elements of Application layer
    /// </summary>
    /// <param name="services">Service collection configuration</param>
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddTransient<IServiceUsuario, ServiceUsuario>();
        services.AddTransient<IServiceProducto, ServiceProducto>();
        services.AddTransient<IServiceRol, ServiceRol>();
        services.AddTransient<IServiceDetalleFactura, ServiceDetalleFactura>();
        services.AddTransient<IServiceFactura, ServiceFactura>();
        services.AddTransient<IServiceSucursal, ServiceSucursal>();
        services.AddTransient<IServiceReserva, ServiceReserva>();
        services.AddTransient<IServiceServicio, ServiceServicio>();
        services.AddTransient<IServiceReservaPregunta, ServiceReservaPregunta>();
        services.AddTransient<IServiceUnidadMedida, ServiceUnidadMedida>();
        services.AddTransient<IServiceCategoria, ServiceCategoria>();
        services.AddTransient<IServiceHorario, ServiceHorario>();
        services.AddTransient<IServiceTipoServicio, ServiceTipoServicio>();
        services.AddTransient<IServiceProvincia, ServiceProvincia>();
        services.AddTransient<IServiceCanton, ServiceCanton>();
        services.AddTransient<IServiceDistrito, ServiceDistrito>();
        services.AddTransient<IServiceFeriado, ServiceFeriado>();
        services.AddTransient<IServiceSucursalFeriado, ServiceSucursalFeriado>();
        services.AddTransient<IServiceSucursalHorario, ServiceSucursalHorario>();
        services.AddTransient<IServiceSucursalHorarioBloqueo, ServiceSucursalHorarioBloqueo>();
        services.AddTransient<IServiceInventario, ServiceInventario>();
        services.AddTransient<IServicePedido, ServicePedido>();
        services.AddTransient<IServiceDetalleReserva, ServiceDetalleReserva>();
        services.AddTransient<IServiceCliente, ServiceCliente>();
        services.AddTransient<IServiceInventarioProducto, ServiceInventarioProducto>();
        services.AddTransient<IServiceInventarioProductoMovimiento, ServiceInventarioProductoMovimiento>();
        services.AddTransient<IServiceTipoPago, ServiceTipoPago>();
        services.AddTransient<IServiceImpuesto, ServiceImpuesto>();
        services.AddTransient<IServiceProveedor, ServiceProveedor>();
        services.AddTransient<IServiceUsuarioSucursal, ServiceUsuarioSucursal>();

        services.AddScoped<IServiceUserContext, ServiceUserContext>();
        services.AddScoped<IServiceUserAuthorization, ServiceUserAuthorization>();
    }

    /// <summary>
    /// Configure auto mapper profiles
    /// </summary>
    /// <param name="services">Service collection configuration</param>
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<ModelToDtoApplicationProfile>();
            config.AddProfile<DtoToModelApplicationProfile>();
            config.AddProfile<MiscApplicationProfile>();
        });
    }
}