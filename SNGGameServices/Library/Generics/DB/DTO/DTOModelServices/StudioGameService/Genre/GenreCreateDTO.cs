using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre
{
    class GenreCreateDTO
    {
        [Required(ErrorMessage = "Title является обязательным")]
        [MaxLength(255, ErrorMessage = "Title не должно превышать 255 символов")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Description является обязательным")]
        [MaxLength(255, ErrorMessage = "Description не должно превышать 255 символов")]
        public required string Description { get; set; }
    }
}
