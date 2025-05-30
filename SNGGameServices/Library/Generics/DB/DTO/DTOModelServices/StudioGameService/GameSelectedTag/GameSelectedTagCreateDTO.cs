using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedTag
{
    class GameSelectedTagCreateDTO
    {
        [Required(ErrorMessage = "GameId является обязательным")]
        public Guid GameId { get; set; }

        [Required(ErrorMessage = "TagId является обязательным")]
        public Guid TagId { get; set; }
    }
}
