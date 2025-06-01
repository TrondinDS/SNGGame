using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment
{
    public class CommentCreateDTO
    {
        // Текст комментария
        [Required(ErrorMessage = "Body является обязательным")]
        [MaxLength(1000, ErrorMessage = "Body не может превышать 1000 символов")]
        [MinLength(1, ErrorMessage = "Body должна содержать хотя бы один символ")]
        public required string Body { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "NumberOrder должено быть положительным числом")]
        public int CountLike { get; set; }

        // Идентификатор темы, к которой относится комментарий
        [Required(ErrorMessage = "TopicId является обязательным")]
        public Guid TopicId { get; set; }

        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "CommentIdReference должен быть корректным GUID"
        )]
        public Guid? CommentIdReference { get; set; }
        [RegularExpression(
            "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{12}$",
            ErrorMessage = "CommentIdResponse должен быть корректным GUID"
        )]
        public Guid? CommentIdResponse { get; set; }
    }
}
