using api_VS.Data;
using Assignment_PRN231_API.DTOs.Staff;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Order> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                // Kiểm tra UserId
                var userExists = await _context.Users.AnyAsync(u => u.Id == createOrderDto.UserId);
                if (!userExists) throw new Exception("⚠ UserId không tồn tại!");

                // Kiểm tra TableId
                var table = await _context.Tables.FirstOrDefaultAsync(t => t.TableId == createOrderDto.TableId);
                if (table == null) throw new Exception("⚠ TableId không hợp lệ!");
                var shopId = table.ShopId;

                // Kiểm tra PaymentId
                var paymentExists = await _context.Payments.AnyAsync(p => p.PaymentId == createOrderDto.PaymentId);
                if (!paymentExists) throw new Exception("⚠ PaymentId không tồn tại!");

                // Tạo đơn hàng
                var order = new Order
                {
                    UserId = createOrderDto.UserId,
                    OrderDate = DateTime.Now,
                    TotalAmount = 0, // Tổng tiền sẽ được tính sau khi thêm các chi tiết đơn hàng
                    OrderStatus = "Pending",
                    PaymentId = createOrderDto.PaymentId
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                // Thêm chi tiết đơn hàng và cập nhật tổng tiền
                foreach (var item in createOrderDto.OrderDetails)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == item.ProductId);
                    if (product == null) throw new Exception($"⚠ Sản phẩm với ProductId {item.ProductId} không tồn tại!");

                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        SubTotal = product.Price * item.Quantity // Tính SubTotal
                    };

                    await _context.OrderDetails.AddAsync(orderDetail);

                    if (orderDetail.SubTotal.HasValue)
                    {
                        order.TotalAmount += orderDetail.SubTotal.Value;
                    } // Cộng dồn vào tổng tiền của đơn hàng
                }

                await _context.SaveChangesAsync();

                // Cập nhật bảng TableOrder
                var tableOrder = new TableOrder
                {
                    OrderId = order.OrderId,
                    TableId = createOrderDto.TableId
                };
                await _context.TableOrders.AddAsync(tableOrder);

                // Cập nhật trạng thái của bàn trong bảng Tables
                table.Status = true;
                _context.Tables.Update(table);

                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 ERROR: {ex.Message}");
                Console.WriteLine($"🔥 INNER EXCEPTION: {ex.InnerException?.Message}");
                throw new Exception($"Lỗi xảy ra khi tạo đơn hàng: {ex.Message}");
            }
        }





        public async Task<Order> GetOrder(int id)
        {
            return await _context.Orders
                                 .Include(o => o.OrderDetails)
                                 .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
        }

        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                // Tìm đơn hàng cần xóa cùng các chi tiết và liên kết với bảng TableOrder
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.OrderId == id);

                if (order == null)
                {
                    throw new Exception("Đơn hàng không tồn tại.");
                }

                // Xóa các liên kết trong bảng TableOrder
                var tableOrders = await _context.TableOrders
                    .Where(to => to.OrderId == id)
                    .ToListAsync();

                _context.TableOrders.RemoveRange(tableOrders);

                // Xóa các chi tiết đơn hàng
                var orderDetails = await _context.OrderDetails
                    .Where(od => od.OrderId == id)
                    .ToListAsync();

                _context.OrderDetails.RemoveRange(orderDetails);

                // Xóa đơn hàng
                _context.Orders.Remove(order);

                // Cập nhật trạng thái của các bàn liên quan
                foreach (var tableOrder in tableOrders)
                {
                    var table = await _context.Tables
                        .FirstOrDefaultAsync(t => t.TableId == tableOrder.TableId);

                    if (table != null)
                    {
                        // Kiểm tra xem bàn còn đơn hàng nào không
                        var hasOrders = await _context.TableOrders
                            .AnyAsync(to => to.TableId == table.TableId);

                        // Nếu không còn đơn hàng nào, cập nhật trạng thái bàn về false
                        if (!hasOrders)
                        {
                            table.Status = false;
                            _context.Tables.Update(table);
                        }
                    }
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 ERROR: {ex.Message}");
                throw new Exception($"Lỗi khi xóa đơn hàng: {ex.Message}");
            }
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string newStatus)
        {
            try
            {
                // Tìm đơn hàng cần cập nhật
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (order == null)
                {
                    throw new Exception("Đơn hàng không tồn tại.");
                }

                // Cập nhật trạng thái đơn hàng
                order.OrderStatus = newStatus;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 ERROR: {ex.Message}");
                throw new Exception($"Lỗi khi cập nhật trạng thái đơn hàng: {ex.Message}");
            }
        }


    }
}
