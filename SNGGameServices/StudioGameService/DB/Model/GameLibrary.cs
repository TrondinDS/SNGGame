using System.ComponentModel.DataAnnotations;

namespace StudioGameService.DB.Model
{
    public class GameLibrary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GameId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public int Status { get; set; }
        public int Rating { get; set; }
        public bool IsBought { get; set; }
        public Game Game { get; set; }
    }
}
