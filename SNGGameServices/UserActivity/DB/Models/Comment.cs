using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library;

namespace UserActivityService.DB.Models
{
    public class Comment : IIsDeleted
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }

        public int TopicId { get; set; }

        [ForeignKey("TopicId")]
        public Topic Topic { get; set; }

        public ICollection<UserReaction> UserReactions { get; set; } = new List<UserReaction>();
    }
}
