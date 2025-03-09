namespace Asignment_PRN231_API_FE.ViewModel
{
    public class Toast
    {
        public string Message { get; set; }
        public string Type { get; set; } // Lớp CSS Bootstrap
        public string Status { get; set; } // Tiêu đề của thông báo

        // Enum đại diện cho loại thông báo
        public enum ToastType
        {
            Success,
            Error,
            Warning,
            Info
        }

        // Dictionary mapping ToastType to CSS classes
        private static readonly Dictionary<ToastType, (string, string)> ToastStyles =
            new Dictionary<ToastType, (string, string)>
            {
            { ToastType.Success, ("toast-header bg-success text-white", "Success") },
            { ToastType.Error, ("toast-header bg-danger text-white", "Error") },
            { ToastType.Warning, ("toast-header bg-warning text-dark", "Warning") },
            { ToastType.Info, ("toast-header bg-info text-white", "Information") }
            };

        // Default constructor for JSON deserialization
        public Toast() { }

        public Toast(string message, ToastType toastType)
        {
            Message = message;
            (Type, Status) = ToastStyles[toastType];
        }

        public static Toast RegisterSuccess(string message = "Register successfully. Please confirm your account in Email.")
            => new Toast(message, ToastType.Success); 
        public static Toast UpdateSuccess(string message = "Update Info successfully.")
            => new Toast(message, ToastType.Success);
        public static Toast RegisterError(string message = "Register Faile. Please try again.")
            => new Toast(message, ToastType.Error);
        public static Toast Success(string message = "Operation completed successfully.")
            => new Toast(message, ToastType.Success);

        public static Toast Error(string message = "An error occurred. Please try again.")
            => new Toast(message, ToastType.Error); 
        public static Toast UpdateError(string message = "Failed to update. Please try again.")
            => new Toast(message, ToastType.Error);

        public static Toast Warning(string message = "Please check your input.")
            => new Toast(message, ToastType.Warning);

        public static Toast Info(string message = "This is an informational message.")
            => new Toast(message, ToastType.Info);
    }
}
