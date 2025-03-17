using System.ComponentModel.DataAnnotations;
using Library;

namespace UserActivityService.DB.Models
{
    public class Topic : IIsDeleted, IEntity
    {
        [Key]
        public int Id { get; set; }
        public int EntityId { get; set; }
        public int EntityType { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserCreatorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
