using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.StudioGame.Game
{
    public class QueryGame
    {
        [StringLength(255, ErrorMessage = "Название игры не должно превышать 255 символов.")]
        public string? TitleGame { get; set; }
        [StringLength(255, ErrorMessage = "Название страны не должно превышать 255 символов.")]
        public string? CountryDevelopment { get; set; }
        [StringLength(255, ErrorMessage = "Название платформы не должно превышать 255 символов.")]
        public string? Platform { get; set; }
        public DateTime? ReleaseYearFrom { get; set; }
        public DateTime? ReleaseYearBefore { get; set; }
    }
}
