using Library;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioGameService.DB.Model
{
    public class Game : IIsDeleted
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string RussianTitle { get; set; }
        [MaxLength(255)]
        public string EnglishTitle { get; set; }
        [MaxLength(255)]
        public string AlternativeTitle { get; set; }
        [MaxLength(255)]
        public string ShortDescription { get; set; }
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
        public bool IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }


        [Required]
        public int StudioId { get; set; }
        [ForeignKey("StudioId")]
        public Studio Studio { get; set; }
        public ICollection<GameSelectedGenre> Genres { get; set; } = new List<GameSelectedGenre>();
        public ICollection<GameSelectedTag> Tags { get; set; } = new List<GameSelectedTag>();
        public ICollection<GameLibrary> GameLibrarys { get; set; } = new List<GameLibrary>();
    }
}
