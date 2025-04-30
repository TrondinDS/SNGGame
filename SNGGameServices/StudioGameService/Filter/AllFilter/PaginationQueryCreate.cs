using Library.Generics.Query.QueryModels.StudioGame.Game;
using Library.Generics.Query.QueryModels.StudioGame.Genre;
using Library.Generics.Query.QueryModels.StudioGame.Pagination;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using StudioGameService.DB.Model;

namespace StudioGameService.Filter.AllFilter
{
    public static class PaginationQueryCreate
    {
        public static IQueryable<Game> Create(QueryPagination queryPagination, IQueryable<Game> BodyQuery)
        {
            if (queryPagination != null && BodyQuery != null)
            {
                if (queryPagination.Number > 0 && queryPagination.NumberPage >= 0)
                {
                    int skip = Math.Max(0, (queryPagination.NumberPage - 1)) * queryPagination.Number;

                    BodyQuery = BodyQuery.Skip(skip).Take(queryPagination.Number);
                }
            }

            return BodyQuery;
        }

        public static IQueryable<Studio> Create(QueryPagination queryPagination, IQueryable<Studio> BodyQuery)
        {
            if (queryPagination != null && BodyQuery != null)
            {
                if (queryPagination.Number > 0 && queryPagination.NumberPage >= 0)
                {
                    int skip = Math.Max(0, (queryPagination.NumberPage - 1)) * queryPagination.Number;

                    BodyQuery = BodyQuery.Skip(skip).Take(queryPagination.Number);
                }
            }

            return BodyQuery;
        }
    }
}
