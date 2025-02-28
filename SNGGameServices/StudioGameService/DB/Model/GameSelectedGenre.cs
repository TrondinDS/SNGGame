using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioGameService.DB.Model
{
    public class GameSelectedGenre
    {
        [Key]
        public int Id { get; set; }
        public int NumberOrder { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int GenreId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
    }
}
