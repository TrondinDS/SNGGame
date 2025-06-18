namespace Library.Generics.Query.QueryModels.Administratum.ComplainTicket;

public class QueryComplainTicketComplainTicket
{
    public List<Guid>? ComplainTicketIds { get; set; }
    public List<Guid>? EntityIds { get; set; }
    public List<int>? EntityTypes { get; set; }
    public string? ComplainTypes { get; set; }
    public string? Statuses { get; set; }
    public DateTime? DatetimeBegin { get; set; }
    public DateTime? DatetimeEnd { get; set; }
}
