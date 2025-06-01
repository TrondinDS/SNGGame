using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio
{
    public class StudioCreateDTO
    {
        [Required(ErrorMessage = "Название является обязательным")]
        [MaxLength(255, ErrorMessage = "Название не должно превышать 255 символов")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Email является обязательным")]
        [MaxLength(255, ErrorMessage = "Email не должен превышать 255 символов")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Image является обязательным")]
        public string Image { get; set; }
        [Required(ErrorMessage = "ImageType является обязательным")]
        public string ImageType { get; set; }
        [Required(ErrorMessage = "Content является обязательным")]
        public string Content { get; set; }
    }
}
