using BaseReservation.Application.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BaseReservation.WebAPI.Configuration;

/// <summary>
/// Fluent validation configuration extension class
/// </summary>
public static class FluentValidationConfiguration
{
    /// <summary>
    /// Add configuration for fluent validations
    /// </summary>
    /// <param name="services">Service collection</param>
    public static void ConfigureFluentValidation(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddValidatorsFromAssemblyContaining<SucursalValidator>();

        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    }
}