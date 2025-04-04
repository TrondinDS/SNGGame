using Library.Generics.Query.QueryModels.StudioGame;
using Library.Generics.Query.QueryModels.StudioGame.Game;
using Library.Generics.Query.QueryModels.StudioGame.Genre;
using StudioGameService.DB.Model;
using StudioGameService.Filter.GameFilter;

namespace StudioGameService.Filter
{
    public static class FilterQuery
    {
        public static IQueryable<Game> CreateQueryble(ParamQuerySG paramQuerySG, IQueryable<Game> BodyQuery)
        {
            BodyQuery = CreateQuerybleGame(paramQuerySG.QueryGame, BodyQuery);
            BodyQuery = CreateQuerybleGenre(paramQuerySG.QueryGenre, BodyQuery);

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
    }
}
