using BaseReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.WebAPI.Configuration;

public static class DBConfigurationExtension
{
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