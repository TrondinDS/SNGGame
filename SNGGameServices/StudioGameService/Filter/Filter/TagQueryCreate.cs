using Library.Generics.Query.QueryModels.StudioGame.Genre;
using Library.Generics.Query.QueryModels.StudioGame.Tag;
using StudioGameService.DB.Model;

namespace StudioGameService.Filter.GameFilter
{
    public static class TagQueryCreate
    {
        public static IQueryable<Game> Create(QueryTag queryTag, IQueryable<Game> BodyQuery)
        {
            if ( queryTag != null && BodyQuery != null )
            {
                if ( queryTag.ListTagId?.Any() == true )
                {
                    BodyQuery = BodyQuery.Where(g =>
                        queryTag.ListTagId.All(tagId =>
                            g.Tags.Any(gst => gst.TagId == tagId)
                        )
                    );
                }
            }

            return BodyQuery;
        }
    }
}
