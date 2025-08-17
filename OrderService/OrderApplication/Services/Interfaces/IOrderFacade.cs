using OrderApplication.DTOs;
using OrderBusiness.Entities;

namespace OrderApplication.Services.Interfaces;

public interface IOrderFacade
{
    Task<bool> CreateOrder(CreateOrderDto createOrderDto);
    Task<List<OrderDto>> GetOrders();
    Task<OrderDto?> GetOrder(int id);
    Task<bool?> CancelOrder(int id);
}