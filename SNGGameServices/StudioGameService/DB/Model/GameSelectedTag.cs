using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("GameId")]
        public Game Game { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
