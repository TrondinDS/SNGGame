using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.StudioGame.Tag;

public class QueryTag
{
    [ValidGenreIds(ErrorMessage = "Все значения в ListTagId должны быть в диапазоне от 1 до максимального значения int.")]
    public List<Guid>? ListTagId { get; set; }
}
