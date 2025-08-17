using InventoryApplication.Services;
using InventoryApplication.Services.Interfaces;
using InventoryBusiness.Repositories;
using InventoryInfraData;
using InventoryInfraData.Data.Repositories;
using InventoryInfraData.Message;
using InventoryInfraData.Message.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryInfraIOC;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        DependencyInjection.ConfigureDatabase(configuration, services);

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductFacade, ProductFacade>();
        services.AddScoped<IMessageConsumer, MessageConsumer>();
    }
}