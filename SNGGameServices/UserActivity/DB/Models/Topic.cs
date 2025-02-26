using System.ComponentModel.DataAnnotations;
using UserActivityService.DB.Models.Interfaces;

namespace UserActivityService.DB.Models
{
    public class Topic : IsDeleted
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EntityId { get; set; }
        [Required]
        public int EntityType { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public int UserCreatorId { get; set; }
        public bool IsDelet { get; set; }
    }
}
