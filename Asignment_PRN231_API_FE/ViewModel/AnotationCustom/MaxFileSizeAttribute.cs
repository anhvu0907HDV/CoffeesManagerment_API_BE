using System.ComponentModel.DataAnnotations;

namespace Asignment_PRN231_API_FE.ViewModel.AnotationCustom
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;
        public MaxFileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxSize)
                {
                    return new ValidationResult($"File không được vượt quá {_maxSize / (1024 * 1024)}MB.");
                }
            }
            return ValidationResult.Success!;
        }
    }
}
