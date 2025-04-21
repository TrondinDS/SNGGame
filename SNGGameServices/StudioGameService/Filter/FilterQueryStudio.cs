using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;
using StudioGameService.Filter.FilterStudio;

namespace StudioGameService.Filter
{
    public static class FilterQueryStudio
    {

        public static IQueryable<Studio> CreateQuerybleAsNoTraking(ParamQueryStudio paramQuerySG, IQueryable<Studio> BodyQuery)
        {
            BodyQuery = BodyQuery.AsNoTracking();

            BodyQuery = CreateQueryble(paramQuerySG, BodyQuery);

            return BodyQuery;
        }

        public static IQueryable<Studio> CreateQueryble(ParamQueryStudio paramQuerySG, IQueryable<Studio> BodyQuery)
        {
            BodyQuery = StudioQueryCreate.Create(paramQuerySG.QueryListStudio, BodyQuery);

            return BodyQuery;
        }
    }
}
