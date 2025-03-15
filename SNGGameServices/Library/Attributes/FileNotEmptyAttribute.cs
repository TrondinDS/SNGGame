using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Library.Attributes
{
    public class FileNotEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            if (value is IFormFile file)
            {
                if (file.Length > 0)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("The uploaded file must have a size greater than 0.");
        }
    }
}
