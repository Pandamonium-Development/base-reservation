using BaseReservation.Infrastructure.Repository.Implementations;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BaseReservation.Infrastructure.Configuration;

public static class Configuration
{
    public static void ConfigureInfraestructure(this IServiceCollection services)
    {
        services.AddTransient<IRepositoryUsuario, RepositoryUsuario>();
        services.AddTransient<IRepositoryProducto, RepositoryProducto>();
        services.AddTransient<IRepositoryRol, RepositoryRol>();
        services.AddTransient<IRepositoryFactura, RepositoryFactura>();
        services.AddTransient<IRepositoryDetalleFactura, RepositoryDetalleFactura>();
        services.AddTransient<IRepositorySucursal, RepositorySucursal>();
        services.AddTransient<IRepositoryReserva, RepositoryReserva>();
        services.AddTransient<IRepositoryServicio, RepositoryServicio>();
        services.AddTransient<IRepositoryHorario, RepositoryHorario>();
        services.AddTransient<IRepositoryReservaPregunta, RepositoryReservaPregunta>();
        services.AddTransient<IRepositoryTipoServicio, RepositoryTipoServicio>();
        services.AddTransient<IRepositoryUnidadMedida, RepositoryUnidadMedida>();
        services.AddTransient<IRepositoryCategoria, RepositoryCategoria>();
        services.AddTransient<IRepositoryCanton, RepositoryCanton>();
        services.AddTransient<IRepositoryProvincia, RepositoryProvincia>();
        services.AddTransient<IRepositoryDistrito, RepositoryDistrito>();
        services.AddTransient<IRepositoryTipoServicio, RepositoryTipoServicio>();
        services.AddTransient<IRepositoryFeriado, RepositoryFeriado>();
        services.AddTransient<IRepositorySucursalFeriado, RepositorySucursalFeriado>();
        services.AddTransient<IRepositorySucursalHorarioBloqueo, RepositorySucursalHorarioBloqueo>();
        services.AddTransient<IRepositorySucursalHorario, RepositorySucursalHorario>();
        services.AddTransient<IRepositoryInventario, RepositoryInventario>();
        services.AddTransient<IRepositoryPedido, RepositoryPedido>();
        services.AddTransient<IRepositoryDetallePedido, RepositoryDetallePedido>();
        services.AddTransient<IRepositoryDetalleReserva, RepositoryDetalleReserva>();
        services.AddTransient<IRepositoryCliente, RepositoryCliente>();
        services.AddTransient<IRepositoryInventarioProducto, RepositoryInventarioProducto>();
        services.AddTransient<IRepositoryInventarioProductoMovimiento, RepositoryInventarioProductoMovimiento>();
        services.AddTransient<IRepositoryTipoPago, RepositoryTipoPago>();
        services.AddTransient<IRepositoryImpuesto, RepositoryImpuesto>();
        services.AddTransient<IRepositoryProveedor, RepositoryProveedor>();
        services.AddTransient<IRepositoryTokenMaster, RepositoryTokenMaster>();
        services.AddTransient<IRepositoryUsuarioSucursal, RepositoryUsuarioSucursal>();
    }
}
// es una extencion xq usa la palabra this
// Trasient cambia por cada request //scoped como para  factruas y// singleton presente hasta la vida util de la aplicacion