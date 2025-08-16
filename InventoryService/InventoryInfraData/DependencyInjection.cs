using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using InventoryInfraData.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryInfraData;

public class DependencyInjection
{
    public static void ConfigureDatabase(IConfiguration configuration, IServiceCollection services)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
    
        var _sessionFactory = Fluently.Configure()
            .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductMap>())
            .BuildSessionFactory(); 
        
        services.AddScoped(factory => _sessionFactory.OpenSession());
    }
}