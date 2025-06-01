using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.UserService.Job
{
    public class JobCreateDTOFront
    {
        [Required(ErrorMessage = "EntityId является обязательным")]
        public Guid EntityId { get; set; }

        [Required(ErrorMessage = "EntityType является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "EntityType должен быть положительным числом")]
        public required int EntityType { get; set; }

        [Required(ErrorMessage = "IsModerator является обязательным")]
        public bool IsModerator { get; set; }

        [MaxLength(255, ErrorMessage = "Position не может превышать 255 символов")]
        [MinLength(1, ErrorMessage = "Position должна содержать хотя бы один символ")]
        public string Position { get; set; }
    }
}
