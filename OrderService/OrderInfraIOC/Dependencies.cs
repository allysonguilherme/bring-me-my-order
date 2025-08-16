using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderInfraData;

namespace OrderInfraIOC;

public class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        DependencyInjection.ConfigureDatabase(configuration, services);
    }
}