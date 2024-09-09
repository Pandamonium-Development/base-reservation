using BaseReservation.Application.Profiles;
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