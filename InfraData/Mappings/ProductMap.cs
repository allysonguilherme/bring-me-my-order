using Business.Entities;
using FluentNHibernate.Mapping;

namespace InfraData.Mappings;

public class ProductMap: ClassMap<Product>
{
    public ProductMap()
    {
        Id(x => x.Id);
        Map(x => x.Name);
        Map(x => x.Stock);
        Map(x => x.Price);
        Map(x => x.Description);
        Table("Products");
    } 
}