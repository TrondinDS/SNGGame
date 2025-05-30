using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.UserService.UserSubscription
{
    public class UserSubscriptionCreateFrontDTO
    {
        [Required(ErrorMessage = "Поле EntityId является обязательным")]
        public Guid EntityId { get; set; }

        [Required(ErrorMessage = "Поле EntityType является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "EntityType должен быть положительным числом")]
        public int EntityType { get; set; }

    }
}
