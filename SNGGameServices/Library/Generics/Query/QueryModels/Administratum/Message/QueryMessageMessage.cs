
namespace Library.Generics.Query.QueryModels.Administratum.Message;

public class QueryMessageMessage
{
    public List<Guid>? MessageIds { get; set; }
    public List<Guid>? UserIds { get; set; }
    public string? Content { get; set; }
    public DateTime? DatetimeBegin { get; set; }
    public DateTime? DatetimeEnd { get; set; }
    public List<Guid>? ChatfeedbackIds { get; set; }
}
