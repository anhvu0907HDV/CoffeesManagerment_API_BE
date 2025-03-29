using Assignment_PRN231_API.DTOs.Order;
using Assignment_PRN231_API.DTOs.Staff;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    namespace Assignment_PRN231_API.Controllers
    {
        [Route("staff")]
        [ApiController]
        public class StaffController : ControllerBase
        {
            private readonly IOrderRepository _orderRepository;
            private readonly ITableRepository _tableRepository;
            private readonly IMapper _mapper;

            public StaffController(IOrderRepository orderRepository, IMapper mapper, ITableRepository tableRepository)
            {
                _orderRepository = orderRepository;
                _tableRepository = tableRepository;
                _mapper = mapper;
            }

            [HttpPost("createOrder")]
            public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
            {
                if (createOrderDto == null || createOrderDto.OrderDetails.Count == 0)
                {
                    return BadRequest("Invalid order data.");
                }

                try
                {
                    var order = await _orderRepository.CreateOrder(createOrderDto);
                    return StatusCode(201, new
                    {
                        message = "✅ Tạo đơn hàng thành công!",
                        orderId = order.OrderId,
                        total = order.TotalAmount
                    });
            }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error occurred while creating the order: {ex.Message}");
                }
            }

            [HttpGet("GetOrderById/{orderId}")]
            public IActionResult GetOrderById(int orderId)
            {
                var order = _orderRepository.GetOrderById(orderId);

                if (order == null)
                {
                    return NotFound(new { message = "Order not found." });
                }

                return Ok(order);
            }

            [HttpGet("GetAllOrders")]
            public IActionResult GetAllOrders()
            {
                var orders = _orderRepository.GetAllOrders();

                if (orders == null || orders.Count == 0)
                {
                    return NotFound(new { message = "No orders found." });
                }

                return Ok(orders);
            }

		[HttpGet("GetOrdersByUserId/{userId}")]
		public IActionResult GetOrdersByUserId(string userId)
		{
			var orders = _orderRepository.GetOrdersByUserId(userId);

			if (orders == null || orders.Count == 0)
			{
				return NotFound(new { message = "Không tìm thấy đơn hàng cho UserId này." });
			}

			return Ok(orders);
		}


		[HttpDelete("DeleteOrder/{id}")]
            public async Task<IActionResult> DeleteOrder(int id)
            {
                var isDeleted = await _orderRepository.DeleteOrder(id);

                if (!isDeleted)
                {
                    return NotFound("Không tìm thấy đơn hàng để xóa.");
                }

                return NoContent();
            }


            [HttpGet("GetRecipeByProductId/{productId}")]
            public IActionResult GetRecipeByProductId(int productId)
            {
                var recipe = _orderRepository.GetRecipeByProductId(productId);

                if (recipe == null)
                {
                    return NotFound(new { message = "Recipe not found for this ProductId." });
                }

                return Ok(recipe);
            }


        // PUT /staff/orders/{id}
        [HttpPut("UpdateOrderInfo/{id}")]
        public async Task<IActionResult> UpdateOrderInfo(int id, [FromBody] UpdateOrderDto dto)
        {
            var success = await _orderRepository.UpdateOrderInfo(id, dto);
            if (success)
            {
                return Ok("✅ Đã cập nhật thông tin đơn hàng.");
            }
            else
            {
                return NotFound(new { message = "Đơn hàng không tồn tại." });
            }
        }

            [HttpGet("get-all-tables")]
            public async Task<IActionResult> GetAllTables()
            {
                try
                {
                    var tables = await _tableRepository.GetAllTablesAsync();
                    if (tables == null || tables.Count == 0)
                    {
                        return NotFound(new { message = "No tables found." });
                    }
                    return Ok(tables);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = $"An error occurred while retrieving tables: {ex.Message}" });
                }
            }

            [HttpGet("get-shop-id-by-user/{userId}")]
            public async Task<IActionResult> GetShopIdByUserId(string userId)
            {
                var shopId = await _orderRepository.GetShopIdByUserIdAsync(userId);

                if (shopId == null)
                {
                    return NotFound(new { message = $"ShopId not found for UserId: {userId}" });
                }

                return Ok(new { ShopId = shopId });
            }

            [HttpGet("get-tables-by-shop/{shopId}")]
            public async Task<IActionResult> GetTablesByShopId(int shopId)
            {
                // Lấy tất cả các bảng từ repository
                var tables = await _tableRepository.GetTablesByShopIdAsync(shopId);

                if (tables == null || tables.Count == 0)
                {
                    return NotFound(new { message = $"No tables found for ShopId: {shopId}" });
                }

                // Chuyển đổi Table thành TableForOrderDTO
                var tableForOrderDTOs = new List<TableForOrderDTO>();
                foreach (var table in tables)
                {
                    tableForOrderDTOs.Add(new TableForOrderDTO
                    {
                        TableId = table.TableId,
                        Name = table.Name,
                        Status = table.Status
                    });
                }

                return Ok(new { Tables = tableForOrderDTOs });
            }

            [HttpPut("update-order-status/{orderId}")]
            public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusDto statusDto)
            {
                if (statusDto == null || string.IsNullOrEmpty(statusDto.OrderStatus) || string.IsNullOrEmpty(statusDto.PaymentStatus))
                {
                    return BadRequest("Thông tin trạng thái không hợp lệ.");
                }

                bool result = await _orderRepository.UpdateOrderAndPaymentStatusAsync(orderId, statusDto.OrderStatus, statusDto.PaymentStatus);

                if (!result)
                {
                    return NotFound(new { message = "Không tìm thấy đơn hàng để cập nhật trạng thái." });
                }

                return Ok(new { message = "Cập nhật trạng thái đơn hàng và thanh toán thành công." });
            }


            [HttpGet("get-tables-by-order/{orderId}")]
            public async Task<IActionResult> GetTablesByOrderId(int orderId)
            {
                var tables = await _orderRepository.GetTablesByOrderIdAsync(orderId);
                if (tables == null || tables.Count == 0)
                {
                    return NotFound("No tables found for the given order.");
                }

                var result = tables.Select(t => new
                {
                    t.TableId,
                    t.Name,
                    t.Status,
                    t.ShopId
                });

                return Ok(result);
            }

            [HttpGet("get-orders-by-table/{tableId}")]
            public async Task<IActionResult> GetOrdersByTableId(int tableId)
            {
                var orders = await _orderRepository.GetOrdersByTableIdAsync(tableId);
                if (orders == null || !orders.Any())
                {
                    return NotFound("Không tìm thấy đơn hàng nào cho bàn này.");
                }

                // Bạn có thể map sang DTO nếu cần
                var result = orders.Select(o => new
                {
                    o.OrderId,
                    o.OrderDate,
                    o.TotalAmount,
                    o.OrderStatus,
                    o.Payment.PaymentMethod
                });

                return Ok(result);
            }

            [HttpPut("update-table-status/{tableId}")]
            public async Task<IActionResult> UpdateTableStatus(int tableId, [FromBody] UpdateTableStatusRequest request)
            {
                var success = await _orderRepository.UpdateTableStatusAsync(tableId, request.Status);
                if (!success)
                {
                    return NotFound("Không tìm thấy bàn.");
                }

                return Ok(new { message = "Cập nhật trạng thái bàn thành công." });
            }

            [HttpGet("get-user-id")]
            public async Task<IActionResult> GetUserIdByEmail([FromQuery] string email)
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest("Email is required.");

                var userId = await _orderRepository.GetUserIdByEmailAsync(email);

                if (userId == null)
                    return NotFound("User not found.");

                return Ok(new { UserId = userId });
            }

    }
 }

