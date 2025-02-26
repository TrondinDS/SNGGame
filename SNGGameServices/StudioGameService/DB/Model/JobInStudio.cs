using StudioGameService.DB.Model.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudioGameService.DB.Model
{
    public class JobInStudio : IsDeleted
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int StudioId { get; set; }
        public bool IsModerator { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        public DateTime? DateFinish { get; set; } 
        [MaxLength(255)]
        public string Position { get; set; }
        public Studio Studio { get; set; }
        public bool IsDelet { get; set; }
    }
}
