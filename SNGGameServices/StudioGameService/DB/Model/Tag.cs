using StudioGameService.DB.Model.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudioGameService.DB.Model
{
    public class Tag : IsDeleted
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }
        public bool IsDelet { get; set; }
    }
}
