using Library.Generics.Query.QueryModels.StudioGame.Game;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;

namespace StudioGameService.Filter.GameFilter
{
    public static class GameQueryCreate
    {
        public static IQueryable<Game> Create(QueryGame queryGame, IQueryable<Game> BodyQuery)
        {
            if (queryGame != null && BodyQuery != null)
            {
                
                if ( !string.IsNullOrEmpty(queryGame.TitleGame) )
                {
                    BodyQuery = BodyQuery.Where( g =>
                        EF.Functions.ILike(g.RussianTitle, $"%{queryGame.TitleGame}%") ||
                        EF.Functions.ILike(g.EnglishTitle, $"%{queryGame.TitleGame}%") ||
                        EF.Functions.ILike(g.AlternativeTitle, $"%{queryGame.TitleGame}%")
                    );
                }

                if ( !string.IsNullOrEmpty(queryGame.CountryDevelopment) )
                {
                    BodyQuery = BodyQuery.Where( g=>
                        EF.Functions.Like(g.CountryDevelopment, $"%{queryGame.CountryDevelopment}%")
                    );
                }

                if ( !string.IsNullOrEmpty(queryGame.Platform) )
                {
                    BodyQuery = BodyQuery.Where(g =>
                        EF.Functions.Like(g.Platform, $"%{queryGame.Platform}%")
                    );
                }

                if (queryGame.ReleaseYearBefore.HasValue || queryGame.ReleaseYearBefore.HasValue)
                {
                    BodyQuery = BodyQuery.Where(g =>
                        (!queryGame.ReleaseYearFrom.HasValue || g.ReleaseDate >= queryGame.ReleaseYearFrom) &&
                        (!queryGame.ReleaseYearBefore.HasValue || g.ReleaseDate <= queryGame.ReleaseYearBefore)
                    );
                }
            }

            return BodyQuery;
        }
    }
}
