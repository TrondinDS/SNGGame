using Library;

namespace GetAwaitService.DB.Models
{
    public class UserTelegramInformation : IIsDeleted
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public int TelegramId { get; set; }

        public required DateTime DateCreate { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
