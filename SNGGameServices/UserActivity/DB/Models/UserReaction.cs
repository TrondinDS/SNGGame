using System.ComponentModel.DataAnnotations;
using UserActivityService.DB.Models.Interfaces;

namespace UserActivityService.DB.Models
{
    public class UserReaction : IsDeleted
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CommentId { get; set; }
        [Required]
        public int ReactionType { get; set; }
        public Comment Comment { get; set; }
        public bool IsDelet { get; set; }
    }
}
