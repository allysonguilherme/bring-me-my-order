using InventoryBusiness.Repositories;
using InventoryInfraData.Message.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Contracts;

namespace InventoryInfraData.Message.Consumers;

public class OrderCreatedConsumer(IServiceProvider serviceProvider): BackgroundService
{
    private const string OrderCreatedQueue = "OrderCreated";
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
                await consumer.ConsumeMessage<OrderCreatedEvent>(OrderCreatedQueue, UpdateProductStock, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await Task.Delay(5000, stoppingToken);
            }
        }

    }

    private async Task UpdateProductStock(OrderCreatedEvent orderCreatedEvent)
    {
        using var scope = serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
        foreach (var item in orderCreatedEvent.Products)
        {
            try
            {
                var product = await repository.GetById(item.ProductId);
                if (product != null)
                {
                    product.WithdrawStock(item.Quantity);
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