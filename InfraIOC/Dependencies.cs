using Application.Services;
using Application.Services.Interfaces;
using Business.Repositories;
using InfraData;
using InfraData.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfraIOC;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        DependencyInjection.ConfigureDatabase(configuration, services);

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductFacade, ProductFacade>();
    }
}