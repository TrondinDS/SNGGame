using Library.Generics.Query.QueryModels.StudioGame.Game;
using Library.Generics.Query.QueryModels.StudioGame.Genre;
using Library.Generics.Query.QueryModels.StudioGame.Library;
using Library.Generics.Query.QueryModels.StudioGame.Pagination;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using Library.Generics.Query.QueryModels.StudioGame.Tag;

namespace Library.Generics.Query.QueryModels.StudioGame;

public class ParamQueryGame
{
    //Studio
    public QueryStudio? QueryStudio { get; set; }

    //Game
    public QueryGame? QueryGame { get; set; }

    //Genre
    public QueryGenre? QueryGenre { get; set; }

    //Tag
    public QueryTag? QueryTag { get; set; }

    //Librari
    public QueryLibrary? QueryLibrary { get; set; }

    //Pagination
    public QueryPagination? QueryPagination { get; set; }

    //Top
    public QueryTop? QueryTop { get; set; }

}
