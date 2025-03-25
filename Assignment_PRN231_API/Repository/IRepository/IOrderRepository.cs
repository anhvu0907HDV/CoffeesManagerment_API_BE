using Assignment_PRN231_API.DTOs.Staff;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(CreateOrderDto createOrderDto);
        Task<Order> GetOrder(int id);
        Task<List<Order>> GetAllOrders();
        Task<bool> DeleteOrder(int id);
    }
}
