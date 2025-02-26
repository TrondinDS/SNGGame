using StudioGameService.DB.Model.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudioGameService.DB.Model
{
    public class Studio : IsDeleted
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
        public bool IsDelet { get; set; }
    }
}
