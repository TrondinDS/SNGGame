using Library;

namespace GetAwaitService.DB.Models
{
    public class UserTelegramInformation : IIsDeleted
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ulong TelegramId { get; set; }

        public required DateTime DateCreate { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
