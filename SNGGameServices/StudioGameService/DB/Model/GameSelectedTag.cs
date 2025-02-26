using System.ComponentModel.DataAnnotations;

namespace StudioGameService.DB.Model
{
    public class GameSelectedTag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int TagId { get; set; }
        public Game Game { get; set; } 
        public Tag Tag { get; set; }
    }
}
