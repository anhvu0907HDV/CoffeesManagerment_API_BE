using Assignment_PRN231_API.DTOs.Order;
using Assignment_PRN231_API.DTOs.Recipe;
using Assignment_PRN231_API.DTOs.Staff;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(CreateOrderDto createOrderDto);
        OrderDto GetOrderById(int orderId);
        List<OrderDto> GetAllOrders();
        List<OrderDto> GetOrdersByUserId(string userId);

		Task<bool> DeleteOrder(int id);
        RecipeDto? GetRecipeByProductId(int productId);

        Task<bool> UpdateOrderInfo(int orderId, UpdateOrderDto dto);
        Task<bool> AddOrderDetail(int orderId, OrderDetailDto dto);
        Task<bool> UpdateOrderDetail(int orderId, int productId, int quantity);
        Task<bool> DeleteOrderDetail(int orderId, int productId);
        Task<int?> GetShopIdByUserIdAsync(string userId);
        Task<bool> UpdateOrderAndPaymentStatusAsync(int orderId, string orderStatus, string paymentStatus);
    }
}
