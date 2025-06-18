using Library.Generics.Query.QueryModels.OrganizerEvent.Event;
using Library.Generics.Query.QueryModels.OrganizerEvent.Organizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.OrganizerEvent;

public class ParamQueryEvent
{
    public QueryEvent? QueryEvent { get; set; }
    public QueryOrganizer? QueryOrganizer { get; set; }
}
