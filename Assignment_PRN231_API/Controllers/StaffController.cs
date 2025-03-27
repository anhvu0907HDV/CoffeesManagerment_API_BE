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
            private readonly IMapper _mapper;

            public StaffController(IOrderRepository orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
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


		[HttpDelete("DeleteOrder{id}")]
            public async Task<IActionResult> DeleteOrder(int id)
            {
                var isDeleted = await _orderRepository.DeleteOrder(id);

                if (!isDeleted)
                {
                    return NotFound("Không tìm thấy đơn hàng để xóa.");
                }

                return NoContent();
            }


            [HttpGet("GetRecipeByProductId{productId}")]
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
            [HttpPut("UpdateOrderInfo{id}")]
            public async Task<IActionResult> UpdateOrderInfo(int id, [FromBody] UpdateOrderDto dto)
            {
                var success = await _orderRepository.UpdateOrderInfo(id, dto);
                return success ? Ok("✅ Đã cập nhật thông tin đơn hàng.") : NotFound("Đơn hàng không tồn tại.");
            }

            // POST /staff/orders/{id}/details
            [HttpPost("AddOrderDetail{id}")]
            public async Task<IActionResult> AddOrderDetail(int id, [FromBody] OrderDetailDto dto)
            {
                var success = await _orderRepository.AddOrderDetail(id, dto);
                return success ? Ok("✅ Đã thêm sản phẩm vào đơn hàng.") : NotFound("Không tìm thấy đơn hàng hoặc sản phẩm.");
            }

            // PUT /staff/orders/{id}/details/{productId}
            [HttpPut("UpdateOrderDetail{id}/{productId}")]
            public async Task<IActionResult> UpdateOrderDetail(int id, int productId, [FromBody] int quantity)
            {
                var success = await _orderRepository.UpdateOrderDetail(id, productId, quantity);
                return success ? Ok("✅ Đã cập nhật số lượng sản phẩm.") : NotFound("Không tìm thấy sản phẩm trong đơn hàng.");
            }

            // DELETE /staff/orders/{id}/details/{productId}
            [HttpDelete("DeleteOrderDetail{id}/{productId}")]
            public async Task<IActionResult> DeleteOrderDetail(int id, int productId)
            {
                var success = await _orderRepository.DeleteOrderDetail(id, productId);
                return success ? Ok("🗑️ Đã xóa sản phẩm khỏi đơn hàng.") : NotFound("Không tìm thấy sản phẩm để xóa.");
            }
    }
    }

