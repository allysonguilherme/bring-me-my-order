using OrderApplication.DTOs;
using OrderBusiness.Entities;

namespace OrderApplication.Services.Interfaces;

public interface IOrderFacade
{
    Task<bool> CreateOrder(CreateOrderDto createOrderDto);
    Task<List<OrderDto>> GetOrders();
}