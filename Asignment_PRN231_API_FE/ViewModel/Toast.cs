namespace Asignment_PRN231_API_FE.ViewModel
{
    public class Toast
    {
        public string Message { get; }
        public string Type { get; }
        public string Status { get; }


        // Constructor khởi tạo
        private Toast(string message, string type, string status)
        {
            Message = message;
            Type = type;
            Status = status;
        }

        public Toast()
        {
        }

        // Static method tạo thông báo thành công
        public static Toast Success()
        {
            return new Toast(
                "Đăng ký thành công.",
                "toast-header bg-success text-white",
                "Success"
            );
        }

        // Static method tạo thông báo lỗi
        public static Toast Error()
        {
            return new Toast(
                "Đăng ký thất bại. Vui lòng kiểm tra lại thông tin.",
                "toast-header bg-danger text-white",
                "Error"

            );
        }

    }
}
