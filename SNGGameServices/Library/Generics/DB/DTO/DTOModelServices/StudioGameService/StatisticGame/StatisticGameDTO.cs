using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame
{
    public class StatisticGameDTO
    {
        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserCreatorId должен быть корректным GUID"
        )]
        public Guid Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "EntityType должен быть положительным числом")]
        public int RatingSum { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "EntityType должен быть положительным числом")]
        public int PeopleCount { get; set; }

        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "UserCreatorId должен быть корректным GUID"
        )]
        public Guid GameId { get; set; }
    }
}
