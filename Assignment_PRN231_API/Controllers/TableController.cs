using Assignment_PRN231_API.DTOs.Table;
using Assignment_PRN231_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("api/tables")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;

        public TableController(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        // Lấy danh sách bàn theo ShopId
        [HttpGet("shop/{shopId}")]
        public async Task<IActionResult> GetTablesByShop(int shopId)
        {
            var tables = await _tableRepository.GetAllTablesAsync(shopId);
            return Ok(tables);
        }

        // Lấy thông tin bàn theo TableId
        [HttpGet("{tableId}")]
        public async Task<IActionResult> GetTableById(int tableId)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            if (table == null) return NotFound(new { Message = "Bàn không tồn tại" });
            return Ok(table);
        }

        // Tạo bàn mới
        [HttpPost]
        public async Task<IActionResult> CreateTable([FromBody] TableDto tableDto)
        {
            if (tableDto == null) return BadRequest(new { Message = "Dữ liệu không hợp lệ" });

            var success = await _tableRepository.CreateTableAsync(tableDto);
            if (!success) return StatusCode(500, new { Message = "Lỗi khi tạo bàn" });

            return StatusCode(201, new { Message = "Bàn đã được tạo thành công!" });
        }

        // Cập nhật bàn
        [HttpPut("{tableId}")]
        public async Task<IActionResult> UpdateTable(int tableId, [FromBody] TableDto tableDto)
        {
            if (tableDto == null) return BadRequest(new { Message = "Dữ liệu không hợp lệ" });

            var success = await _tableRepository.UpdateTableAsync(tableId, tableDto);
            if (!success) return NotFound(new { Message = "Không tìm thấy bàn hoặc lỗi khi cập nhật" });

            return Ok(new { Message = "Cập nhật bàn thành công!" });
        }

        // Xóa bàn
        [HttpDelete("{tableId}")]
        public async Task<IActionResult> DeleteTable(int tableId)
        {
            var success = await _tableRepository.DeleteTableAsync(tableId);
            if (!success) return NotFound(new { Message = "Không tìm thấy bàn để xóa" });

            return Ok(new { Message = "Bàn đã được xóa thành công!" });
        }
    }

}
