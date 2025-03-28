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
        Task<int?> GetShopIdByUserIdAsync(string userId);
        Task<bool> UpdateOrderAndPaymentStatusAsync(int orderId, string orderStatus, string paymentStatus);
        Task<List<Table>> GetTablesByOrderIdAsync(int orderId);
        Task<List<Order>> GetOrdersByTableIdAsync(int tableId);
        Task<bool> UpdateTableStatusAsync(int tableId, bool status);
    }
}
