using System.ComponentModel.DataAnnotations;
using Library;

namespace UserActivityService.DB.Models
{
    public class Comment : IIsDeleted
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public required string Body { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        Topic Topic { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }
    }
}
