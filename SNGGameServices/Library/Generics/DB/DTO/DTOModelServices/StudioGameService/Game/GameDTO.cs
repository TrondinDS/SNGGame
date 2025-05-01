using System.ComponentModel.DataAnnotations;

namespace Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game
{
    public class GameDTO
    {
        [Range(0, int.MaxValue, ErrorMessage = "Id должен быть положительным числом")]
        public Guid Id { get; set; }

        [MaxLength(255, ErrorMessage = "Русское название не должно превышать 255 символов")]
        public string RussianTitle { get; set; }

        [MaxLength(255, ErrorMessage = "Английское название не должно превышать 255 символов")]
        public string EnglishTitle { get; set; }

        [MaxLength(255, ErrorMessage = "Альтернативное название не должно превышать 255 символов")]
        public string AlternativeTitle { get; set; }

        [MaxLength(255, ErrorMessage = "Краткое описание не должно превышать 255 символов")]
        public string ShortDescription { get; set; }

        [MaxLength(255, ErrorMessage = "Ссылка на сайт не должна превышать 255 символов")]
        public string LinkSite { get; set; }

        [MaxLength(255, ErrorMessage = "Издатель не должен превышать 255 символов")]
        public string? Publisher { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [MaxLength(255, ErrorMessage = "Страна разработки не должна превышать 255 символов")]
        public string CountryDevelopment { get; set; }

        [MaxLength(
            255,
            ErrorMessage = "Ссылка на страницу в магазине не должна превышать 255 символов"
        )]
        public string LinkPageStore { get; set; }

        [MaxLength(255, ErrorMessage = "Platform не должна превышать 255 символов")]
        public string Platform { get; set; }

        [Required(ErrorMessage = "IsDeleted является обязательным")]
        public bool IsDeleted { get; set; }

        public DateTime? DateDeleted { get; set; }

        [Required(ErrorMessage = "StudioId является обязательным")]
        [Range(1, int.MaxValue, ErrorMessage = "StudioId должен быть положительным числом")]
        public Guid StudioId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "StatisticGameId должен быть положительным числом")]
        public int? StatisticGameId { get; set; }
    }
}
