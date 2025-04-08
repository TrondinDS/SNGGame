using Library.Generics.Query.QueryModels.StudioGame;
using Library.Generics.Query.QueryModels.StudioGame.Game;
using Library.Generics.Query.QueryModels.StudioGame.Genre;
using Library.Generics.Query.QueryModels.StudioGame.Library;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using Library.Generics.Query.QueryModels.StudioGame.Tag;
using StudioGameService.DB.Model;
using StudioGameService.Filter.GameFilter;

namespace StudioGameService.Filter
{
    public static class FilterQueryGame
    {
        public static IQueryable<Game> CreateQueryble(ParamQuerySG paramQuerySG, IQueryable<Game> BodyQuery)
        {
            // I`m not testing!!!!!!!
            BodyQuery = CreateQueryableStudio(paramQuerySG.QueryStudio, BodyQuery);

            // I`m testing (OK)
            BodyQuery = CreateQuerybleGame(paramQuerySG.QueryGame, BodyQuery);
            BodyQuery = CreateQuerybleGenre(paramQuerySG.QueryGenre, BodyQuery);

            // I`m not testing!!!!!!!
            BodyQuery = CreateQuerybleTag(paramQuerySG.QueryTag, BodyQuery);
            BodyQuery = CreateQuerybleLibrary(paramQuerySG.QueryLibrary, BodyQuery);

            return BodyQuery;
        }

        static IQueryable<Game> CreateQuerybleGame(QueryGame queryGame, IQueryable<Game> BodyQuery)
        {
            BodyQuery = GameQueryCreate.Create(queryGame, BodyQuery);
            return BodyQuery;
        }
        
        static IQueryable<Game> CreateQuerybleGenre(QueryGenre queryGenre, IQueryable<Game> BodyQuery)
        {
            BodyQuery = GenreQueryCreate.Create(queryGenre, BodyQuery);
            return BodyQuery;
        }

        static IQueryable<Game> CreateQuerybleTag(QueryTag queryTag, IQueryable<Game> BodyQuery)
        {
            BodyQuery = TagQueryCreate.Create(queryTag, BodyQuery);
            return BodyQuery;
        }

        static IQueryable<Game> CreateQuerybleLibrary(QueryLibrary queryLibrary, IQueryable<Game> BodyQuery)
        {
            BodyQuery = LibraryQueryCreate.Create(queryLibrary, BodyQuery);
            return BodyQuery;
        }

        static IQueryable<Game> CreateQueryableStudio(QueryStudio queryStudio, IQueryable<Game> BodyQuery)
        {
            BodyQuery = StudioQueryCreate.Create(queryStudio, BodyQuery);
            return BodyQuery;
        }
    }
}
