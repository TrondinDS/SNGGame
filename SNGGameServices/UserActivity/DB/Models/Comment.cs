using Library;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserActivityService.DB.Models
{
    public class Comment : IIsDeleted
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public required string Body { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }

        [Required]
        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        public Topic Topic { get; set; }

        public ICollection<UserReaction> UserReactions { get; set; } = new List<UserReaction>();
    }
}
