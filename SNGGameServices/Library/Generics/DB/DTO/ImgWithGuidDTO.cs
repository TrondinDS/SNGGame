using Library.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO
{
    public class ImgWithGuidDTO
    {
        [Required(ErrorMessage = "UserId is required.")]
        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "id должен быть корректным GUID"
        )]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Image file is required.")]
        [FileNotEmpty]
        public IFormFile ImageFile { get; set; }
    }
}
