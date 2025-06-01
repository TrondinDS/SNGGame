namespace GetAwaitService.DB.DTO
{
    public class BannedCheckResponseDTO
    {
        public bool IsBanned { get; set; }
        public string? Reason { get; set; }
        public DateTime? DateFinish { get; set; }
    }
}
