using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame
{
    class StatisticGameCreateDTO
    {
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
