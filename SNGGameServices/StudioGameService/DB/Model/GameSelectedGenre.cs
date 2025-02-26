using System.ComponentModel.DataAnnotations;

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
        public Game Game { get; set; }
        public Genre Genre { get; set; }
    }
}
