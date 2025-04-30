using Library.Generics.Query.QueryModels.StudioGame.Library;
using Library.Generics.Query.QueryModels.StudioGame.Pagination;
using StudioGameService.DB.Model;

namespace StudioGameService.Filter.AllFilter
{
    public static class TopQueryCreate
    {
        public static IQueryable<Game> Create(QueryTop queryTop, IQueryable<Game> BodyQuery)
        {
            if (queryTop != null && BodyQuery != null)
            {
                if (queryTop.Top == true)
                {
                    BodyQuery = BodyQuery.Where(g => g.StatisticGame != null && g.StatisticGame.PeopleCount > 0)
                        .OrderByDescending(g => g.StatisticGame.PeopleCount * g.StatisticGame.RatingSum);
                }
            }

            return BodyQuery;
        }

        public static IQueryable<Studio> Create(QueryTop queryTop, IQueryable<Studio> BodyQuery)
        {
            if (queryTop != null && BodyQuery != null)
            {
                if (queryTop.Top == true)
                {
                    BodyQuery = BodyQuery.Where(s => s.Games.Any(g => g.StatisticGame != null))
                        .OrderByDescending(s => s.Games
                            .Where(g => g.StatisticGame != null)
                            .Sum(g => (long)(g.StatisticGame.PeopleCount * g.StatisticGame.RatingSum)));

                }
            }

            return BodyQuery;
        }
    }
}
