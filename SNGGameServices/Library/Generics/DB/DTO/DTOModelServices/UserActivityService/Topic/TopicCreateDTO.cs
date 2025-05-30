using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic
{
    class TopicCreateDTO
    {
        // Идентификатор связанной сущности
        [Required(ErrorMessage = "EntityId является обязательным")]
        public Guid EntityId { get; set; }

        // Тип связанной сущности
        [Required(ErrorMessage = "EntityType является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "EntityType должен быть положительным числом")]
        public int EntityType { get; set; }

        // Заголовок сущности
        [Required(ErrorMessage = "Title является обязательным")]
        [MaxLength(255, ErrorMessage = "Title не может превышать 255 символов")]
        [MinLength(1, ErrorMessage = "Title должна содержать хотя бы один символ")]
        public required string Title { get; set; }

        // Описание сущности (опционально)
        [MaxLength(1000, ErrorMessage = "Description не может превышать 1000 символов")]
        public string Description { get; set; }
    }
}
