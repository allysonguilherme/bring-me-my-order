using InventoryBusiness.Entities;
using FluentNHibernate.Mapping;

namespace InventoryInfraData.Mappings;

public class ProductMap: ClassMap<Product>
{
    public ProductMap()
    {
        Id(x => x.Id).GeneratedBy.Sequence("products_id_seq");
        Map(x => x.Name);
        Map(x => x.Stock);
        Map(x => x.Price);
        Map(x => x.Description);
        Table("Products");
    } 
}