using BaseReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.WebAPI.Configuration;

/// <summary>
/// Database configuration extension class
/// </summary>
public static class DBConfigurationExtension
{
    /// <summary>
    /// Method in charge on configure the database context
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration settings</param>
    public static void ConfigureDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services); //excepciones si no hay servicio 

        services.AddDbContext<BaseReservationContext>(options => options.UseSqlServer(configuration.GetConnectionString("BaseReservationDatabase"),
             sqlServerOption =>
             {
                 sqlServerOption.EnableRetryOnFailure(); //si se desconecta volver a intentar 
             })
        );
    }
}