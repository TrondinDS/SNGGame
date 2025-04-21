using Library.Generics.Query.QueryModels.StudioGame.Game;
using Library.Generics.Query.QueryModels.StudioGame.Genre;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using StudioGameService.DB.Model;

namespace StudioGameService.Filter.FilterGame
{
    public static class StudioQueryCreate
    {
        public static IQueryable<Game> Create(QueryStudio queryStudio, IQueryable<Game> BodyQuery)
        {
            if ( queryStudio != null && BodyQuery != null )
            {
                if ( queryStudio.StudioId > 0 )
                {
                    BodyQuery = BodyQuery.Where(g =>
                            g.StudioId == queryStudio.StudioId
                        );
                }
            }

            return BodyQuery;
        }
    }
}
