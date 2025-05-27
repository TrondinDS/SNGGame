using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library;

namespace UserActivityService.DB.Models
{
    public class Comment : IIsDeleted
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }

        public Guid TopicId { get; set; }

        [ForeignKey("TopicId")]
        public Topic Topic { get; set; }

        public int CountLike { get; set; }

        public Guid? CommentIdReference { get; set; }

        [ForeignKey("CommentIdReference")]
        public Comment? CommentReference { get; set; }

        public Guid? CommentIdResponse { get; set; }

        [ForeignKey("CommentIdResponse")]
        public Comment? CommentResponse { get; set; }


        public ICollection<UserReaction> UserReactions { get; set; } = new List<UserReaction>();
    }
}
