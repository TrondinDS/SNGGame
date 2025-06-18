namespace Library.Generics.Query.QueryModels.Administratum.ChatFeedback;

public class QueryChatfeedbackChatfeedback
{
    public List<Guid>? ChatfeedbackId { get; set; }
    public string? Title;
    public DateTime? DatetimeBegin { get; set; }
    public DateTime? DatetimeEnd { get; set; }
    public List<Guid>? UserIds { get; set; }
    public List<Guid>? MessageIds { get; set; }
}
