using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.StudioGame.Pagination
{
    public class QueryPagination
    {
        [Range(1, int.MaxValue, ErrorMessage = "Значение Number должно быть больше или равно 1.")]
        public int Number { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Значение NumberPage должно быть больше или равно 1.")]
        public int NumberPage { get; set; }
    }
}
