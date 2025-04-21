using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;
using StudioGameService.Filter.FilterGame;

namespace StudioGameService.Filter
{
    public static class FilterQueryGame
    {
        public static IQueryable<Game> CreateQuerybleAsNoTraking(ParamQueryGame paramQuerySG, IQueryable<Game> BodyQuery)
        {
            BodyQuery = BodyQuery.AsNoTracking();

            BodyQuery = CreateQueryble(paramQuerySG, BodyQuery);

            return BodyQuery;
        }

        public static IQueryable<Game> CreateQueryble(ParamQueryGame paramQuerySG, IQueryable<Game> BodyQuery)
        {
            BodyQuery = StudioQueryCreate.Create(paramQuerySG.QueryStudio, BodyQuery);

            BodyQuery = GameQueryCreate.Create(paramQuerySG.QueryGame, BodyQuery);

            BodyQuery = GenreQueryCreate.Create(paramQuerySG.QueryGenre, BodyQuery);
            BodyQuery = TagQueryCreate.Create(paramQuerySG.QueryTag, BodyQuery);

            BodyQuery = LibraryQueryCreate.Create(paramQuerySG.QueryLibrary, BodyQuery);

            return BodyQuery;
        }

    }
}
