using Library.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO
{
    public class ImgDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Image file is required.")]
        [FileNotEmpty]
        public IFormFile ImageFile { get; set; }
    }
}
