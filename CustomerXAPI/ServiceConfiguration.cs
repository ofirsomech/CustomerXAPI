using CustomerXAPI.Interfaces;
using CustomerXAPI.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class ServiceConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IPackageService, PackageService>();
    }
}