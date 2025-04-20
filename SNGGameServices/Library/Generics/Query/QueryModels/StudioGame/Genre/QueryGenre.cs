using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.StudioGame.Genre
{
    public class QueryGenre
    {
        [ValidGenreIds(ErrorMessage = "Все значения в ListGenreId должны быть в диапазоне от 1 до максимального значения int.")]
        public List<int>? ListGenreId { get; set; }
    }
}
