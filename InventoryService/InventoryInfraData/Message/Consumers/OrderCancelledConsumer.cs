using InventoryBusiness.Repositories;
using InventoryInfraData.Message.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Contracts;

namespace InventoryInfraData.Message.Consumers;

public class OrderCancelledConsumer (IServiceProvider serviceProvider): BackgroundService
{
    private const string OrderCancelledQueue = "OrderCancelled";
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
                await consumer.ConsumeMessage<OrderCancelledEvent>(OrderCancelledQueue, UpdateProductStock, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
    
    private async Task UpdateProductStock(OrderCancelledEvent orderCancelledEvent)
    {
        using var scope = serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
        foreach (var item in orderCancelledEvent.Products)
        {
            try
            {
                var product = await repository.GetById(item.ProductId);
                if (product != null)
                {
                    product.AddStock(item.Quantity);
                    await repository.Update(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    } 
}