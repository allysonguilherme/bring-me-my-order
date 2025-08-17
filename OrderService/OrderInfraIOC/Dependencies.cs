using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApplication.Services;
using OrderApplication.Services.Interfaces;
using OrderBusiness.Publishers;
using OrderBusiness.Repositories;
using OrderInfraData;
using OrderInfraData.Message;
using OrderInfraData.Message.Interfaces;
using OrderInfraData.Repositories;

namespace OrderInfraIOC;

public class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        DependencyInjection.ConfigureDatabase(configuration, services);

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderFacade, OrderFacade>();
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        services.AddScoped<IOrderPublisher, OrderPublisher>();
    }
}