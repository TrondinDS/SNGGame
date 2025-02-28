using System.ComponentModel.DataAnnotations;
using Library;


namespace StudioGameService.DB.Model
{
    public class Studio : IIsDeleted
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public required string FilepathToDescription { get; set; }
        [Required]
        [MaxLength(255)]
        public required string FilepathToPhotoIcon { get; set; }
        [Required]
        public bool IsResolutionPublication { get; set; }
        [Required]
        public int CreatorId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
