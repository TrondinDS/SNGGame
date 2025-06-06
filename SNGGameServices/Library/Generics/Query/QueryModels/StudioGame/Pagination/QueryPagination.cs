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
        [Range(1, int.MaxValue, ErrorMessage = "Значение NumberInPage (число записей на странице) должно быть больше или равно 1.")]
        public int NumberInPage { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Значение NumberPage (номер страницы) должно быть больше или равно 1.")]
        public int NumberPage { get; set; }
    }
}
