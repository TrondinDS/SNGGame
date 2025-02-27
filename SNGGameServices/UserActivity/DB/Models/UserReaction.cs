using System.ComponentModel.DataAnnotations;

namespace UserActivityService.DB.Models
{
    public class UserReaction
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
    }
}
