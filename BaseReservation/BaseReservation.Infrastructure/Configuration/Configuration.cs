using BaseReservation.Infrastructure.Repository.Implementations;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BaseReservation.Infrastructure.Configuration;

public static class Configuration
{
    public static void ConfigureInfraestructure(this IServiceCollection services)
    {
        // General
        services.AddTransient<IRepositoryPaymentType, RepositoryPaymentType>();
        services.AddTransient<IRepositoryTypeService, RepositoryTypeService>();
        services.AddTransient<IRepositoryUnitMeasure, RepositoryUnitMeasure>();
        services.AddTransient<IRepositoryProduct, RepositoryProduct>();
        services.AddTransient<IRepositoryService, RepositoryService>();
        services.AddTransient<IRepositoryCategory, RepositoryCategory>();
        services.AddTransient<IRepositoryTax, RepositoryTax>();
        services.AddTransient<IRepositoryCustomer, RepositoryCustomer>();
        services.AddTransient<IRepositorySchedule, RepositorySchedule>();
        services.AddTransient<IRepositoryHoliday, RepositoryHoliday>();
        services.AddTransient<IRepositoryVendor, RepositoryVendor>();

        // Security
        services.AddTransient<IRepositoryUser, RepositoryUser>();
        services.AddTransient<IRepositoryRole, RepositoryRole>();
        services.AddTransient<IRepositoryTokenMaster, RepositoryTokenMaster>();

        // Address
        services.AddTransient<IRepositoryProvince, RepositoryProvince>();
        services.AddTransient<IRepositoryCanton, RepositoryCanton>();
        services.AddTransient<IRepositoryDistrict, RepositoryDistrict>();

        // Invoice
        services.AddTransient<IRepositoryInvoice, RepositoryInvoice>();
        services.AddTransient<IRepositoryInvoiceDetail, RepositoryInvoiceDetail>();

        // Order
        services.AddTransient<IRepositoryOrder, RepositoryOrder>();
        services.AddTransient<IRepositoryOrderDetail, RepositoryOrderDetail>();

        // Reservation
        services.AddTransient<IRepositoryReservation, RepositoryReservation>();
        services.AddTransient<IRepositoryReservationDetail, RepositoryReservationDetail>();
        services.AddTransient<IRepositoryReservationQuestion, RepositoryReservationQuestion>();

        // Branch
        services.AddTransient<IRepositoryBranch, RepositoryBranch>();
        services.AddTransient<IRepositoryBranchHoliday, RepositoryBranchHoliday>();
        services.AddTransient<IRepositoryBranchSchedule, RepositoryBranchSchedule>();
        services.AddTransient<IRepositoryBranchScheduleBlock, RepositoryBranchScheduleBlock>();
        services.AddTransient<IRepositoryUserBranch, RepositoryUserBranch>();

        // Inventory
        services.AddTransient<IRepositoryInventory, RepositoryInventory>();
        services.AddTransient<IRepositoryInventoryProduct, RepositoryInventoryProduct>();
        services.AddTransient<IRepositoryInventoryProductTransaction, RepositoryInventoryProductTransaction>();
    }
}