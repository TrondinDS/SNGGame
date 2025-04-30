using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;
using StudioGameService.Filter.AllFilter;
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
            // 1. Фильтрация
            BodyQuery = StudioQueryCreate.Create(paramQuerySG.QueryStudio, BodyQuery);
            BodyQuery = GameQueryCreate.Create(paramQuerySG.QueryGame, BodyQuery);
            BodyQuery = GenreQueryCreate.Create(paramQuerySG.QueryGenre, BodyQuery);
            BodyQuery = TagQueryCreate.Create(paramQuerySG.QueryTag, BodyQuery);
            BodyQuery = LibraryQueryCreate.Create(paramQuerySG.QueryLibrary, BodyQuery);

            // 2. Сортировка (ТОП)
            BodyQuery = TopQueryCreate.Create(paramQuerySG.QueryTop, BodyQuery);

            // 3. Пагинация
            BodyQuery = PaginationQueryCreate.Create(paramQuerySG.QueryPagination, BodyQuery);

            return BodyQuery;
        }

    }
}
