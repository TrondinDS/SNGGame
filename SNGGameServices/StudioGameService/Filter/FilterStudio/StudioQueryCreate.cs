using Library.Generics.Query.QueryModels.StudioGame.Genre;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using StudioGameService.DB.Model;

namespace StudioGameService.Filter.FilterStudio;

public static class StudioQueryCreate
{
    public static IQueryable<Studio> Create(QueryListStudio queryListStudio, IQueryable<Studio> BodyQuery)
    {
        if (queryListStudio != null && BodyQuery != null )
        {
            if (queryListStudio.ListStudioGameId?.Any() == true)
            {
                BodyQuery = BodyQuery.Where(s =>
                        queryListStudio.ListStudioGameId.Any(Id => Id == s.Id)
                    );
            }
        }

        return BodyQuery;
    }
}
