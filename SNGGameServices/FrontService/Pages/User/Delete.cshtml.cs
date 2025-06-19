using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Library.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FrontService.Pages.User
{
    /// <summary>
    /// Страница для блокировки пользователя.
    /// </summary>
    public class DeleteModel : PageModel
    {
        private readonly IUserApiService _userApiService;
        private readonly IBannedApiService _bannedApiService;

        public DeleteModel(IUserApiService userApiService, IBannedApiService bannedApiService)
        {
            _userApiService = userApiService;
            _bannedApiService = bannedApiService;
        }

        [BindProperty]
        public UserDTO? User { get; set; }

        [BindProperty]
        public string? BanReason { get; set; }

        [BindProperty]
        public DateTime BanUntil { get; set; } = DateTime.UtcNow.AddDays(7);

        [BindProperty]
        public EntityType.Type SelectedEntityType { get; set; } = EntityType.Type.Studio;

        [BindProperty]
        public PunishmentType.Type SelectedPunishmentType { get; set; } = PunishmentType.Type.Ban;

        [BindProperty]
        [Required(ErrorMessage = "EntityId обязателен")]
        public Guid EntityId { get; set; }
        public SelectList EntityTypes { get; private set; } = default!;
        public SelectList PunishmentTypes { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id, Guid entityId)
        {
            var user = await _userApiService.GetUserByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Пользователь не найден.";
                return RedirectToPage("Index");
            }

            User = user;
            EntityId = entityId;

            LoadSelectLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User == null || User.Id == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Некорректный идентификатор пользователя.";
                return RedirectToPage("Index");
            }

            var dto = new BannedCreateDTO
            {
                EntityId = EntityId,
                EntityType = (int)SelectedEntityType,
                Reason = BanReason ?? "Не указана причина",
                DateFinish = BanUntil,
                TypePunishment = (int)SelectedPunishmentType,
                UserIdBanned = User.Id
            };

            var result = await _bannedApiService.CreateBannedAsync(dto);

            if (result == null)
            {
                TempData["ErrorMessage"] = "Не удалось заблокировать пользователя.";
                return RedirectToPage("Index");
            }

            return RedirectToPage("Index");
        }

        private void LoadSelectLists()
        {
            EntityTypes = new SelectList(Enum.GetValues(typeof(EntityType.Type))
                .Cast<EntityType.Type>()
                .Select(e => new { Value = (int)e, Text = e.ToString() }), "Value", "Text");

            PunishmentTypes = new SelectList(Enum.GetValues(typeof(PunishmentType.Type))
                .Cast<PunishmentType.Type>()
                .Select(e => new { Value = (int)e, Text = e.ToString() }), "Value", "Text");
        }

        private Guid GetCurrentUserId()
        {
            // TODO: Заменить на получение ID текущего пользователя
            return Guid.Parse("00000000-0000-0000-0000-000000000001");
        }
    }
}
