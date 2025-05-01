using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioGameService.DB.Model
{
    public class GameSelectedTag
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid GameId { get; set; }

        [Required]
        public Guid TagId { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
