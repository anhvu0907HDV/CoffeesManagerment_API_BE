using api_VS.Data;
using Assignment_PRN231_API.DTOs.Order;
using Assignment_PRN231_API.DTOs.Payment;
using Assignment_PRN231_API.DTOs.Recipe;
using Assignment_PRN231_API.DTOs.Staff;
using Assignment_PRN231_API.DTOs.User;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Assignment_PRN231_API.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

                var newPaymentId = Guid.NewGuid().ToString();

                var payment = new Payment
                {
                    PaymentId = newPaymentId,
                    PaymentMethod = createOrderDto.PaymentMethod,
                    PaymentStatus = "Pending"
                };
                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();

                // Tạo đơn hàng
                var order = new Order
                {
                    UserId = createOrderDto.UserId,
                    OrderDate = DateTime.Now,
                    TotalAmount = 0, // Tổng tiền sẽ được tính sau khi thêm các chi tiết đơn hàng
                    OrderStatus = "Pending",
                    PaymentId = newPaymentId
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




        public OrderDto GetOrderById(int orderId)
        {
            var order = _context.Orders
                .Where(o => o.OrderId == orderId)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    Users = _context.Users
                        .Where(u => u.Id == o.UserId)
                        .Select(u => new UserGetDto
                        {
                            FullName = u.FirstName + " " + u.LastName,
                            Role = _context.UserShops
                            .Where(us => us.UserId == u.Id)
                            .Select(us => us.Role)
                            .FirstOrDefault() ?? "Staff"
                        }).ToList(),

                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    OrderStatus = o.OrderStatus,
                    Payment = new PaymentDto
                    {
                        PaymentMethod = o.Payment.PaymentMethod,
                        PaymentStatus = o.Payment.PaymentStatus
                    },
                    OrderDetails = o.OrderDetails
                        .Select(od => new OrderDetailGetDto
                        {
                            ProductName = od.Product.ProductName,
                            Quantity = od.Quantity,
                            SubTotal = od.SubTotal
                        })
                        .ToList()
                })
                .FirstOrDefault(); // Ensure to return only one order based on the given OrderId

            return order;
        }

        public List<OrderDto> GetAllOrders()
        {
            var orders = _context.Orders
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    Users = _context.Users
                    .Where(u => u.Id == o.UserId)
                    .Select(u => new UserGetDto
                    {
                        FullName = u.FirstName + " " + u.LastName,
                        Role = _context.UserShops
                            .Where(us => us.UserId == u.Id)
                            .Select(us => us.Role)
                            .FirstOrDefault() ?? "Staff"
                    })
                    .ToList(),
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    OrderStatus = o.OrderStatus,
                    Payment = new PaymentDto
                    {
                        PaymentMethod = o.Payment.PaymentMethod,
                        PaymentStatus = o.Payment.PaymentStatus
                    },
                    OrderDetails = o.OrderDetails
                        .Select(od => new OrderDetailGetDto
                        {
                            ProductName = od.Product.ProductName,
                            Quantity = od.Quantity,
                            SubTotal = od.SubTotal
                        })
                        .ToList()
                })
                .ToList();

            return orders;
        }

		public List<OrderDto> GetOrdersByUserId(string userId)
		{
			var orders = _context.Orders
		        .Where(o => o.UserId == userId)
		        .Select(o => new OrderDto
		        {
			        OrderId = o.OrderId,
			        Users = _context.Users
				        .Where(u => u.Id == o.UserId)
				        .Select(u => new UserGetDto
				        {
					        FullName = u.FirstName + " " + u.LastName,
					        Role = _context.UserShops
						        .Where(us => us.UserId == u.Id)
						        .Select(us => us.Role)
						        .FirstOrDefault() ?? "Staff"
				        })
				        .ToList(),
			        OrderDate = o.OrderDate,
			        TotalAmount = o.TotalAmount,
			        OrderStatus = o.OrderStatus,
			        Payment = new PaymentDto
			        {
				        PaymentMethod = o.Payment.PaymentMethod,
				        PaymentStatus = o.Payment.PaymentStatus
			        },
			        OrderDetails = o.OrderDetails
				        .Select(od => new OrderDetailGetDto
				        {
					        ProductName = od.Product.ProductName,
					        Quantity = od.Quantity,
					        SubTotal = od.SubTotal
				        })
				        .ToList()
		        })
		        .ToList();

			        return orders;
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



        public RecipeDto? GetRecipeByProductId(int productId)
        {
            var recipe = _context.Recipes
                .Where(r => r.ProductId == productId)
                .Select(r => new RecipeDto
                {
                    RecipeId = r.RecipeId,
                    ProductName = r.Product.ProductName,
                    Description = r.Description,
                    RecipeDetails = r.RecipeDetails.Select(rd => new RecipeDetailDto
                    {
                        RecipeDetailId = rd.RecipeDetailId,
                        Quantity = rd.Quantity,
                        IngredientName = rd.Ingredient.IngredientName
                    }).ToList()
                })
                .FirstOrDefault();

            return recipe;
        }

        public async Task<bool> UpdateOrderInfo(int orderId, UpdateOrderDto dto)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null) return false;

            if (!string.IsNullOrEmpty(dto.UserId)) order.UserId = dto.UserId;
            if (dto.OrderDate.HasValue) order.OrderDate = dto.OrderDate.Value;
            if (!string.IsNullOrEmpty(dto.OrderStatus)) order.OrderStatus = dto.OrderStatus;
            if (!string.IsNullOrEmpty(dto.PaymentId)) order.PaymentId = dto.PaymentId;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddOrderDetail(int orderId, OrderDetailDto dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == dto.ProductId);
            if (product == null) throw new Exception("Sản phẩm không tồn tại.");

            var subTotal = product.Price * dto.Quantity;

            var newDetail = new OrderDetail
            {
                OrderId = orderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                SubTotal = subTotal
            };

            await _context.OrderDetails.AddAsync(newDetail);

            // Cập nhật tổng tiền
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order != null) order.TotalAmount += subTotal;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderDetail(int orderId, int productId, int quantity)
        {
            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);
            if (orderDetail == null) return false;

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null) throw new Exception("Sản phẩm không tồn tại.");

            // Cập nhật lại tổng tiền
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order != null && orderDetail.SubTotal.HasValue)
                order.TotalAmount -= orderDetail.SubTotal.Value;

            orderDetail.Quantity = quantity;
            orderDetail.SubTotal = product.Price * quantity;

            if (order != null) order.TotalAmount += orderDetail.SubTotal.Value;

            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteOrderDetail(int orderId, int productId)
        {
            var detail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);
            if (detail == null) return false;

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order != null && detail.SubTotal.HasValue)
                order.TotalAmount -= detail.SubTotal.Value;

            _context.OrderDetails.Remove(detail);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
