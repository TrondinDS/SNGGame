using Library.Generics.Query.QueryModels.StudioGame.Library;
using Library.Generics.Query.QueryModels.StudioGame.Pagination;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.StudioGame
{
    public class ParamQueryStudio
    {
        public QueryListStudio? QueryListStudio { get; set; }

        //Pagination
        public QueryPagination? QueryPagination { get; set; }

        //Top
        public QueryTop? QueryTop { get; set; }
    }
}
