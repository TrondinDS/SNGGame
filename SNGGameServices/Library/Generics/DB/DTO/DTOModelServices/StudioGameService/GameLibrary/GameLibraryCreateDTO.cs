using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameLibrary
{
    class GameLibraryCreateDTO
    {
        [Required(ErrorMessage = "Статус является обязательным")]
        public int Status { get; set; }

        [Range(1, 10, ErrorMessage = "Рейтинг должен быть от 1 до 10")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "IsBought является обязательным")]
        public bool IsBought { get; set; }

        [Required(ErrorMessage = "GameId является обязательным")]
        public Guid GameId { get; set; }
    }
}
