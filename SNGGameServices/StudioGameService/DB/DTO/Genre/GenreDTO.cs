using System.ComponentModel.DataAnnotations;

namespace StudioGameService.DB.DTO.Genre
{
    public class GenreDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title является обязательным")]
        [MaxLength(255, ErrorMessage = "Title не должно превышать 255 символов")]
        public required string Title { get; set; }
        [Required(ErrorMessage = "Description является обязательным")]
        [MaxLength(255, ErrorMessage = "Description не должно превышать 255 символов")]
        public required string Description { get; set; }
    }
}
