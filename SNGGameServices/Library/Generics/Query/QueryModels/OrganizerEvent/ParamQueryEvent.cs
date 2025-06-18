using Library.Generics.Query.QueryModels.OrganizerEvent.Event;

namespace Library.Generics.Query.QueryModels.OrganizerEvent;

public class ParamQueryEvent
{
    public QueryEvent? QueryEvent { get; set; }
    public QueryEventOrganizer? QueryOrganizer { get; set; }
}
