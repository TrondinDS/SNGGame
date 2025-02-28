using System.ComponentModel.DataAnnotations;

namespace StudioGameService.DB.Model
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }
        public ICollection<GameSelectedTag> Games { get; set; } = new List<GameSelectedTag>();
    }
}
