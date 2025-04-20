using Library.Generics.Query.QueryModels.StudioGame.Game;
using Library.Generics.Query.QueryModels.StudioGame.Genre;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using StudioGameService.DB.Model;

namespace StudioGameService.Filter.Filter
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

        public static IQueryable<Studio> Create(QueryStudio queryStudio, IQueryable<Studio> BodyQuery)
        {
            if (queryStudio != null && BodyQuery != null)
            {
                if (queryStudio.StudioId > 0)
                {
                    BodyQuery = BodyQuery.Where(s =>
                            s.Id == queryStudio.StudioId
                        );
                }
            }

            return BodyQuery;
        }
        
        public static IQueryable<Studio> Create(QueryListStudio paramQueryListStudio, IQueryable<Studio> BodyQuery)
        {
            if (paramQueryListStudio != null && BodyQuery != null)
            {
                if(paramQueryListStudio.ListStudioGameId?.Any() == true)
                {
                    BodyQuery = BodyQuery.Where(s => 
                            paramQueryListStudio.ListStudioGameId.Contains(s.Id)
                        );
                }
            }

            return BodyQuery;
        }

    }
}
