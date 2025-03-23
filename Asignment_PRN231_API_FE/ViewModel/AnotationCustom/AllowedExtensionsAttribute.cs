using System.ComponentModel.DataAnnotations;

namespace Asignment_PRN231_API_FE.ViewModel.AnotationCustom
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"Chỉ cho phép định dạng: {string.Join(", ", _extensions)}");
                }
            }
            return ValidationResult.Success!;
        }
    }
}
