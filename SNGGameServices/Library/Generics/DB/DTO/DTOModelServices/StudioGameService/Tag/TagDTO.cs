using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag
{
    public class TagDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title является обязательным")]
        [MaxLength(255, ErrorMessage = "Title не должен превышать 255 символов")]
        public required string Title { get; set; }
    }
}
