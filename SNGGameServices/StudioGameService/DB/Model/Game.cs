using StudioGameService.DB.Model.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudioGameService.DB.Model
{
    public class Game : IsDeleted
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StudioId { get; set; }
        [MaxLength(255)]
        public string RussianTitle { get; set; }
        [MaxLength(255)]
        public string EnglishTitle { get; set; }
        [MaxLength(255)]
        public string AlternativeTitle { get; set; }
        [MaxLength(255)]
        public string FilepathToDescription { get; set; }
        [MaxLength(255)]
        public string FilepathToPhotoIcon { get; set; }
        [MaxLength(255)]
        public string LinkSite { get; set; }
        [MaxLength(255)]
        public string? Publisher { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        [MaxLength(255)]
        public string CountryDevelopment { get; set; }
        [MaxLength(255)]
        public string LinkPageStore { get; set; }
        [MaxLength(255)]
        public string Platform { get; set; }
        public Studio Studio { get; set; }
        public bool IsDelet { get; set; }
    }
}
