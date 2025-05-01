using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioGameService.DB.Model
{
    public class StatisticGame
    {
        [Key]
        public Guid Id { get; set; }
        public int RatingSum { get; set; }
        public int PeopleCount { get; set; }

        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
