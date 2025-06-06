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
                if (queryPagination.PageSize > 0 && queryPagination.PageNumber >= 0)
                {
                    int skip = Math.Max(0, (queryPagination.PageNumber - 1)) * queryPagination.PageSize;

                    BodyQuery = BodyQuery.Skip(skip).Take(queryPagination.PageSize);
                }
            }

            return BodyQuery;
        }

        public static IQueryable<Studio> Create(QueryPagination queryPagination, IQueryable<Studio> BodyQuery)
        {
            if (queryPagination != null && BodyQuery != null)
            {
                if (queryPagination.PageSize > 0 && queryPagination.PageNumber >= 0)
                {
                    int skip = Math.Max(0, (queryPagination.PageNumber - 1)) * queryPagination.PageSize;

                    BodyQuery = BodyQuery.Skip(skip).Take(queryPagination.PageSize);
                }
            }

            return BodyQuery;
        }
    }
}
