using api_VS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Assignment_PRN231_API.Controllers
{
    [Route("payment")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PaymentController(ApplicationDBContext context)
        {
            _context = context;
        }

        // Xử lý callback từ VNPay
        [HttpGet("callback")]
        public async Task<IActionResult> PaymentCallback([FromQuery] string vnp_TxnRef, [FromQuery] string vnp_SecureHash)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId.ToString() == vnp_TxnRef);
            if (order == null)
            {
                return BadRequest("Order not found.");
            }

            // Lấy các tham số từ VNPay gửi về để tạo expected hash
            var vnp_Params = Request.Query;
            string hashData = "vnp_Version=" + vnp_Params["vnp_Version"] +
                              "&vnp_Command=" + vnp_Params["vnp_Command"] +
                              "&vnp_TmnCode=" + vnp_Params["vnp_TmnCode"] +
                              "&vnp_Amount=" + vnp_Params["vnp_Amount"] +
                              "&vnp_CurrCode=" + vnp_Params["vnp_CurrCode"] +
                              "&vnp_TxnRef=" + vnp_Params["vnp_TxnRef"] +
                              "&vnp_OrderInfo=" + vnp_Params["vnp_OrderInfo"] +
                              "&vnp_OrderType=" + vnp_Params["vnp_OrderType"] +
                              "&vnp_Locale=" + vnp_Params["vnp_Locale"] +
                              "&vnp_ReturnUrl=" + vnp_Params["vnp_ReturnUrl"] +
                              "&vnp_NotifyUrl=" + vnp_Params["vnp_NotifyUrl"] +
                              "&vnp_CreateDate=" + vnp_Params["vnp_CreateDate"];

            // Thêm phần secret key
            string vnp_HashSecret = "YOUR_HASH_SECRET";  // Thay thế với key bí mật của bạn
            string hashString = vnp_HashSecret + "&" + hashData;

            // Tính toán MD5 hash từ chuỗi hashString
            string expectedHash = GetMD5Hash(hashString).ToUpper();

            // So sánh expectedHash với vnp_SecureHash
            if (expectedHash != vnp_SecureHash)
            {
                return BadRequest("Invalid payment signature.");
            }

            // Cập nhật trạng thái đơn hàng sau khi thanh toán
            var paymentStatus = vnp_Params["vnp_ResponseCode"];
            if (paymentStatus == "00")  // Thanh toán thành công
            {
                order.OrderStatus = "Paid";
            }
            else
            {
                order.OrderStatus = "Failed";
            }

            // Cập nhật trạng thái thanh toán trong bảng Payment
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == order.OrderId);
            if (payment != null)
            {
                payment.PaymentStatus = order.OrderStatus == "Paid" ? "Completed" : "Failed";
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return string.Concat(hashBytes.Select(b => b.ToString("x2")));
            }
        }
    }
}
