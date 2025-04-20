using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.StudioGame.Studio
{
    public class QueryListStudio
    {
        [ValidGenreIds(ErrorMessage = "Все значения в ListStudioGameId должны быть в диапазоне от 1 до максимального значения int.")]
        public List<int>? ListStudioGameId { get; set; }
    }
}
