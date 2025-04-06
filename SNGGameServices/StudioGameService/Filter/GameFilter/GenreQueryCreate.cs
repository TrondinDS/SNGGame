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
                if (queryGenre.ListGenreId?.Any() == true)
                {
                    BodyQuery = BodyQuery.Where(g =>
                        queryGenre.ListGenreId.All(genreId =>
                            g.Genres.Any(gsg => gsg.GenreId == genreId)
                        )
                    );
                }
            }

            return BodyQuery;
        }
    }
}
