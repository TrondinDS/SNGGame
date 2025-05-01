using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserActivityService.DB.Models
{
    public class UserReaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int ReactionType { get; set; }

        [Required]
        public Guid CommentId { get; set; }

        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }
    }
}
