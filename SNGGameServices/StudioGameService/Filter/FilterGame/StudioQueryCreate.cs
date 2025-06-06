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
            if (
                 queryStudio?.StudiosId is not null && 
                 queryStudio.StudiosId.Any() && 
                 BodyQuery is not null 
               )
            {
                BodyQuery = BodyQuery.Where(g =>
                        queryStudio.StudiosId.Contains(g.StudioId)
                    );
            }

            return BodyQuery;
        }
    }
}
