using Library.Generics.Query.QueryModels.StudioGame.Library;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;

namespace StudioGameService.Filter.FilterGame
{
    public static class LibraryQueryCreate
    {
        public static IQueryable<Game> Create(QueryLibrary queryLibrary, IQueryable<Game> BodyQuery)
        {
            if ( queryLibrary is not null && BodyQuery is not null )
            {
                if ( queryLibrary.Rating > 0 )
                {
                    BodyQuery = BodyQuery.Where(g =>
                        g.StatisticGame != null &&
                        g.StatisticGame.PeopleCount > 0 &&
                        (int) (g.StatisticGame.RatingSum / g.StatisticGame.PeopleCount) >= queryLibrary.Rating
                    );
                }

                if ( queryLibrary.UserId is not null )
                {
                    BodyQuery = BodyQuery.Where(g =>
                            g.GameLibrarys.Any(gl => gl.UserId == queryLibrary.UserId)
                        );
                }
            }

            return BodyQuery;
        }

    }
}
