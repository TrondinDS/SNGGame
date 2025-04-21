using Library.Generics.Query.QueryModels.StudioGame.Library;
using StudioGameService.DB.Model;

namespace StudioGameService.Filter.FilterGame
{
    public static class LibraryQueryCreate
    {
        public static IQueryable<Game> Create(QueryLibrary queryLibrary, IQueryable<Game> BodyQuery)
        {
            if ( queryLibrary != null && BodyQuery != null )
            {
                if ( queryLibrary.Rating > 0 )
                {
                    BodyQuery = BodyQuery.Where(g =>
                        g.GameLibrarys.Any() &&
                        g.GameLibrarys.Average(gl => gl.Rating) >= queryLibrary.Rating
                    );
                }

                if (queryLibrary.gameInLibraryUserId != null && queryLibrary.gameInLibraryUserId != Guid.Empty)
                {
                    BodyQuery = BodyQuery.Where(g => 
                        g.GameLibrarys.Any(gl => 
                            gl.UserId == queryLibrary.gameInLibraryUserId
                        )
                    );
                }
            }

            return BodyQuery;
        }

    }
}
