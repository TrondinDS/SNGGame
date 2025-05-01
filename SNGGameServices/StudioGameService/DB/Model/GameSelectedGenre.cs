using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioGameService.DB.Model
{
    public class GameSelectedGenre
    {
        [Key]
        public Guid Id { get; set; }
        public int NumberOrder { get; set; }

        [Required]
        public Guid GameId { get; set; }

        [Required]
        public Guid GenreId { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
    }
}
