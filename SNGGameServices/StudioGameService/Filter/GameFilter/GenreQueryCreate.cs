using Library.Generics.Query.QueryModels.StudioGame.Genre;
using StudioGameService.DB.Model;

namespace StudioGameService.Filter.GameFilter
{
    public static class GenreQueryCreate
    {
        public static IQueryable<Game> Create(QueryGenre queryGenre, IQueryable<Game> BodyQuery)
        {
            if (queryGenre != null && BodyQuery != null)
            {
                if (queryGenre.ListGenreId != null && queryGenre.ListGenreId.Any())
                {
                    BodyQuery = BodyQuery.Where(g =>
                        g.Genres.Any(gsg => queryGenre.ListGenreId.Contains(gsg.GenreId))
                    );
                }
            }

            return BodyQuery;
        }
    }
}
