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
        services.AddTransient<IServiceUser, ServiceUser>();
        services.AddTransient<IServiceProduct, ServiceProduct>();
        services.AddTransient<IServiceRole, ServiceRole>();
        services.AddTransient<IServiceInvoiceDetail, ServiceInvoiceDetail>();
        services.AddTransient<IServiceInvoice, ServiceInvoice>();
        services.AddTransient<IServiceBranch, ServiceBranch>();
        services.AddTransient<IServiceReservation, ServiceReservation>();
        services.AddTransient<IServiceService, ServiceService>();
        services.AddTransient<IServiceReservationQuestion, ServiceReservationQuestion>();
        services.AddTransient<IServiceUnitMeasure, ServiceUnitMeasure>();
        services.AddTransient<IServiceCategory, ServiceCategory>();
        services.AddTransient<IServiceSchedule, ServiceSchedule>();
        services.AddTransient<IServiceTypeService, ServiceTypeService>();
        services.AddTransient<IServiceProvince, ServiceProvince>();
        services.AddTransient<IServiceCanton, ServiceCanton>();
        services.AddTransient<IServiceDistrict, ServiceDistrict>();
        services.AddTransient<IServiceHoliday, ServiceHoliday>();
        services.AddTransient<IServiceBranchHoliday, ServiceBranchHoliday>();
        services.AddTransient<IServiceBranchSchedule, ServiceBranchSchedule>();
        services.AddTransient<IServiceBranchScheduleBlock, ServiceBranchScheduleBlock>();
        services.AddTransient<IServiceInventory, ServiceInventory>();
        services.AddTransient<IServiceOrder, ServiceOrder>();
        services.AddTransient<IServiceReservationDetail, ServiceReservationDetail>();
        services.AddTransient<IServiceCustomer, ServiceCustomer>();
        services.AddTransient<IServiceInventoryProduct, ServiceInventoryProduct>();
        services.AddTransient<IServiceInventoryProductTransaction, ServiceInventoryProductTransaction>();
        services.AddTransient<IServicePaymentType, ServicePaymentType>();
        services.AddTransient<IServiceTax, ServiceTax>();
        services.AddTransient<IServiceVendor, ServiceVendor>();
        services.AddTransient<IServiceUserBranch, ServiceUserBranch>();

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