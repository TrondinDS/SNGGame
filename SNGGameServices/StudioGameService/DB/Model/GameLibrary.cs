using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioGameService.DB.Model
{
    public class GameLibrary
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public int Status { get; set; }
        public int Rating { get; set; }
        public bool IsBought { get; set; }

        public Guid GameId { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
