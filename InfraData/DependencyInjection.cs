using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using InfraData.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfraData;

public class DependencyInjection
{
    public static void ConfigureDatabase(IConfiguration configuration, IServiceCollection services)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
    
        var _sessionFactory = Fluently.Configure()
            .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(ProductMap).Assembly))
            .BuildSessionFactory(); 
        
        services.AddScoped(factory => _sessionFactory.OpenSession());
    }
}