using BannedService.DB.Models;
using Library;
using StudioGameService.DB.Model;
using System.ComponentModel.DataAnnotations;

namespace UserService.DB.Models
{
    public class User : IIsDeleted
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public required string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateBirth { get; set; }
        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public required string Email { get; set; }
        public required int KeyIdPhotoAvatar { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsGlobalModerator { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        [DataType(DataType.DateTime)]
        public DateTime? DateDeleted { get; set; }

        // Коллекция для всех банов, где пользователь является модератором
        public ICollection<Banned> ModeratedBans { get; set; } = new List<Banned>();

        // Коллекция для всех банов, где пользователь был забанен
        public ICollection<Banned> BannedRecords { get; set; } = new List<Banned>();
        // Коллекция для всех подписок пользователя
        public ICollection<UserSubscription> Subscriptions { get; set; } = new List<UserSubscription>();

        // Коллекция для всех работ пользователя
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
