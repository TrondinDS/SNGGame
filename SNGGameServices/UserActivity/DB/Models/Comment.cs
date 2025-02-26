using System.ComponentModel.DataAnnotations;
using UserActivityService.DB.Models.Interfaces;

namespace UserActivityService.DB.Models
{
    public class Comment : IsDeleted
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
        public bool IsDelet { get; set; }
    }
}
