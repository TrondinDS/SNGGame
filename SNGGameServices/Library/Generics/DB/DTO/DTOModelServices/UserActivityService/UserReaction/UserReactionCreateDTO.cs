using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.UserActivityService.UserReaction
{
    public class UserReactionCreateDTO
    {
        public int ReactionType { get; set; }

        // Идентификатор комментария, на который дана реакция
        [Required(ErrorMessage = "CommentId является обязательным")]
        public Guid CommentId { get; set; }
    }
}
