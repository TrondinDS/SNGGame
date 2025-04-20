using Library.Generics.Query.QueryModels.StudioGame;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;
using StudioGameService.Filter.Filter;

namespace StudioGameService.Filter
{
    public static class FilterQueryStudio
    {

        public static IQueryable<Studio> CreateQuerybleAsNoTraking(ParamQuerySG paramQuerySG, IQueryable<Studio> BodyQuery)
        {
            BodyQuery = BodyQuery.AsNoTracking();

            BodyQuery = CreateQueryble(paramQuerySG, BodyQuery);

            return BodyQuery;
        }

        public static IQueryable<Studio> CreateQueryble(ParamQuerySG paramQuerySG, IQueryable<Studio> BodyQuery)
        {
            BodyQuery = CreateQueryableStudio(paramQuerySG.QueryStudio, BodyQuery);

            return BodyQuery;
        }

        static IQueryable<Studio> CreateQueryableStudio(QueryStudio queryStudio, IQueryable<Studio> BodyQuery)
        {
            BodyQuery = StudioQueryCreate.Create(queryStudio, BodyQuery);
            return BodyQuery;
        }



        public static IQueryable<Studio> CreateQueryableStudioListAsNoTraking(QueryListStudio paramQueryListStudio, IQueryable<Studio> BodyQuery)
        {
            BodyQuery = BodyQuery.AsNoTracking();

            BodyQuery = StudioQueryCreate.Create(paramQueryListStudio, BodyQuery);
            return BodyQuery;
        }
    }
}
