using System.ComponentModel.DataAnnotations;

namespace StudioGameService.DB.DTO.Tag
{
    public class TagDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title является обязательным")]
        [MaxLength(255, ErrorMessage = "Title не должен превышать 255 символов")]
        public required string Title { get; set; }
    }
}
