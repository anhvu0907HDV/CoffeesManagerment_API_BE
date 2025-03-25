using System.Security.Cryptography;
using System.Text;


namespace Assignment_PRN231_API.Service
{
    public class VNPayService
    {
        private readonly string vnp_TmnCode = "YOUR_TMN_CODE";  // Mã thương gia
        private readonly string vnp_HashSecret = "YOUR_HASH_SECRET";  // Key bí mật
        private readonly string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";  // URL VNPay (sandbox hoặc live)
        private readonly string vnp_ReturnUrl = "YOUR_RETURN_URL";  // URL trả về của bạn
        private readonly string vnp_NotifyUrl = "YOUR_NOTIFY_URL";  // URL thông báo (dùng để xử lý thông báo thanh toán)

        public string CreatePaymentUrl(int orderId, decimal totalAmount)
        {
            var vnp_Params = new SortedDictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", vnp_TmnCode },
                { "vnp_Amount", ((long)(totalAmount)).ToString() },  // Sử dụng số tiền tính theo VND
                { "vnp_CurrCode", "VND" },  // Tiền tệ là VND
                { "vnp_TxnRef", orderId.ToString() },  // Mã đơn hàng
                { "vnp_OrderInfo", $"Order #{orderId}" },
                { "vnp_OrderType", "billpayment" },
                { "vnp_Locale", "vn" },
                { "vnp_ReturnUrl", vnp_ReturnUrl },
                { "vnp_NotifyUrl", vnp_NotifyUrl },
                { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
            };


            // Tạo mã bảo mật
            var hashData = string.Join("&", vnp_Params.Select(kv => $"{kv.Key}={kv.Value}"));
            var hashString = $"vnp_HashSecret={vnp_HashSecret}&{hashData}";
            var hash = GetMD5Hash(hashString).ToUpper();
            vnp_Params.Add("vnp_SecureHash", hash);

            // Tạo URL thanh toán
            var queryString = string.Join("&", vnp_Params.Select(kv => $"{kv.Key}={kv.Value}"));
            var paymentUrl = $"{vnp_Url}?{queryString}";

            return paymentUrl;
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
